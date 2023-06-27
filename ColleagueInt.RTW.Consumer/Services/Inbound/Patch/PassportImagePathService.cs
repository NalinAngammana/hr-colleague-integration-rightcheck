using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Core;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ColleagueInt.RTW.Consumer.Data.Contracts;
using static ColleagueInt.RTW.Consumer.Data.HCMRetrieveData;
using System.Globalization;

namespace ColleagueInt.RTW.Consumer.Services.Inbound.Patch
{
    class PassportImagePathService : BackgroundService
    {
        private readonly ILogger<PassportImagePathService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly HcmSettings _hcmSettings;
        private readonly GeneralSettings _generalSettings;
        private readonly RTWSettings _rtwSettings;
        private readonly IAzureApiService _restApiService;
        private readonly IJwTokenService _jwTokenService;
        private readonly IDataMapper _dataMapper;

        private static bool _enableDetailedLogging;
        private static string _getPersonDetailsBaseURL;
        private static string _getPersonDocSetBaseURL;
        private IEnumerable<DocumentTypeViewModel> documentTypeViewModel;
        private IInboundErrorLogService _inboundErrorService;

        public PassportImagePathService(
                        ILogger<PassportImagePathService> logger,
                        IOptions<HcmSettings> hcmSettings,
                        IOptions<GeneralSettings> generalSettings,
                        IOptions<RTWSettings> rtwSettings,
                        IServiceProvider serviceProvider,
                        IAzureApiService restApiService,
                        IJwTokenService jwTokenService,
                        IDataMapper dataMapper
            )

