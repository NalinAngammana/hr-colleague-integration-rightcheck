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

namespace ColleagueInt.RTW.Consumer.Services.Inbound
{
    class DataReaderService : BackgroundService
    {
        private readonly ILogger<DataReaderService> _logger;
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
        private IEnumerable<CountryViewModel> allCountryViewModel;
        private IInboundErrorLogService _inboundErrorService;
        private bool rtwReviewStatus = true;
        private readonly string _defaultHCMEndDate = "4712-12-31";

        public DataReaderService(
                        ILogger<DataReaderService> logger,
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
                    var inboundServiceDownTimeFlag = appSettingsService.GetAppSetttingForKeyAsync("RTW-To-HCM-InboundFlow").GetAwaiter().GetResult();
                    if (bool.Parse(inboundServiceDownTimeFlag.Value))
                    {
                        var now = DateTime.Now;
                        var startTime = appSettingsService.GetAppSetttingForKeyAsync("RTW-To-HCM-InboundFlow-DownTime-Start").GetAwaiter().GetResult().Value;
                        var endTime = appSettingsService.GetAppSetttingForKeyAsync("RTW-To-HCM-InboundFlow-DownTime-End").GetAwaiter().GetResult().Value;

                        DateTime downTimeStart = Convert.ToDateTime(startTime, new CultureInfo("en-GB"));
                        DateTime downTimeEnd = Convert.ToDateTime(endTime, new CultureInfo("en-GB"));


                        if (now >= downTimeStart && now <= downTimeEnd)
                        {
                            Logging.LogInformation(_logger, $"Planned downtime for HCM application, the inbound service will be suspended between ServiceDowntime_Start: {startTime} to ServiceDowntime_End: {endTime}");
                            var delayInMins = (downTimeEnd - now).TotalMinutes;
                            Logging.LogInformation(_logger, $"Task delayed for {delayInMins} minutes");
                            await Task.Delay(TimeSpan.FromMinutes(delayInMins), stoppingToken);

                            //Set the flag to false once delay is over
                            inboundServiceDownTimeFlag.Value = "FALSE";
                            await appSettingsService.UpdateAppSetttingAsync(inboundServiceDownTimeFlag);
                        }
                    }

                    //Get the first 100 records
                    var count = 100;

                    if (allCountryViewModel == null)
                    {
                        var countryService = scope.ServiceProvider.GetRequiredService<ICountryService>();
                        allCountryViewModel = await countryService.GetAllCountryAsync();
                    }

                    var colleagueService = scope.ServiceProvider.GetRequiredService<IColleagueService>();
                    var colleagueList = await colleagueService.GetSuccessfullyCheckCompletedColleagueListAsync(count);
                    _inboundErrorService = scope.ServiceProvider.GetRequiredService<IInboundErrorLogService>();

                    var logMessage = "";

                    if (colleagueList.Any())
                    {

                        var documentTypeService = scope.ServiceProvider.GetRequiredService<IDocumentTypeService>();
                        documentTypeViewModel = await documentTypeService.GetAllDocumentTypesAsync();

                        foreach (var colleagueViewModelData in colleagueList)
                        {
                            var docRecordCount = 0;
                            rtwReviewStatus = true;

                            if (colleagueViewModelData.ClientId == null)
                            {
                                logMessage = $"ClientId not available for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
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
                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
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
                                            await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMUpdateFailed);
                                            continue;
                                        }

                                        var result = UpdateRTWCitizenshipsInfoInHCM(hcmWorker, personDataRTW, hcmData);
                                        if (!result)
                                        {
                                            await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMUpdateFailed);
                                        }
                                        var getPersonDocSetURL = _getPersonDocSetBaseURL.Replace("{personId}", colleagueViewModelData.ClientId);
                                        IRestResponse restDocumentResponse = await _restApiService.GetResponseAsync(getPersonDocSetURL, null);
                                        if (restDocumentResponse.IsSuccessful)
                                        {
                                            var currentDocSet = JsonConvert.DeserializeObject<CurrentDocumentSet>(restDocumentResponse.Content);
                                            if (!(String.IsNullOrEmpty(hcmData.PersonNumber) && String.IsNullOrEmpty(employeeNumber)))
                                            {

                                                foreach (var docSetRTW in currentDocSet.documents)
                                                {
                                                    result = UpdateRTWInfoInHCM(hcmWorker, personDataRTW, docSetRTW, hcmData);
                                                    if (!result)
                                                    {
                                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMUpdateFailed);
                                                    }
                                                    else
                                                    {
                                                        logMessage = $"Successfully updated document details in HCM for PersonNumber : {colleagueViewModelData.PersonNumber} and document type : {docSetRTW.documentType.name}";
                                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMUpdated);
                                                        Logging.LogInformation(_logger, logMessage);
                                                    }

                                                    if (!String.IsNullOrWhiteSpace(docSetRTW.documentImages))
                                                    {
                                                        IRestResponse restDocImageResponse = await _restApiService.GetResponseAsync(docSetRTW.documentImages, null);
                                                        if (restDocImageResponse.IsSuccessful)
                                                        {
                                                            HCMDocumentRecord hcmDocRec = new HCMDocumentRecord();
                                                            var docImageRTWList = JsonConvert.DeserializeObject<List<Image>>(restDocImageResponse.Content).ToList();

                                                            foreach (Image docImageRTW in docImageRTWList)
                                                            {
                                                                docRecordCount++;
                                                                result = GenerateHCMDocRecordModel(docSetRTW, docImageRTW, hcmData.PersonNumber, hcmDocRec, docRecordCount);
                                                                if (!result)
                                                                {
                                                                    logMessage = $"Error occurred while generating document model for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                                                    await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMDocUploadFailed);
                                                                    await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.HCMUpdateError, colleagueViewModelData.PersonNumber);

                                                                    //Update review status as 'MyHR-Update Failed' in RTW app
                                                                    rtwReviewStatus = false;
                                                                    UpdateRTWReviewStatus(colleagueViewModelData.ClientId, colleagueViewModelData.PersonNumber);
                                                                    continue;
                                                                }

                                                                var uploadStatus = UploadHCMDocRecordAsync(docImageRTW, hcmDocRec, docSetRTW.documentType.name).GetAwaiter().GetResult();
                                                                if (uploadStatus)
                                                                {
                                                                    logMessage = $"Successfully uploaded document in HCM for PersonNumber : {colleagueViewModelData.PersonNumber} and Document type : {docSetRTW.documentType.name}";
                                                                    await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMDocUploaded);
                                                                    Logging.LogInformation(_logger, logMessage);

                                                                }
                                                                else
                                                                {
                                                                    await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.HCMDocUploadFailed);

                                                                    //Update review status as 'MyHR-Update Failed' in RTW app
                                                                    rtwReviewStatus = false;
                                                                    UpdateRTWReviewStatus(colleagueViewModelData.ClientId, colleagueViewModelData.PersonNumber);
                                                                    continue;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            logMessage = $"Failed to read document images from RTW API for PersonNumber : {colleagueViewModelData.PersonNumber}, Status Code : {restDocImageResponse.StatusCode}";
                                                            await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                                            await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                                            //Update review status as 'MyHR-Update Failed' in RTW app
                                                            rtwReviewStatus = false;
                                                            UpdateRTWReviewStatus(colleagueViewModelData.ClientId, colleagueViewModelData.PersonNumber);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        logMessage = $"No document images available for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                                                        await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                                        await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);
                                                        //Update review status as 'MyHR-Update Failed' in RTW app
                                                        rtwReviewStatus = false;
                                                        UpdateRTWReviewStatus(colleagueViewModelData.ClientId, colleagueViewModelData.PersonNumber);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                logMessage = $"No PersonNumber found to update details in HCM ";
                                                await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                                await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);

                                                //Update review status as 'MyHR-Update Failed' in RTW app
                                                rtwReviewStatus = false;
                                                UpdateRTWReviewStatus(colleagueViewModelData.ClientId, colleagueViewModelData.PersonNumber);
                                            }
                                        }
                                        else
                                        {
                                            logMessage = $"Failed to retrieve Person Documents from RTW API for PersonNumber : {colleagueViewModelData.PersonNumber}, Status Code : {restDocumentResponse.StatusCode}";
                                            await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                            await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber);

