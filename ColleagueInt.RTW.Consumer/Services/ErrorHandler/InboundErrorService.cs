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
    public class InboundErrorService : BackgroundService
    {

        private readonly ILogger<OutboundErrorService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly GeneralSettings _generalSettings;
        private IInboundErrorLogService _inboundErrorLogService;
        private IIncidentService _incidentService;

        private static int _apiErrorRecordInterval;
        private static int _apiErrorRecordLimit;

        public InboundErrorService(
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
                    _inboundErrorLogService = scope.ServiceProvider.GetRequiredService<IInboundErrorLogService>();
                    _incidentService = scope.ServiceProvider.GetRequiredService<IIncidentService>();
                    
                    await GenerateIncidents(IncidentErrorDescription.RTWAPIInboundError);
                    
                    await GenerateIncidents(IncidentErrorDescription.HCMUpdateError);

                }
                catch (Exception ex)
                {
                    await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
                }

                await Task.Delay(TimeSpan.FromHours(_apiErrorRecordInterval), stoppingToken);
            }


            async Task GenerateIncidents(IncidentErrorDescription incidentErrorDescription)
            {
                var failedColleagueList = await _inboundErrorLogService.GetLatestErrorLogByErrorTypeAsync(incidentErrorDescription);

                if (failedColleagueList.Any())
                {
                    StringBuilder sb = new StringBuilder();

                    var incidentDetails = await _incidentService.GetIncidentDetailsAsync(incidentErrorDescription);
    
                    sb.AppendLine($"Following warning messages are recorded while updating new starter details in the myHR");
                    foreach (var failedColleague in failedColleagueList)
                    {
                        sb.AppendLine($"{failedColleague.Description}");
                    }

                    incidentDetails.ServiceNowDescription = sb.ToString();
                    Logging.LogInformation(_logger, $"Incident raised for the warning messages are recorded while updating new starter details in the myHR");
                    if (incidentDetails.ServiceNowDescription.Length >= 4000)
                    {
                        Logging.LogInformation(_logger, $"IncidentDetails description has more than 4000 characters: {incidentDetails.ServiceNowDescription}.");
                    }

                    await _incidentService.AddIncidentAsync(incidentErrorDescription, incidentDetails.ServiceNowDescription, incidentDetails.Type.ToString());
                }
            }
        }
    }
}
