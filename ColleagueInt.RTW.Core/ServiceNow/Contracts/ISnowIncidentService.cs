
namespace ColleagueInt.RTW.Core.ServiceNow.Contracts
{
    using ColleagueInt.RTW.Core.ServiceNow.Data;
    using ColleagueInt.RTW.ViewModels;
    using System.Threading.Tasks;

    public interface ISnowIncidentService
    {
        Task<string> CreateIncidentAsync(IncidentViewModel incidentViewModel);
        Task<IncidentOutput> GetIncidentAsync(IncidentViewModel incidentViewModel);
        Task<string> GetIncidentStatusAsync(IncidentViewModel incidentViewModel);
    }
}
