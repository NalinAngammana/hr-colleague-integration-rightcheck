using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ColleagueInt.RTW.Services.Contracts
{
    public interface IColleagueService
    {
        Task<Colleague> AddColleagueAsync(ColleagueViewModel colleagueViewModel);
        Task UpdateColleagueAsync(ColleagueViewModel colleagueViewModel);
        Task<string> GetTrackingReferenceAsync(string personNumber);
        Task<bool> AbleToCreateCheckRequestForColleagueAsync(string personNumber);
        Task<ColleagueViewModel> GetColleagueRecordByTrackingRefAsync(string trackingRef);
        Task<IEnumerable<ColleagueViewModel>> GetInitialStageColleagueListAsync(int count);
        Task<IEnumerable<ColleagueViewModel>> GetLatestCheckRequestFailedListAsync();
        Task<IEnumerable<ColleagueViewModel>> GetSuccessfullyCheckCompletedColleagueListAsync(int count);
        Task RemoveColleagueDataAsync(DateTime tillDateTime);
        Task<bool> AbleToCreateDocumentCheckRequestForColleagueAsync(string personNumber);
        Task<IEnumerable<ColleagueViewModel>> GetHCMUpdatedColleagueListAsync(int count, DateTime cutOffDate);
    }
}
