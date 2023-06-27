
namespace ColleagueInt.RTW.Consumer.Services.Archive
{
    using ColleagueInt.RTW.Consumer.Configuration;
    using ColleagueInt.RTW.Consumer.Misc;
    using ColleagueInt.RTW.Database.Constants;
    using ColleagueInt.RTW.Services.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CleanUpDataService : BackgroundService
    {
        private readonly ILogger<CleanUpDataService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly GeneralSettings _generalSettings;

        public CleanUpDataService(
            ILogger<CleanUpDataService> logger, 
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
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        Logging.LogInformation(_logger, "CleanUpDataService started. ");
                        var cleanUpDaysValue = _generalSettings.CleanUpAfterDays;
                        if (cleanUpDaysValue == 0)
                            return;
                        Logging.LogInformation(_logger, $"CleanUpDaysValue {cleanUpDaysValue}. ");
                        // Get how many days back data needs to be deleted
                        var cleanupTillDate = DateTime.UtcNow.AddDays(-cleanUpDaysValue);
                        Logging.LogInformation(_logger, $"cleanupTillDate {cleanupTillDate}. ");     

                        // Deletes resolved incidents older than cleanupTillDate Value
                        var incidentService = scope.ServiceProvider.GetRequiredService<IIncidentService>();
                        await incidentService.RemoveIncidentsAsync(cleanupTillDate, IncidentStatus.Resolved);
                        Logging.LogInformation(_logger, $"Incident records older than date: {cleanupTillDate:dd/MM/yyyy HH:mm:ss} are purged.");

                        // Deletes colleague records from Colleague table older than cleanupTillDate Value
                        var colleagueService = scope.ServiceProvider.GetRequiredService<IColleagueService>();
                        await colleagueService.RemoveColleagueDataAsync(cleanupTillDate);
                        Logging.LogInformation(_logger, $"Colleague records older than date: {cleanupTillDate:dd/MM/yyyy HH:mm:ss} are purged.");

                        // Deletes error records from database which are older than cleanupTillDate Value
                        var errorService = scope.ServiceProvider.GetRequiredService<IErrorService>();
                        await errorService.RemoveErrorsAsync(cleanupTillDate);
                        Logging.LogInformation(_logger, $"Error records older than date: {cleanupTillDate:dd/MM/yyyy HH:mm:ss} are purged.");

                        // Deletes inbound error log records from database which are older than cleanupTillDate Value
                        var inboundErrorLogService = scope.ServiceProvider.GetRequiredService<IInboundErrorLogService>();
                        await inboundErrorLogService.RemoveInboundErrorLogsAsync(cleanupTillDate);
                        Logging.LogInformation(_logger, $"Inbound Error Log records older than date: {cleanupTillDate:dd/MM/yyyy HH:mm:ss} are purged.");
                    }
                    Logging.LogInformation(_logger, $"CleanUpDataService completed successfully at {DateTime.UtcNow}; next run after 1 day.");
                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Logging.LogInformation(_logger, $"Exception in CleanDataService. Error Added in Error table.");
               await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
            }
        }
    }
}
