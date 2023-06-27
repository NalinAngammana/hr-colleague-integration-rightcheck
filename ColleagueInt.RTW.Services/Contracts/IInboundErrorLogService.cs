using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services.Contracts
{
    public interface IInboundErrorLogService
    {
        Task<InboundErrorLog> AddErrorLogAsync(InboundErrorLogViewModel outboundErrorLogViewModel);

        Task<IEnumerable<InboundErrorLog>> GetLatestErrorLogByErrorTypeAsync(IncidentErrorDescription incidentErrorDescription);

        Task RemoveInboundErrorLogsAsync(DateTime tillDateTime);

    }
}
