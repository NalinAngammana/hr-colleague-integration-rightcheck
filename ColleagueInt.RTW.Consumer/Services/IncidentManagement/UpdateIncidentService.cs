
using CloudColleaguePublisher.Core;
using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Core.ServiceNow.Contracts;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Consumer.Services.IncidentManagement
{
    
    public class UpdateIncidentService : BackgroundService
    {
        private readonly ILogger<UpdateIncidentService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly GeneralSettings _generalSettings;

        public UpdateIncidentService(
            ILogger<UpdateIncidentService> logger,
            IOptions<GeneralSettings> settings,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _generalSettings = settings.Value;
            _serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var updateIncidentStatusInterval = _generalSettings.UpdateIncidentStatusInterval;

                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var incidentService = scope.ServiceProvider.GetRequiredService<IIncidentService>();
                        var snowIncidentService = scope.ServiceProvider.GetRequiredService<ISnowIncidentService>();

                        var allActiveIncidents = await incidentService.GetAllActiveIncidentsAsync();
                        Logging.LogInformation(_logger, $"Found {allActiveIncidents.Count()} (Active)Not-Resolved incidents from database.");
                        foreach (var activeIncident in allActiveIncidents)
                        {
                            var status = await snowIncidentService.GetIncidentStatusAsync(activeIncident);

                            if (activeIncident.Status.ToString() == status)
                                continue;

                            var incidentStatus = status.GetEnumValueFromDescription<IncidentStatus>();
                            if (incidentStatus != IncidentStatus.None)
                            {
                                await incidentService.UpdateIncidentAsync(activeIncident.Id, incidentStatus);
                                Logging.LogInformation(_logger, $"Incident #{activeIncident.Number} is updated with {incidentStatus} status from ServiceNow in database.");
                            }
                        }
                    }
                    Logging.LogInformation(_logger, $"Update Incident service completed and will be on hold for {updateIncidentStatusInterval} minutes until next run.");
                    await Task.Delay(updateIncidentStatusInterval, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
            }
        }
    }
}