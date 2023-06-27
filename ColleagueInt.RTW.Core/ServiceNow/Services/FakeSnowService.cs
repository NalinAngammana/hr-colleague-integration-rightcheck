
namespace ColleagueInt.RTW.Core.ServiceNow.Services
{

    using ColleagueInt.RTW.Core.ServiceNow.Contracts;
    using ColleagueInt.RTW.Core.ServiceNow.Data;
    using ColleagueInt.RTW.ViewModels;
    using System;
    using System.Threading.Tasks;

    public class FakeSnowIncidentService : ISnowIncidentService
    {
        private string ServiceNowUrl { get; set; }

        public FakeSnowIncidentService(string serviceNowUrl)
        {
            ServiceNowUrl = serviceNowUrl;
        }

        public Task<string> CreateIncidentAsync(IncidentViewModel jsonData)
        {
            Random random = new Random();
            var number = random.Next(1000000,9999999);
            return Task.FromResult($"DUMMY_INC{number}");
        }

        public Task<IncidentOutput> GetIncidentAsync(IncidentViewModel incidentViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetIncidentStatusAsync(IncidentViewModel incidentViewModel)
        {
            return Task.FromResult("Resolved");
        }
    }
}