        {
            _logger = logger;
            _hcmSettings = hcmSettings.Value;
            _generalSettings = generalSettings.Value;
            _rtwSettings = rtwSettings.Value;
            _serviceProvider = serviceProvider;
            _restApiService = restApiService;
            _jwTokenService = jwTokenService;
            _dataMapper = dataMapper;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    _enableDetailedLogging = _generalSettings.EnableDetailedLogging;
                    _getPersonDetailsBaseURL = _rtwSettings.PersonDetailsURL;
                    _getPersonDocSetBaseURL = _rtwSettings.PersonCurrentDocumentSetURL;

                }
                catch (Exception ex)
                {
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
                }
            }
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!(stoppingToken.IsCancellationRequested))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    //Check service downtime flag
                    var appSettingsService = scope.ServiceProvider.GetRequiredService<IAppSettingsService>();
                    var patchActiveFlag = appSettingsService.GetAppSetttingForKeyAsync("RTW-To-HCM-InboundPastportUpdate").GetAwaiter().GetResult();
                    var patchCutOff= appSettingsService.GetAppSetttingForKeyAsync("RTW-To-HCM-InboundPastportUpdateCutOffTime").GetAwaiter().GetResult().Value; 
                    var logMessage = "";

                    if (bool.Parse(patchActiveFlag.Value))
                    {
                        var count = 100;
                        DateTime patchCutOffDate = Convert.ToDateTime(patchCutOff, new CultureInfo("en-GB"));

                        var colleagueService = scope.ServiceProvider.GetRequiredService<IColleagueService>();
                        var colleagueList = await colleagueService.GetHCMUpdatedColleagueListAsync(count, patchCutOffDate);
                        _inboundErrorService = scope.ServiceProvider.GetRequiredService<IInboundErrorLogService>();

                        if (colleagueList.Any())
                        {

                            var documentTypeService = scope.ServiceProvider.GetRequiredService<IDocumentTypeService>();
                            documentTypeViewModel = await documentTypeService.GetAllDocumentTypesAsync();

                            foreach (var colleagueViewModelData in colleagueList)
                            {
                                var docRecordCount = 0;

                                if (colleagueViewModelData.ClientId == null)
                                {
                                    logMessage = $"ClientId not available for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                    await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchFailed);
                                    await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                }
                                else
                                {
                                    logMessage = $"Started Reading RTW data for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                    Logging.LogInformation(_logger, logMessage);

                                    var getPersonDetailsURL = _getPersonDetailsBaseURL.Replace("{personId}", colleagueViewModelData.ClientId);
                                    IRestResponse restPersonResponse = await _restApiService.GetResponseAsync(getPersonDetailsURL, null);

                                    if (restPersonResponse.IsSuccessful)
                                    {
                                        HCMUpdateData hcmData = new HCMUpdateData();
                                        var personDataRTW = JsonConvert.DeserializeObject<Person>(restPersonResponse.Content);
                                        if (personDataRTW == null)
                                        {
                                            logMessage = $"Unable to read person details from RTW for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                            await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchFailed);
                                            await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                        }
                                        else
                                        {
                                            hcmData.PersonNumber = colleagueViewModelData.PersonNumber;
                                            var reviewStatus = personDataRTW.personDetails.reviewStatus;
                                            var employeeNumber = personDataRTW.personDetails.customField2; // Employee Number

                                            var hcmWorker = GetHCMWorkerAsync(hcmData, personDataRTW).GetAwaiter().GetResult();
                                            if (hcmWorker == null)
                                            {
                                                await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchFailed);
                                                continue;
                                            }

                                            var getPersonDocSetURL = _getPersonDocSetBaseURL.Replace("{personId}", colleagueViewModelData.ClientId);
                                            IRestResponse restDocumentResponse = await _restApiService.GetResponseAsync(getPersonDocSetURL, null);
                                            if (restDocumentResponse.IsSuccessful)
                                            {
                                                var currentDocSet = JsonConvert.DeserializeObject<CurrentDocumentSet>(restDocumentResponse.Content);
                                                if (!(String.IsNullOrEmpty(hcmData.PersonNumber) && String.IsNullOrEmpty(employeeNumber)))
                                                {

                                                    var docSetRTW = currentDocSet.documents.Where(x => x.documentType.name == "Passport").FirstOrDefault();
                                                    if (docSetRTW == null)
                                                    {
                                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMDocUploaded);
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        if (!String.IsNullOrWhiteSpace(docSetRTW.documentImages))
                                                        {
                                                            IRestResponse restDocImageResponse = await _restApiService.GetResponseAsync(docSetRTW.documentImages, null);
                                                            if (restDocImageResponse.IsSuccessful)
                                                            {
                                                                HCMDocumentRecord hcmDocRec = new HCMDocumentRecord();
                                                                var docImageRTWList = JsonConvert.DeserializeObject<List<Image>>(restDocImageResponse.Content).ToList();

                                                                if (docImageRTWList.Count<=1)
                                                                {
                                                                    await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchIgnored);
                                                                    continue;
                                                                }
                                                                foreach (Image docImageRTW in docImageRTWList)
                                                                {
                                                                    docRecordCount++;
                                                                    var result = GenerateHCMDocRecordModel(docSetRTW, docImageRTW, hcmData.PersonNumber, hcmDocRec, docRecordCount);
                                                                    if (!result)
                                                                    {
                                                                        logMessage = $"Error occurred while generating document model for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchFailed);
                                                                        await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.HCMUpdateError, colleagueViewModelData.PersonNumber);
                                                                        continue;
                                                                    }

                                                                    var uploadStatus = UploadHCMDocRecordAsync(docImageRTW, hcmDocRec, docSetRTW.documentType.name).GetAwaiter().GetResult();
                                                                    if (uploadStatus)
                                                                    {
                                                                        logMessage = $"Successfully uploaded passport details in HCM for PersonNumber : {colleagueViewModelData.PersonNumber} and Document type : {docSetRTW.documentType.name}";
                                                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchCompleted);
                                                                        Logging.LogInformation(_logger, logMessage);

                                                                    }
                                                                    else
                                                                    {
                                                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchFailed);
                                                                        continue;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                logMessage = $"Failed to read document images from RTW API for PersonNumber : {colleagueViewModelData.PersonNumber}, Status Code : {restDocImageResponse.StatusCode}";
                                                                await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.PassportPatchFailed);
                                                                await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            logMessage = $"No document images available for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                                            await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                                            await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    logMessage = $"No PersonNumber found to update details in HCM ";
                                                    await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                                    await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                                }
                                            }
                                            else
                                            {
                                                logMessage = $"Failed to retrieve Person Documents from RTW API for PersonNumber : {colleagueViewModelData.PersonNumber}, Status Code : {restDocumentResponse.StatusCode}";
                                                await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                                await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        logMessage = $"Failed to retrieve Person Detail from RTW API for PersonNumber : {colleagueViewModelData.PersonNumber}, Status Code : {restPersonResponse.StatusCode}";
                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                        await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber, colleagueViewModelData.ClientId);
                                    }
                                }
                            }
                            var delayinMins = 2;
                            logMessage = $"'Passport image update Completed' - records(Count : {colleagueList.Count()}) found in the RTW database and processed. Next run in {delayinMins} minutes.";
                            Logging.LogInformation(_logger, logMessage);
                            await Task.Delay(TimeSpan.FromMinutes(delayinMins), stoppingToken);
                        }
                        else
                        {
                            var delayinMins = 2;
                            logMessage = $"'Passport image update Completed' - data not found in the RTW database. Next run in {delayinMins} minutes.";
                            Logging.LogInformation(_logger, logMessage);
                            await Task.Delay(TimeSpan.FromMinutes(delayinMins), stoppingToken);
                        }
                    }
                    else
                    {
                        var delayinMins = 2;
                        logMessage = $"'Passport image update Completed' - data not found in the RTW database. Next run in {delayinMins} minutes.";
                        Logging.LogInformation(_logger, logMessage);
                        await Task.Delay(TimeSpan.FromMinutes(delayinMins), stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
                }
            }
        }

        private async Task RecordInboundErrorLogAsync(string errorDescription, IncidentErrorDescription incidentErrorDescription, string personNumber, string clientId)
        {
            Logging.LogError(_logger, errorDescription);
            var errorLogViewModel = new InboundErrorLogViewModel
            {
                Description = errorDescription,
                ErrorType = incidentErrorDescription,
                PersonNumber = personNumber,
                ClientId = clientId,
                Logged = DateTime.UtcNow
            };

            await _inboundErrorService.AddErrorLogAsync(errorLogViewModel);
        }
     
        private async Task RecordInboundErrorLogAsync(string errorDescription, IncidentErrorDescription incidentErrorDescription, string personNumber)
        {
            await RecordInboundErrorLogAsync(errorDescription, incidentErrorDescription, personNumber, null);
        }

        private async Task UpdateColleagueStatusAsync(IColleagueService colleagueService, ColleagueViewModel colleagueViewModelData, Stages stage)
        {
            colleagueViewModelData.StageId = stage;
            colleagueViewModelData.LastUpdateOn = DateTime.UtcNow;
            await colleagueService.UpdateColleagueAsync(colleagueViewModelData);
        }
             
        private DocumentTypeViewModel GetDocumentType(Document docSetRTW, string personNumber)
        {
            var documentType = documentTypeViewModel.Where(x => x.RTWDocumentName == docSetRTW.documentType.name).FirstOrDefault();
            if (documentType == null)
            {
                var logMessage = $"Invalid RTW Document Name found in RTW for PersonNumber : {personNumber} , Document Name : {docSetRTW.documentType.name}";
                RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, personNumber).GetAwaiter().GetResult();
            }
            return documentType;
        }

        private bool GenerateHCMDocRecordModel(Document docSetRTW, Image docImageRTW, string PersonNumber, HCMDocumentRecord hcmDocRec, int docRecordCount)
        {
            if (docImageRTW != null)
            {
                DocumentTypeViewModel documentType = GetDocumentType(docSetRTW, PersonNumber);
                if (documentType == null)
                {
                    return false;
                }
                hcmDocRec.PersonNumber = PersonNumber;
                hcmDocRec.DocumentCode = "RTW_" + Guid.NewGuid();
                hcmDocRec.DocumentNumber = docRecordCount.ToString();
                //Either DocumentType or DocumentTypeId should be mapped in the payload.
                if (_generalSettings.DocumentOfRecordsReferenceField == "DocumentTypeId")
                {
                    hcmDocRec.DocumentTypeId = documentType.HCMDocumentTypeId;
                }
                if (_generalSettings.DocumentOfRecordsReferenceField == "DocumentType")
                {
                    hcmDocRec.DocumentType = documentType.HCMDocumentName;
                }
                hcmDocRec.attachments = new List<Attachment>();
                //to get document extension
                var docExtension = docImageRTW.mimeType.Split('/').Length > 1 ? docImageRTW.mimeType.Split('/')[1] : "";
                var documentFullName = docSetRTW.documentType.pageDocumentTypes.FirstOrDefault().page.name + "." + docExtension;
                var imageContent = docImageRTW.imageData;
                hcmDocRec.attachments.Add(new Attachment(documentFullName, imageContent));
                return true;
            }
            return false;
        }

        private async Task<HCMColleagueDetails> GetHCMWorkerAsync(HCMUpdateData hcmUpdateData, Person personDataRTW)
        {
            // Get the details for this Person Id.
            var hcmServerHost = _hcmSettings.HcmServerHost;
            var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var hcmDetailPersonUrl = "";

            if (!String.IsNullOrEmpty(hcmUpdateData.PersonNumber))
            {
                hcmDetailPersonUrl =
                    $"{hcmServerHost}/hcmRestApi/resources/latest/workers?q=PersonNumber={hcmUpdateData.PersonNumber}&effectiveDate={currentDate}&onlyData=false&fields=PersonNumber";
            }
            else
            {
                //TODO- to add additional parameters to validate the colleague. 
                var empNumberRTW = personDataRTW.personDetails.customField2;
                hcmDetailPersonUrl =
                               $"{hcmServerHost}/hcmRestApi/resources/latest/workers?q=PersonNumber={empNumberRTW}&&effectiveDate={currentDate}&onlyData=false&fields=PersonNumber";
            }
            var restResponse = await _jwTokenService.GetResponseWithJWTokenAsync(hcmDetailPersonUrl, CacheConstants.CloudApplicationCertificate);
            if (restResponse.IsSuccessful)
            {
                try
                {
                    var result = JsonHelper.ToClass<HCMColleagueDetails>(restResponse.Content);
                    if (result.count == 0)
                    {
                        var logMessage = $"Failed to retrieve HCM URLs for PersonNumber : {hcmUpdateData.PersonNumber}, Error details : Person not found";
                        await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.HCMUpdateError, hcmUpdateData.PersonNumber);
                        return null;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    var error  = new Exception($"Error occurred while retrieving HCM URLs for PersonNumber : {hcmUpdateData.PersonNumber}; Message : {ex.Message}; Response : {restResponse.Content}");
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, error, IncidentErrorDescription.HCMUpdateError); 
                    return null;
                }
            }
            else
            {
                var logMessage = $"Error occurred while retrieving HCM URLs for PersonNumber : {hcmUpdateData.PersonNumber}, Return Data : {restResponse.Content}";
                await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.HCMUpdateError, hcmUpdateData.PersonNumber);
                
                var error = new Exception(logMessage);
                await Logging.LogExceptionAsync(_serviceProvider, _logger, error, IncidentErrorDescription.HCMUpdateError);
                
                return null;
            }
        }
         
        private async Task<bool> UploadHCMDocRecordAsync(Image docImageRTW, HCMDocumentRecord hcmDocRec, string rtwDocumentName)
        {
            // Get the details for this Person Id.
            var hcmServerHost = _hcmSettings.HcmServerHost;
            var hcmDocRecordUrl = $"{hcmServerHost}/hcmRestApi/resources/latest/documentRecords";
            var payload = JsonHelper.FromClass(hcmDocRec);
            //var payloadContentType = docImageRTW.mimeType; not required.

            var restResponse = await _jwTokenService.PostDataWithJWTokenAsync(hcmDocRecordUrl,
                CacheConstants.CloudApplicationCertificate, payload);

            if (!restResponse.IsSuccessful)
            {
                var logMessage = $"Failed to upload document for PersonNumber : {hcmDocRec.PersonNumber}, Document type : {rtwDocumentName}. Error details : {restResponse.Content}";
                await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.HCMUpdateError, hcmDocRec.PersonNumber.ToString());
            }
            return restResponse.IsSuccessful;
        }
    }
}