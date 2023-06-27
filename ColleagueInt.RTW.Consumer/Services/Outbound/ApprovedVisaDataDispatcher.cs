using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Consumer.Services.HCMDataServices.Contracts;
using ColleagueInt.RTW.Core;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ColleagueInt.RTW.Consumer.Data.RTWData;

namespace ColleagueInt.RTW.Consumer.Services.Outbound
{
    public class ApprovedVisaDataDispatcher : BackgroundService
    {

        private readonly ILogger<DataDispatcherService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly GeneralSettings _generalSettings;
        private readonly RTWSettings _rtwSettings;
        private readonly IAzureApiService _restApiService;
        private readonly HcmSettings _hcmSettings;
        private readonly IJwTokenService _jwTokenService;
        private readonly IColleagueService _colleagueService;
        private readonly IHCMAdditionalData _hcmAdditionalData;
        private static bool _enableDetailedLogging;
        private static string _createRequestURL;
        

        public ApprovedVisaDataDispatcher(        
                        ILogger<DataDispatcherService> logger,
                        IOptions<GeneralSettings> generalSettings,
                        IOptions<RTWSettings> rtwSettings,
                        IServiceProvider serviceProvider,
                        IAzureApiService restApiService,
                        IOptions<HcmSettings> hcmSettings,
                        IJwTokenService jwTokenService,
                        IColleagueService colleagueService,
                        IHCMAdditionalData hcmAdditionalData)
                      
