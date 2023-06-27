using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Data.Contracts;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Consumer.Services.HCMDataServices.Contracts;
using ColleagueInt.RTW.Consumer.Services.myHRDataReceiver.Contracts;
using ColleagueInt.RTW.Core;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.EventHandler.EventHandler;
using ColleagueInt.RTW.EventHandler.EventHandler.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ColleagueInt.RTW.Consumer.Data.RTWData;

namespace ColleagueInt.RTW.Consumer.Services.myHRDataReceiver
{
    public abstract class BaseEnrichedDataReceiver
    {
        private readonly ILogger<BaseEnrichedDataReceiver> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventHubAdapter _eventHubAdapter;
        private readonly EventHubSettings _eventHubSettings;
        private readonly GeneralSettings _generalSettings;
        private readonly HcmSettings _hcmSettings;
        private readonly IColleagueService _colleagueService;
        private readonly IFilterDataService _filterDataService;
        private readonly IJwTokenService _jwTokenService;
        private readonly IHCMAdditionalData _hcmAdditionalData;
        private IEventHubDataReceiver _eventHubDataReceiver;
        private readonly IDataMapper _dataMapper;
        private string EventType { get; set; }
        private IEnumerable<FilterDataViewModel> filterDataViewModel;
        private List<string> allowedBusinessUnits;

        protected BaseEnrichedDataReceiver(
                        ILogger<BaseEnrichedDataReceiver> logger,
                        IServiceProvider serviceProvider,
                        IEventHubAdapter eventHubAdapter,
                        IOptions<EventHubSettings> eventHubSettings,
                        IOptions<GeneralSettings> generalSettings,
                        IOptions<HcmSettings> hcmSettings,
                        IColleagueService colleagueService,
                        IFilterDataService filterDataService,
                        IJwTokenService jwTokenService,
                        IHCMAdditionalData hcmAdditionalData,
                        IDataMapper dataMapper,
                        string eventType
            )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _eventHubAdapter = eventHubAdapter;
            _eventHubSettings = eventHubSettings.Value;
            _generalSettings = generalSettings.Value;
            _hcmSettings = hcmSettings.Value;
            _colleagueService = colleagueService;
            _filterDataService = filterDataService;
            _jwTokenService = jwTokenService;
            _hcmAdditionalData = hcmAdditionalData;
            _dataMapper = dataMapper;
            EventType = eventType;
        }

        public async void InitializeDataAsync()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    if (EventType == "Published")
                    {
                        EventHubClientData enrichEHClientData = new EventHubClientData
                        {
                            EHNS_ConnectionString = _eventHubSettings.EHNS_ConnectionString,
                            EH_Stg_ConnectionString = _eventHubSettings.EH_Stg_ConnectionString,
                            EH_Name = _eventHubSettings.EHName_EnrichedPublished,
                            EH_BlobContainerName = _eventHubSettings.EHBlobContainer_sub_EnrichedPublished,
                            EH_ConsumerGroup = _eventHubSettings.EHConsumerGroup_sub_EnrichedPublished
                        };
                        _eventHubDataReceiver = _eventHubAdapter.GetIEventHubReceiverAsync(enrichEHClientData);
                    }
                    if (EventType == "Updated")
                    {
                        EventHubClientData enrichEHClientData = new EventHubClientData
                        {
                            EHNS_ConnectionString = _eventHubSettings.EHNS_ConnectionString,
                            EH_Stg_ConnectionString = _eventHubSettings.EH_Stg_ConnectionString,
                            EH_Name = _eventHubSettings.EHName_EnrichedUpdated,
                            EH_BlobContainerName = _eventHubSettings.EHBlobContainer_sub_EnrichedUpdated,
                            EH_ConsumerGroup = _eventHubSettings.EHConsumerGroup_sub_EnrichedUpdated
                        };
                        _eventHubDataReceiver = _eventHubAdapter.GetIEventHubReceiverAsync(enrichEHClientData);
                    }

                    //Get filter data from database table 
                    filterDataViewModel = await _filterDataService.GetAllValidFiltersAsync();
                    allowedBusinessUnits = filterDataViewModel.Select(x => x.BusinessUnitName).ToList();


