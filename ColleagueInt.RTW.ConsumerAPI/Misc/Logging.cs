
namespace ColleagueInt.RTW.ConsumerAPI.Misc
{
    using ColleagueInt.RTW.Core;
    using ColleagueInt.RTW.Database.Constants;
    using ColleagueInt.RTW.Services.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class Logging
    {
        public static async Task LogExceptionAsync<T>(
            IServiceProvider serviceProvider,
            ILogger<T> logger,
            Exception ex,
            IncidentErrorDescription errorDescription = IncidentErrorDescription.NoIncident,
            bool fileAlso = true)
        {
            logger.LogError(ex.Message);

            if (fileAlso)
            {
                LogFileWriter.WriteToLog(ex.Message);
            }

            // Add Exception in Error Table
            using var scope = serviceProvider.CreateScope();
            var errorHandlingService = scope.ServiceProvider.GetRequiredService<IErrorService>();
            await errorHandlingService.AddErrorAsync(ex);

            // Create an Incident too for this exception
            var incidentService = scope.ServiceProvider.GetRequiredService<IIncidentService>();
            string errorMessageForIncident = $"Exception {ex.Message}{Environment.NewLine} from Service: {ex.StackTrace}";
            await incidentService.AddIncidentAsync(errorDescription, errorMessageForIncident, IncidentType.Application.ToString());
        }

        public static void LogError<T>(ILogger<T> logger, string errorMessage, bool fileAlso = true)
        {
            var now = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:ffff");
            logger.LogError($"{now}: {errorMessage}");

            if (fileAlso)
            {
                LogFileWriter.WriteToLog(errorMessage);
            }
        }

        public static void LogInformation<T>(ILogger<T> logger, string informationMessage, bool fileAlso = true)
        {
            var now = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:ffff");
            logger.LogInformation($"{now}: {informationMessage}");

            if (fileAlso)
            {
                LogFileWriter.WriteToLog(informationMessage);
            }
        }
    }
}