        {
            _logger = logger;
            _generalSettings = generalSettings.Value;
            _rtwSettings = rtwSettings.Value;
            _serviceProvider = serviceProvider;
            _restApiService = restApiService;
            _hcmSettings = hcmSettings.Value;
            _jwTokenService = jwTokenService;
            _colleagueService = colleagueService;
            _hcmAdditionalData = hcmAdditionalData;
        }


        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    _enableDetailedLogging = _generalSettings.EnableDetailedLogging;
                    _createRequestURL = _rtwSettings.CreateRequestURL;
                    
                }
                catch (Exception ex)
                {
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, ex);
                }
            }
            await base.StartAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                   
                    var appSettingsService = scope.ServiceProvider.GetRequiredService<IAppSettingsService>();
                    var refreshVisaApprovedColleaguesDailyAt = await appSettingsService.GetAppSetttingForKeyAsync(AppSettings.RefreshVisaApprovedColleaguesDailyAt);
                    var lastTimeReadVisaApprovedColleagues = await appSettingsService.GetAppSetttingForKeyAsync(AppSettings.LastTimeReadVisaApprovedColleagues);

                    var fromDate = Convert.ToDateTime(lastTimeReadVisaApprovedColleagues.Value, new CultureInfo("en-GB")).ToString("yyyy-MM-dd");
                    var toDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                    if (DateTime.Now.Hour.ToString() == refreshVisaApprovedColleaguesDailyAt.Value)
                    {
                        var now = DateTime.Now;
                        var startTime = appSettingsService.GetAppSetttingForKeyAsync("RTW-To-HCM-InboundFlow-DownTime-Start").GetAwaiter().GetResult().Value;
                        var endTime = appSettingsService.GetAppSetttingForKeyAsync("RTW-To-HCM-InboundFlow-DownTime-End").GetAwaiter().GetResult().Value;
                        DateTime downTimeStart = Convert.ToDateTime(startTime, new CultureInfo("en-GB"));
                        DateTime downTimeEnd = Convert.ToDateTime(endTime, new CultureInfo("en-GB"));

                        if (now >= downTimeStart && now <= downTimeEnd)
                        {
                            Logging.LogInformation(_logger, $"Planned downtime for RTW application, the outbound service will be suspended between ServiceDowntime_Start: {startTime} to ServiceDowntime_End: {endTime}");
                            var delayInMins = (downTimeEnd - now).TotalMinutes;

                            Logging.LogInformation(_logger, $"Task delayed for {delayInMins} minutes");
                            await Task.Delay(TimeSpan.FromMinutes(delayInMins), stoppingToken);

                            continue;
                        }
                        else
                        {
                            var logMessage = "";

                            logMessage = $"Start retrieving Approved Visa Checks from HCM.";
                            Logging.LogInformation(_logger, logMessage);

                            var result = await ColleagueWithExpiredVisaDocumentAsync(fromDate, toDate);

                            if (result != null)
                            {
                                if (result.Any())
                                {
                                    foreach (HCMExpiredDocDetails expiredDocDetails in result)
                                    {
                                        foreach (HCMExpiredDocDetails.Item item in expiredDocDetails.items)
                                        {
                                            var employeeWorkTypes = item.workRelationships.Where(x => x.WorkerType == "E").ToList();
                                            var latestAssignment = employeeWorkTypes.Where(x => x.assignments.Where(x => x.AssignmentStatusType == "ACTIVE").OrderByDescending(x => x.AssignmentStatusType).Any()).FirstOrDefault();
                                            if (latestAssignment == null)
                                            {
                                                logMessage = $"Approved visa checks not processed for PersonNumber: { item.PersonNumber}, don't have any active assignment.";
                                                Logging.LogInformation(_logger, logMessage);
                                                continue;
                                            }
                                            var assignmentId = latestAssignment.assignments.FirstOrDefault().AssignmentId;
                                            var businessUnitName = latestAssignment.assignments.FirstOrDefault().BusinessUnitName;
                                            var managerAssignmentNumber = latestAssignment.assignments.FirstOrDefault().managers.FirstOrDefault() != null ? latestAssignment.assignments.FirstOrDefault().managers.FirstOrDefault().ManagerAssignmentNumber : "";
                                            var personId = item.PersonId;
                                            var personNumber = item.PersonNumber;

                                            if (!string.IsNullOrEmpty(assignmentId))
                                            {
                                                var locationId = "";
                                                locationId = await GetColleagueCostCentre(personNumber, businessUnitName, long.Parse(personId), long.Parse(assignmentId), managerAssignmentNumber, locationId);

                                                if (!string.IsNullOrEmpty(locationId))
                                                {
                                                    bool checkRequestRequired = _colleagueService.AbleToCreateDocumentCheckRequestForColleagueAsync(personNumber).GetAwaiter().GetResult();
                                                    if (checkRequestRequired)
                                                    {
                                                        PersonCheckRequest rtwColleageDetails = new PersonCheckRequest
                                                        {
                                                            FirstName = item.names[0].FirstName,
                                                            LastName = item.names[0].LastName,
                                                            LocationId = locationId,
                                                            AdditionalIdentifier = personNumber
                                                        };

                                                        rtwColleageDetails.TrackingReference = _colleagueService.GetTrackingReferenceAsync(personNumber).GetAwaiter().GetResult();
                                                        var jsonData = JsonHelper.FromClass(rtwColleageDetails);

                                                        ColleagueViewModel colleagueView = new ColleagueViewModel
                                                        {
                                                            PersonNumber = personNumber,
                                                            TrackingReference = rtwColleageDetails.TrackingReference,
                                                            JsonData = DataProtector.Encrypt(jsonData),
                                                            StartDate = Convert.ToDateTime(DateTime.UtcNow),
                                                            StageId = Stages.InitalStage,
                                                            LastUpdateOn = DateTime.UtcNow
                                                        };
                                                        await _colleagueService.AddColleagueAsync(colleagueView);

                                                        logMessage = $"Approved visa checks processing for PersonNumber: {personNumber} and data added into the database.";
                                                        Logging.LogInformation(_logger, logMessage);

                                                        if (_enableDetailedLogging)
                                                        {
                                                            var detailedLog = $"Data persisted in database for PersonNumber: {personNumber}, json: {jsonData}.";
                                                            Logging.LogInformation(_logger, detailedLog);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        logMessage = $"Approved visa checks not processed for PersonNumber: {personNumber}, Reason: Right to work check already scheduled.";
                                                        Logging.LogInformation(_logger, logMessage);
                                                    }
                                                }
                                                else
                                                {
                                                    logMessage = $"Approved visa checks not processed for PersonNumber: {personNumber}, Reason: Cost Centre available. " +
                                                        $"Business Unit Name: {businessUnitName}, PersonId: {personId}, AssignmentId: {assignmentId}, Manager Assignment Number: {managerAssignmentNumber}";
                                                    Logging.LogInformation(_logger, logMessage);
                                                }
                                            }
                                            else
                                            {
                                                logMessage = $"Approved visa checks not processed for PersonNumber: {personNumber}, Reason: AssignmentId not available.";
                                                Logging.LogInformation(_logger, logMessage);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    logMessage = $"No Approved Visa Checks found in the HCM";
                                    Logging.LogInformation(_logger, logMessage);
                                }

                                lastTimeReadVisaApprovedColleagues.Value = DateTime.Now.ToString("yyyy-MM-dd");
                                await appSettingsService.UpdateAppSetttingAsync(lastTimeReadVisaApprovedColleagues);
                            }
                            else
                            {
                                var error = new Exception($"Error occurred while retrieving approved visa details from HCM during background process");
                                await Logging.LogExceptionAsync(_serviceProvider, _logger, error, IncidentErrorDescription.GenericException);
                                Logging.LogInformation(_logger, $"{error}");
                            }
                        }
                    }
                    else
                    {
                        Logging.LogInformation(_logger, $"'Approved Visa Checks' - Scheduled to process after {refreshVisaApprovedColleaguesDailyAt.Value}.00");
                    }

                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
                                    
                catch (Exception ex)
                {
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
                }
            }

           
        }

        private async Task<string> GetColleagueCostCentre(string personNumber, string businessUnitName, long personId, long assignmentId, string managerAssignmentNumber, string locationId)
        {
            List<string> managerCostCentreBU = _generalSettings.RTW_Get_ManagerCostCentre_ForBUs.Split('|').ToList();

            if (managerCostCentreBU.Any(s => s.Contains(businessUnitName)))
            {
                if (managerAssignmentNumber.Length == 8)
                {
                    var managerpersonNumber = managerAssignmentNumber.Substring(1, 7);
                    locationId = await _hcmAdditionalData.GetColleagueCostCentre(managerpersonNumber.ToString());
                    Logging.LogInformation(_logger, $"Retrieved line manager's cost centre: {locationId} for PersonNumber: {personNumber}, " +
                        $"BusinessUnit: {businessUnitName}, Manager Person Number: {managerpersonNumber}");
                }
                else
                {
                    Logging.LogInformation(_logger, $"Retrieved line manager's cost centre: {locationId} for PersonNumber: {personNumber}, " +
                        $"BusinessUnit: {businessUnitName}, Manager Person Number: invalid");
                }
            }
            else
            {
                locationId = await _hcmAdditionalData.GetColleagueCostCentre(personId, assignmentId);
                Logging.LogInformation(_logger, $"Retrieved Colleague cost centre: {locationId} for PersonNumber: {personNumber}, " +
                    $"BusinessUnit: {businessUnitName}, PersonId {personId}, AssignmentId: {assignmentId}");
            }
            return locationId;
        }

        private async Task<List<HCMExpiredDocDetails>> ColleagueWithExpiredVisaDocumentAsync(string  fromDate, string toDate )
        {
            var hasMore = true;
            var offset = 0;
            List<HCMExpiredDocDetails> hCMExpiredDocDetails = new List<HCMExpiredDocDetails>();
            var hcmServerHost = _hcmSettings.HcmServerHost;
            
            while (hasMore)
            {
                var hcmDetailPersonUrl = $"{hcmServerHost}/hcmRestApi/resources/latest/workers?q=(visasPermits.ExpirationDate>'{fromDate}' and visasPermits.ExpirationDate<'{toDate}' ) and (visasPermits.VisaPermitType='MHR_COA_HOME_OFF_APPLICATION')&onlyData=true&fields=PersonId,PersonNumber;names:FirstName,LastName;visasPermits:VisaPermitId,VisaPermitType,ExpirationDate;workRelationships:LegalEmployerName,WorkerType;workRelationships.assignments:AssignmentNumber,AssignmentStatusType,BusinessUnitName,SystemPersonType,EffectiveStartDate,AssignmentId;workRelationships.assignments.managers:ManagerAssignmentNumber&totalResults=true&orderBy=PersonNumber:desc&offset={offset}&limit=100";

                var restResponse = await _jwTokenService.GetResponseWithJWTokenAsync(hcmDetailPersonUrl, CacheConstants.CloudApplicationCertificate);
                if (restResponse.IsSuccessful)
                {
                    try
                    {
                        var result = JsonHelper.ToClass<HCMExpiredDocDetails>(restResponse.Content);
                        if (result.count == 0)
                        {
                            return hCMExpiredDocDetails;
                        }

                        hasMore = result.hasMore;
                        offset += result.limit;
                        hCMExpiredDocDetails.Add(result);
                    }
                    catch (Exception ex)
                    {
                        var error = new Exception($"Error occurred while retrieving HCM URLs; Message: {ex.Message}; Response: {restResponse.Content}");
                        await Logging.LogExceptionAsync(_serviceProvider, _logger, error, IncidentErrorDescription.HCMUpdateError);
                        return null;
                    }
                }
                else
                {
                    var logMessage = $"Error occurred while retrieving HCM URLs, Return Data: {restResponse.Content}";
                    var error = new Exception(logMessage);
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, error, IncidentErrorDescription.HCMUpdateError);
                    return null;
                }
            }
            return hCMExpiredDocDetails;
        }
    }
}