                    _eventHubDataReceiver.EventHubData += EventHubDataReceiver_EventHubData;
                    Logging.LogInformation(_logger, $"{EventType} Eventhub configuration for RTW completed.");

                    // TODO - Handle Event Hub Errors too.
                    //_eventHubDataReceiver.EventHubError += EventHubDataReceiver_EventHubError;

                    await _eventHubDataReceiver.StartReceivingMessagesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EventHubDataReceiver_EventHubData(object? sender, EventHubMessageEventArgs eventArgs)
        {
            EventHubDataReceiverEventHandler(eventArgs).GetAwaiter().GetResult();
        }

        private async Task EventHubDataReceiverEventHandler(EventHubMessageEventArgs eventArgs)
        {
            try
            {
                var encryptedMessage = eventArgs.EventMessage;
                var logMessage = "";
                if (!String.IsNullOrEmpty(encryptedMessage))
                {
                    var decryptedMessage = DataProtector.Decrypt(encryptedMessage);
                    ColleagueEntryEvent colleagueEntryEvent = JsonHelper.ToClass<ColleagueEntryEvent>(decryptedMessage);
                    var feedType = colleagueEntryEvent.FeedType;
                    ColleagueEntry colleagueEntry = colleagueEntryEvent.ColleagueEntry;


                    if ((feedType == FeedTypeConstants.NewHire && !colleagueEntry.IsFutureDatedData) ||  // <-New starters  
                          (feedType == FeedTypeConstants.EmployeeUpdates &&                              // <- New starters with changed start date
                            colleagueEntry.HireDate == DateTime.Now.ToString("yyy-MM-dd") &&
                            colleagueEntry.PreviousStartDate != DateTime.Now.ToString("yyy-MM-dd") &&
                            !string.IsNullOrEmpty(colleagueEntry.PreviousStartDate)))
                    {
                        var firstAssignment = colleagueEntry.assignments.FirstOrDefault();

                        // To handle new hires from Global Transfer -- no legal requirement for RTW check
                        if (firstAssignment.ActionCode == "GLB_TRANSFER")
                        {
                            logMessage = $"EventType : {EventType} -- {eventArgs.EnqueuedTime} -- Data not processed for PersonNumber {colleagueEntry.PersonNumber}, Reason: New Hire due to Global Transfer not in scope.";
                            Logging.LogInformation(_logger, logMessage);
                            return;
                        }

                        var enableDetailLog = _generalSettings.EnableDetailedLogging;
                        var allowedColleagueTypes = _generalSettings.RTW_AllowedColleagueTypes;
                        if (!allowedColleagueTypes.Contains(colleagueEntry.WorkerType))
                        {
                            logMessage = $"EventType : {EventType} -- {eventArgs.EnqueuedTime} -- Data not processed for PersonNumber {colleagueEntry.PersonNumber}, Reason: WorkerType {colleagueEntry.WorkerType} not in scope";
                            Logging.LogInformation(_logger, logMessage);
                            return;
                        }

                        if (!allowedBusinessUnits.Any(s => s.Contains(colleagueEntry.BusinessUnit)))
                        {
                            logMessage = $"EventType : {EventType} -- {eventArgs.EnqueuedTime} -- Data not processed for PersonNumber {colleagueEntry.PersonNumber}, Reason: BusinessUnit {colleagueEntry.BusinessUnit} not in scope";
                            Logging.LogInformation(_logger, logMessage);
                            return;
                        }
                        bool checkRequestRequired = _colleagueService.AbleToCreateCheckRequestForColleagueAsync(colleagueEntry.PersonNumber).GetAwaiter().GetResult();
                        if (checkRequestRequired)
                        {

                            var trackingReference = _colleagueService.GetTrackingReferenceAsync(colleagueEntry.PersonNumber).GetAwaiter().GetResult();
                            PersonCheckRequest rtwColleageDetails;
                            var jsonData = "";
                            //Check if we need to retrieve line managers cost centre for this BU
                            List<string> managerCostCentreBU = _generalSettings.RTW_Get_ManagerCostCentre_ForBUs.Split('|').ToList();

                            if (managerCostCentreBU.Any(s => s.Contains(colleagueEntry.BusinessUnit)))
                            {
                                logMessage = $"Proceeding to retrieve line manager's cost centre for PersonNumber: {colleagueEntry.PersonNumber}, BusinessUnit: {colleagueEntry.BusinessUnit}";
                                Logging.LogInformation(_logger, logMessage);

                                //Get manager's cost centre using BI report
                                if (colleagueEntry.ManagerPersonNumber != null)
                                {
                                    string managerCostCentre = await _hcmAdditionalData.GetColleagueCostCentre(colleagueEntry.ManagerPersonNumber);
                                    if (!String.IsNullOrEmpty(managerCostCentre))
                                    {
                                        rtwColleageDetails = _dataMapper.GenerateRTWColleagueObject(colleagueEntry, trackingReference, managerCostCentre);
                                        jsonData = JsonHelper.FromClass(rtwColleageDetails);
                                        logMessage = $"Retrieved line manager's cost centre: {managerCostCentre} for PersonNumber: {colleagueEntry.PersonNumber}, BusinessUnit: {colleagueEntry.BusinessUnit}";
                                        Logging.LogInformation(_logger, logMessage);
                                    }
                                    else
                                    {
                                        rtwColleageDetails = _dataMapper.GenerateRTWColleagueObject(colleagueEntry, trackingReference);
                                        jsonData = JsonHelper.FromClass(rtwColleageDetails);
                                    }
                                }
                            }
                            else
                            {
                                rtwColleageDetails = _dataMapper.GenerateRTWColleagueObject(colleagueEntry, trackingReference);
                                jsonData = JsonHelper.FromClass(rtwColleageDetails);
                            }

                            ColleagueViewModel colleagueView = new ColleagueViewModel
                            {
                                PersonNumber = colleagueEntry.PersonNumber,
                                TrackingReference = trackingReference,
                                JsonData = DataProtector.Encrypt(jsonData),
                                StartDate = Convert.ToDateTime(colleagueEntry.HireDate),
                                StageId = Stages.InitalStage,
                                LastUpdateOn = DateTime.UtcNow
                            };
                            await _colleagueService.AddColleagueAsync(colleagueView);

                            logMessage = $"Processing data for PersonNumber : {colleagueEntry.PersonNumber}, EventType : {EventType} -- {eventArgs.EnqueuedTime} and data added in database.";
                            Logging.LogInformation(_logger, logMessage);

                            if (enableDetailLog)
                            {
                                var detailedLog = $"Data persisted in database for PersonNumber : {colleagueEntry.PersonNumber}, json : {jsonData}.";
                                Logging.LogInformation(_logger, detailedLog);
                            }
                            return;
                        }
                        else
                        {
                            logMessage = $"EventType : {EventType} -- {eventArgs.EnqueuedTime} -- Data not processed for PersonNumber {colleagueEntry.PersonNumber}, Reason: Right to work check already scheduled.";
                            Logging.LogInformation(_logger, logMessage);
                            return;
                        }
                    }
                    else
                    {
                        if (feedType == FeedTypeConstants.NewHire && colleagueEntry.IsFutureDatedData)
                        {
                            // This is a new starter data with Future Dated Data
                            logMessage = $"EventType : {EventType} -- {eventArgs.EnqueuedTime} -- Data not processed for PersonNumber {colleagueEntry.PersonNumber}, Reason: Future starter not in scope";
                            Logging.LogInformation(_logger, logMessage);
                            return;
                        }
                        else
                        {
                            //This is not a new starter data
                            logMessage = $"EventType : {EventType} -- {eventArgs.EnqueuedTime} -- Data not processed for PersonNumber {colleagueEntry.PersonNumber}, Reason: Not a NEW HIRE";
                            Logging.LogInformation(_logger, logMessage);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
            }
        }  

        public async Task AddNewStarter(ColleagueViewModel colleagueViewModel)
        {
            using var scope = _serviceProvider.CreateScope();
            var colleagueService = scope.ServiceProvider.GetRequiredService<IColleagueService>();
            await colleagueService.AddColleagueAsync(colleagueViewModel);
        }

        
    }
}