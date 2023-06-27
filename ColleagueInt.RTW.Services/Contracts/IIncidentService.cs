using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services.Contracts
{
    public interface IIncidentService
    {
        Task AddIncidentAsync(IncidentErrorDescription errorDescription, string errorMessage, string errorType);
        Task<IncidentViewModel> GetIncidentDetailsAsync(IncidentErrorDescription errorReason);
        Task<bool> IncidentExistsAsync(IncidentErrorDescription errorReason);
        Task RemoveIncidentsAsync(DateTime tillDateTime, IncidentStatus status);
        Task<IEnumerable<IncidentViewModel>> GetAllActiveIncidentsAsync();
        Task UpdateIncidentAsync(int id, IncidentStatus status);
    }
}
