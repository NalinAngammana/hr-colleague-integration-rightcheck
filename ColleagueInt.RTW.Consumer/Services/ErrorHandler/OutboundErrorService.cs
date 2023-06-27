using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ColleagueInt.RTW.Database.Constants;

namespace ColleagueInt.RTW.Consumer.Services.ErrorHandler
{
    public class OutboundErrorService : BackgroundService
    {

        private readonly ILogger<OutboundErrorService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly GeneralSettings _generalSettings;

        private static int _apiErrorRecordInterval;
        private static int _apiErrorRecordLimit;

        public OutboundErrorService(
                        ILogger<OutboundErrorService> logger,
                        IOptions<GeneralSettings> generalSettings,
                        IServiceProvider serviceProvider)

        {
            _logger = logger;
            _generalSettings = generalSettings.Value;
            _serviceProvider = serviceProvider;
        }


        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    _apiErrorRecordInterval = _generalSettings.APIErrorRecordInterval;
                    _apiErrorRecordLimit = _generalSettings.APIErrorRecordLimit;

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
                    var colleagueService = scope.ServiceProvider.GetRequiredService<IColleagueService>();
                    var incidentService = scope.ServiceProvider.GetRequiredService<IIncidentService>();

                    var failedColleagueList = await colleagueService.GetLatestCheckRequestFailedListAsync();

                    if (_apiErrorRecordLimit <= failedColleagueList.Count())
                    {
                        StringBuilder sb = new StringBuilder();

                        var incidentDetails = await incidentService.GetIncidentDetailsAsync(IncidentErrorDescription.RTWAPIOutboundError);
                        var incidentErrorDescription = IncidentErrorDescription.RTWAPIOutboundError;

                        sb.AppendLine($"Unable to post following new starter details into right to work API ");
                        sb.AppendLine("List of Person Numbers:");
                        foreach (var failedColleague in failedColleagueList)
                        {
                            sb.AppendLine($"{failedColleague.PersonNumber} - {failedColleague.ErrorLog}");
                        }
                                                
                        incidentDetails.ServiceNowDescription = sb.ToString();
                        Logging.LogInformation(_logger, $"Incident raised for the RTW API post data failed collegues");
                        if (incidentDetails.ServiceNowDescription.Length >= 4000)
                        {
                            Logging.LogInformation(_logger, $"IncidentDetails description has more than 4000 characters: {incidentDetails.ServiceNowDescription}.");
                        }

                        await incidentService.AddIncidentAsync(incidentErrorDescription, incidentDetails.ServiceNowDescription, incidentDetails.Type.ToString());
                    }
                }
                catch (Exception ex)
                {
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
                }

                await Task.Delay(TimeSpan.FromHours(_apiErrorRecordInterval), stoppingToken);
            }
        }
    }
}