                                            //Update review status as 'MyHR-Update Failed' in RTW app
                                            rtwReviewStatus = false;
                                            UpdateRTWReviewStatus(colleagueViewModelData.ClientId, colleagueViewModelData.PersonNumber);
                                        }
                                    }
                                }
                                else
                                {
                                    logMessage = $"Failed to retrieve Person Detail from RTW API for PersonNumber : {colleagueViewModelData.PersonNumber}, Status Code : {restPersonResponse.StatusCode}";
                                    await UpdateColleagueStatusAsync(colleagueService, colleagueViewModelData, Stages.ReadDataFailed);
                                    await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, colleagueViewModelData.PersonNumber, colleagueViewModelData.ClientId);

                                    //Update review status as 'MyHR-Update Failed' in RTW app
                                    rtwReviewStatus = false;
                                    UpdateRTWReviewStatus(colleagueViewModelData.ClientId, colleagueViewModelData.PersonNumber);
                                }
                            }
                        }
                        
                        //If records are found in DB - change the delay to 2 mins
                        var delayinMins = 2;
                        logMessage = $"'Check Completed' - records(Count : {colleagueList.Count()}) found in the RTW database and processed. Next run in {delayinMins} minutes.";
                        Logging.LogInformation(_logger, logMessage);
                        await Task.Delay(TimeSpan.FromMinutes(delayinMins), stoppingToken);
                    }
                    else
                    {
                        var delayinMins = 10;
                        logMessage = $"'Check Completed' - data not found in the RTW database. Next run in {delayinMins} minutes.";
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

        private bool UpdateRTWCitizenshipsInfoInHCM(HCMColleagueDetails hcmWorker, Person personDataRTW, HCMUpdateData hcmData)
        {
            var result = false;

            var countryCode = GetCountryCode(personDataRTW.personDetails.nationality, hcmData.PersonNumber);
            if (countryCode != null)
            {
                if (personDataRTW.personDetails.nationality != null)
                {
                    HCMCitizenship citizenship = new HCMCitizenship
                    {
                        Citizenship = countryCode,
                        CitizenshipStatus = "A", // Status lookup in HCM  A - Active, E- Expired.
                        FromDate = ShortDate(personDataRTW.personDetails.createdDate),
                        ToDate = _defaultHCMEndDate
                    };

                    var payload = JsonHelper.FromClass(citizenship);
                    result = UpdateHCMDataAsync("citizenships", hcmWorker, payload).GetAwaiter().GetResult();
                }
            }
            return result;
        }

        private bool UpdateRTWInfoInHCM(HCMColleagueDetails hcmWorker, Person personDataRTW, Document docSetRTW, HCMUpdateData hcmData)
        {
            var result = false;
            DocumentTypeViewModel documentType = GetDocumentType(docSetRTW, hcmData.PersonNumber);
            if (documentType != null)
            {
                if (documentType.DocumentSection == DocumentSection.Visa || documentType.DocumentSection == DocumentSection.Passport)
                {
                    var issuingCountry = docSetRTW.documentKeyValuePairs.Where(x => x.Key == "Issuing State Name").Select(x => x.Value).FirstOrDefault();
                    var countryCode = GetCountryCode(issuingCountry, hcmData.PersonNumber);

                    if (documentType.DocumentSection == DocumentSection.Passport)
                    {
                        HCMPassports passports = new HCMPassports
                        {
                            IssuingCountry = countryCode,
                            PassportNumber = docSetRTW.documentKeyValuePairs.Where(x => x.Key == "Document Number").Select(x => x.Value).FirstOrDefault(),
                            IssuingAuthority = docSetRTW.documentKeyValuePairs.Where(x => x.Key == "Issuing State Name").Select(x => x.Value).FirstOrDefault(),
                            IssueDate = ShortDate(docSetRTW.createdDate),
                            ExpirationDate = GetValidExpiryDate(docSetRTW.createdDate, docSetRTW.documentKeyValuePairs.Where(x => x.Key == "Expiration Date").Select(x => x.Value).FirstOrDefault())
                        };

                        var payload = JsonHelper.FromClass(passports);
                        result = UpdateHCMDataAsync("passports", hcmWorker, payload, documentType).GetAwaiter().GetResult();

                    }

                    else if (documentType.DocumentSection == DocumentSection.Visa)
                    {
                        HCMVisas visa = new HCMVisas
                        {
                            VisaPermitType = documentType.HCMVisaPermitTypeCode,
                            //VisaPermitCategory -- not required
                            VisaPermitNumber = docSetRTW.documentKeyValuePairs.Where(x => x.Key == "Document Number").Select(x => x.Value).FirstOrDefault(),
                            IssuingAuthority = docSetRTW.documentKeyValuePairs.Where(x => x.Key == "Issuing State Name").Select(x => x.Value).FirstOrDefault(),
                            IssuingCountry = countryCode,
                            ExpirationDate = docSetRTW.documentKeyValuePairs.Where(x => x.Key == "Expiration Date").Select(x => x.Value).FirstOrDefault(),
                            //EffectiveStartDate = ShortDate(DateTime.UtcNow),
                            IssueDate = ShortDate(docSetRTW.createdDate),
                            EntryDate = ShortDate(docSetRTW.createdDate),
                            VisaPermitStatus = "A"
                        };
                        var payload = JsonHelper.FromClass(visa);
                        result = UpdateHCMDataAsync("visasPermits", hcmWorker, payload, documentType).GetAwaiter().GetResult();
                    }
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        private string GetCountryCode(string RTWCountry, string personNumber)
        {
            var countryCode = allCountryViewModel.Where(x => x.RTWCountryCode == RTWCountry ||
                                                             x.RTWCountryName == RTWCountry).Select(x => x.HCMCountryCode).FirstOrDefault();
            return countryCode;
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

        private async Task<bool> UpdateHCMDataAsync(string hcmResourceType, HCMColleagueDetails hcmWorker, string payload, DocumentTypeViewModel documentTypeViewModel = null)
        {
            var hcmWorkerLink = hcmWorker.items[0].links.Where(x => x.name == hcmResourceType && x.rel == "child").Select(x => x.href).FirstOrDefault();

            if (!String.IsNullOrEmpty(hcmWorkerLink))
            {
                var documentType = documentTypeViewModel != null ? $", RTW Document Type : {documentTypeViewModel.RTWDocumentName}, HCM Document Type : {documentTypeViewModel.HCMDocumentName}" : "";

                var postRestResponse = await _jwTokenService.PostDataWithJWTokenAsync(hcmWorkerLink, CacheConstants.CloudApplicationCertificate, payload);
                if (postRestResponse.IsSuccessful)
                {
                    Logging.LogInformation(_logger, $"HCM {hcmResourceType} data updated successfully for PersonNumber : {hcmWorker.items[0].PersonNumber} {documentType}");
                    return true;
                }
                else
                {
                    Logging.LogInformation(_logger, $"Failed to update HCM {hcmResourceType} data for PersonNumber : {hcmWorker.items[0].PersonNumber}{documentType}. Error details : {postRestResponse.Content}");
                    
                    var result = await OverWriteHCMDataAsync(hcmResourceType, hcmWorkerLink, payload, hcmWorker.items[0].PersonNumber.ToString(), documentType);
                    return result;
                }

            }
            return false;
        }

        private async Task<bool> OverWriteHCMDataAsync(string hcmResourceType, string hcmWorkerLink, string payload, string personNumber, string documentTypeDetails)
        {
            if (!String.IsNullOrEmpty(hcmWorkerLink))
            {
                var postRestResponse = await _jwTokenService.GetResponseWithJWTokenAsync(hcmWorkerLink, CacheConstants.CloudApplicationCertificate);
                if (postRestResponse.IsSuccessful)
                {
                    switch (hcmResourceType)
                    {
                        case "citizenships":
                            return await PatchHCMCitizenshipsDataAsync(hcmWorkerLink, payload, personNumber, documentTypeDetails, postRestResponse);
                        case "passports":
                            return await PatchHCMPassportDataAsync(hcmWorkerLink, payload, personNumber, documentTypeDetails, postRestResponse);
                        case "visasPermits":
                            return await PatchHCMVisasPermitDataAsync(hcmWorkerLink, payload, personNumber, documentTypeDetails, postRestResponse);
                        default:
                            return false;
                    }
                }
                else
                {
                    Logging.LogError(_logger, $"Failed to retrieve HCM {hcmResourceType} data for PersonNumber : {personNumber}{documentTypeDetails}. Error details : {postRestResponse.Content}");
                    return false;
                }
            }
            return false;
        }

        private async Task<bool> PatchHCMCitizenshipsDataAsync(string hcmWorkerLink, string payload, string personNumber, string documentTypeDetails, IRestResponse postRestResponse)
        {
            var dataToUpdate = JsonConvert.DeserializeObject<HCMCitizenship>(payload);
            var existingHCMDataList = JsonConvert.DeserializeObject<CitizenshipData>(postRestResponse.Content);
            var matchingHCMData = existingHCMDataList.items.Where(x => x.Citizenship == dataToUpdate.Citizenship).FirstOrDefault();

            if (matchingHCMData != null)
            {
                hcmWorkerLink = matchingHCMData.links.FirstOrDefault().href;
                HCMCitizenship citizenship = new HCMCitizenship
                {
                    Citizenship = dataToUpdate.Citizenship,
                    CitizenshipStatus = dataToUpdate.CitizenshipStatus,
                    FromDate = dataToUpdate.FromDate,
                    ToDate = dataToUpdate.ToDate
                };

                var patchPayload = JsonHelper.FromClass(citizenship);
                return await PatchHCMDataAsync ("citizenships", patchPayload, hcmWorkerLink, personNumber, documentTypeDetails);
            }
            else
            {
                Logging.LogError(_logger, $"Failed to patch HCM citizenships data for PersonNumber : {personNumber}{documentTypeDetails}. " +
                                            $"Error details : Could not find matching citizenships data, Citizenship :{dataToUpdate.Citizenship}");
                return false;
            }
        }

        private async Task<bool> PatchHCMPassportDataAsync(string hcmWorkerLink, string payload, string personNumber, string documentTypeDetails, IRestResponse postRestResponse)
        {
            var dataToUpdate = JsonConvert.DeserializeObject<HCMPassports>(payload);
            var existingHCMDataList = JsonConvert.DeserializeObject<CitizenshipData>(postRestResponse.Content);
            var matchingHCMData = existingHCMDataList.items.Where(x => x.IssuingCountry == dataToUpdate.IssuingCountry && x.PassportNumber == dataToUpdate.PassportNumber).FirstOrDefault();
            
            if (matchingHCMData != null)
            {
                hcmWorkerLink = matchingHCMData.links.FirstOrDefault().href;
                HCMPassports passports = new HCMPassports
                {
                    IssuingCountry = dataToUpdate.IssuingCountry,
                    PassportNumber = dataToUpdate.PassportNumber,
                    IssuingAuthority = dataToUpdate.IssuingAuthority,
                    IssueDate = dataToUpdate.IssueDate,
                    ExpirationDate = dataToUpdate.ExpirationDate
                };

                var patchPayload = JsonHelper.FromClass(passports);
                return await PatchHCMDataAsync("passports", patchPayload, hcmWorkerLink, personNumber, documentTypeDetails);
            }
            else
            {
                Logging.LogError(_logger, $"Failed to patch HCM passports data for PersonNumber : {personNumber}{documentTypeDetails}. " +
                                            $"Error details : Could not find any matching passports data, IssuingCountry :{dataToUpdate.IssuingCountry} and PassportNumber :{dataToUpdate.PassportNumber}");
                return false;
            }
        }
           
        private async Task<bool> PatchHCMVisasPermitDataAsync (string hcmWorkerLink, string payload, string personNumber, string documentTypeDetails, IRestResponse postRestResponse)
        {
            var rangeEndDate = _defaultHCMEndDate;
            var rangeStartDate = ShortDate(DateTime.Now);

            var dataToUpdate = JsonConvert.DeserializeObject<HCMVisas>(payload);
            var existingHCMDataList = JsonConvert.DeserializeObject<CitizenshipData>(postRestResponse.Content);
            var matchingHCMData = existingHCMDataList.items.Where(x => x.VisaPermitType == dataToUpdate.VisaPermitType).FirstOrDefault();
            
            if (matchingHCMData != null)
            {
                hcmWorkerLink = matchingHCMData.links.FirstOrDefault().href;
                HCMVisas visa = new HCMVisas
                {
                    VisaPermitType = dataToUpdate.VisaPermitType,
                    VisaPermitNumber = dataToUpdate.VisaPermitNumber,
                    IssuingAuthority = dataToUpdate.IssuingAuthority,
                    IssuingCountry = dataToUpdate.IssuingCountry,
                    ExpirationDate = dataToUpdate.ExpirationDate,
                    IssueDate = dataToUpdate.IssueDate,
                    EntryDate = dataToUpdate.EntryDate,
                    VisaPermitStatus = dataToUpdate.VisaPermitStatus
                };
                string effectiveOfHeader = $"RangeMode=UPDATE;RangeStartDate={rangeStartDate};RangeEndDate={rangeEndDate}";
                
                var patchPayload = JsonHelper.FromClass(visa);
                return await PatchHCMDataAsync("visasPermits", patchPayload, hcmWorkerLink, personNumber, documentTypeDetails, effectiveOfHeader);
            }
            else
            {
                Logging.LogError(_logger, $"Failed to patch HCM visasPermits data for PersonNumber : {personNumber}{documentTypeDetails}. " +
                                            $"Error details : Could not find any matching visasPermits data, VisaPermitType :{dataToUpdate.VisaPermitType}");
                return false;
            }
        }
        
        private async Task<bool> PatchHCMDataAsync(string hcmResourceType, string payload, string hcmWorkerLink, string personNumber, string documentTypeDetails, string effectiveOfHeader = null)
        {
            if (!String.IsNullOrEmpty(hcmWorkerLink))
            {
                var postRestResponse = await _jwTokenService.PatchDataWithJWTokenAsync(hcmWorkerLink, CacheConstants.CloudApplicationCertificate, payload, effectiveOfHeader);
                if (postRestResponse.IsSuccessful)
                {
                    Logging.LogInformation(_logger, $"HCM {hcmResourceType} data patched successfully for PersonNumber : {personNumber} {documentTypeDetails}");
                }
                else
                {
                    var logMessage = $"Failed to patch HCM {hcmResourceType} data for PersonNumber : {personNumber}{documentTypeDetails}. Error details : {postRestResponse.Content}";
                    await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.HCMUpdateError, personNumber.ToString());
                }

                return postRestResponse.IsSuccessful;
            }

            return false;
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

        private string ShortDate(DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("yyyy-MM-dd") : "";
        }

        private string GetValidExpiryDate(DateTime? issueDate, string expirationDate)
        {
            try
            {
                if (issueDate.HasValue && !string.IsNullOrEmpty(expirationDate))
                {
                    return (DateTime.Parse(expirationDate).AddDays(1) > issueDate ? ShortDate(DateTime.Parse(expirationDate)) : "");
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private async void UpdateRTWReviewStatus(string clientId, string personNumber)
        {
            var getPersonDetailsURL = _getPersonDetailsBaseURL.Replace("{personId}", clientId);

            var rtwColleageStatus = _dataMapper.GenerateRTWColleagueStatusObject(rtwReviewStatus);
            var jsonData = JsonHelper.FromClass(rtwColleageStatus);

            IRestResponse restResponse = await _restApiService.PostResponseAsync(getPersonDetailsURL, jsonData, Method.PUT);
            if (!restResponse.IsSuccessful)
            {
                var logMessage = $"Failed to update review status in RTW portal for ClientID : {clientId}, PersonNumber : {personNumber}";
                await RecordInboundErrorLogAsync(logMessage, IncidentErrorDescription.RTWAPIInboundError, personNumber);
            }
            else
            {
                Logging.LogInformation(_logger, $"Review status updated in RTW portal for for ClientID : {clientId}, PersonNumber : {personNumber}");
            }
        }

    }
}