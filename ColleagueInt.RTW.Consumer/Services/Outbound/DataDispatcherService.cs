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
using RestSharp;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Consumer.Services.Outbound
{
    public class DataDispatcherService : BackgroundService
    {

        private readonly ILogger<DataDispatcherService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly GeneralSettings _generalSettings;
        private readonly RTWSettings _rtwSettings;
        private readonly IAzureApiService _restApiService;

        private static bool _enableDetailedLogging;
        private static string _createRequestURL;

        public DataDispatcherService(
                        ILogger<DataDispatcherService> logger,
                        IOptions<GeneralSettings> generalSettings,
                        IOptions<RTWSettings> rtwSettings,
                        IServiceProvider serviceProvider,
                        IAzureApiService restApiService)
                      
        {
            _logger = logger;
            _generalSettings = generalSettings.Value;
            _rtwSettings = rtwSettings.Value;
            _serviceProvider = serviceProvider;
            _restApiService = restApiService;
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
            while (!(stoppingToken.IsCancellationRequested))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    //Check service downtime flag
                    var appSettingsService = scope.ServiceProvider.GetRequiredService<IAppSettingsService>();
                    var outboundServiceDownTimeFlag = appSettingsService.GetAppSetttingForKeyAsync("HCM-To-RTW-OutboundFlow").GetAwaiter().GetResult();
                    if (bool.Parse(outboundServiceDownTimeFlag.Value))
                    {
                        var now = DateTime.Now;
                        var startTime = appSettingsService.GetAppSetttingForKeyAsync("HCM-To-RTW-OutboundFlow-DownTime-Start").GetAwaiter().GetResult().Value;
                        var endTime = appSettingsService.GetAppSetttingForKeyAsync("HCM-To-RTW-OutboundFlow-DownTime-End").GetAwaiter().GetResult().Value;

                        DateTime downTimeStart = Convert.ToDateTime(startTime, new CultureInfo("en-GB"));
                        DateTime downTimeEnd = Convert.ToDateTime(endTime, new CultureInfo("en-GB"));


                        if (now >= downTimeStart && now <= downTimeEnd )
                        {
                            Logging.LogInformation(_logger, $"Planned downtime for RTW application, the outbound service will be suspended between ServiceDowntime_Start: {startTime} to ServiceDowntime_End: {endTime}");
                            var delayInMins = (downTimeEnd - now).TotalMinutes;

                            Logging.LogInformation(_logger, $"Task delayed for {delayInMins} minutes");
                            await Task.Delay(TimeSpan.FromMinutes(delayInMins), stoppingToken);

                            //Set the flag to false once delay is over
                            outboundServiceDownTimeFlag.Value = "FALSE";
                            await appSettingsService.UpdateAppSetttingAsync(outboundServiceDownTimeFlag);
                        }
                    }


                    //Get the first 100 records
                    var count = 100;
                    string logMessage = "";

                    var colleagueService = scope.ServiceProvider.GetRequiredService<IColleagueService>();
                    var colleagueList = await colleagueService.GetInitialStageColleagueListAsync(count);

                    if (colleagueList.Any())
                    {
                        foreach (var colleagueViewModelData in colleagueList)
                        {
                            var jsonDataString = DataProtector.Decrypt(colleagueViewModelData.JsonData);

                            logMessage = $"RTW check requested for PersonNumber : {colleagueViewModelData.PersonNumber} ";
                            Logging.LogInformation(_logger, logMessage);


                            IRestResponse restResponse = await _restApiService.PostResponseAsync(_createRequestURL, jsonDataString);
                            if (restResponse.IsSuccessful)
                            {
                                UpdateColleagueViewModel(colleagueViewModelData, Stages.CheckRequested, CheckStatus.CheckNotCompleted, "");
                                await colleagueService.UpdateColleagueAsync(colleagueViewModelData);

                                var detailedLog = $"API call successfully completed for PersonNumber : {colleagueViewModelData.PersonNumber}";
                                if (_enableDetailedLogging)
                                    detailedLog = $"{detailedLog}, Response Data : {restResponse.Content}";
                                Logging.LogInformation(_logger, detailedLog);
                            }
                            else
                            {
                                UpdateColleagueViewModel(colleagueViewModelData, Stages.CheckRequestFailed, null, restResponse.Content);
                                await colleagueService.UpdateColleagueAsync(colleagueViewModelData);

                                var detailedLog = $"API call failed for PersonNumber : {colleagueViewModelData.PersonNumber}";
                                if (_enableDetailedLogging)
                                    detailedLog = $"{detailedLog}, Response Data : {restResponse.Content}";
                                
                                var error = new Exception($"{detailedLog}, Error details : {restResponse.Content}");
                                await Logging.LogExceptionAsync(_serviceProvider, _logger, error, IncidentErrorDescription.RTWAPIOutboundError);
                            }
                        }

                        //If records are found in DB - change the delay to 2 mins
                        var delayinMins = 2;
                        logMessage = $"'New Starter' - records(Count : {colleagueList.Count()}) found in the RTW database and processed. Next run in {delayinMins} minutes.";
                        Logging.LogInformation(_logger, logMessage);
                        await Task.Delay(TimeSpan.FromMinutes(delayinMins), stoppingToken);
                    }
                    else
                    {
                        var delayinMins = 10;
                        logMessage = $"'New Starter' - data not found in the RTW database. Next run in {delayinMins} minutes.";
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

        private void UpdateColleagueViewModel(ColleagueViewModel colleagueViewModel, Stages checkRequested, CheckStatus? checkNotCompleted, string errorLog)
        {
            colleagueViewModel.StageId = checkRequested;
            colleagueViewModel.StatusId = checkNotCompleted;
            colleagueViewModel.LastUpdateOn = DateTime.UtcNow;
            colleagueViewModel.ErrorLog = errorLog;
        }
    }
}
