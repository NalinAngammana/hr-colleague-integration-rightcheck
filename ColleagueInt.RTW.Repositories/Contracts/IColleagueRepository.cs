using ColleagueInt.RTW.Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Repositories.Contracts
{
    public interface IColleagueRepository : IBaseRepository<Colleague>
    {
        Task<Colleague> GetLastColleagueEntryAsync(string personNumber);
        Task<bool> AbleToCreateCheckRequestForColleagueAsync(string personNumber);
        Task <IEnumerable<Colleague>> GetInitialStageColleagueListAsync(int count);
        Task<IEnumerable<Colleague>> GetLatestCheckRequestFailedListAsync(DateTime? lastIncidentRecordedDate);
        Task <IEnumerable<Colleague>> GetCheckSuccessfullyCompletedColleagueListAsync(int count);
        Task<bool> AbleToCreateDocumentCheckRequestForColleagueAsync(string personName);
        Task<IEnumerable<Colleague>> GetHCMUpdatedColleagueListAsync(int count, DateTime cutOffDate);
    }
}
