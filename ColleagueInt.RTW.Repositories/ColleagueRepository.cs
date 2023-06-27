using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ColleagueInt.RTW.Database.Constants;
using System;
using System.Collections.Generic;

namespace ColleagueInt.RTW.Repositories
{
    public class ColleagueRepository : BaseRepository<Colleague>, IColleagueRepository
    {
        private new readonly RTWContext _context;

        public ColleagueRepository(RTWContext context) : base(context)
        {

            _context = context;
        }

        public async Task<bool> AbleToCreateCheckRequestForColleagueAsync(string personNumber)
        {
            var colleague = await _context.Colleague.Where(c => c.PersonNumber == personNumber) 
                                        .OrderByDescending(x => x.Id)
                                        .Select(r =>r)
                                        .FirstOrDefaultAsync();

            return (colleague == null ||
                    ((Stages)colleague.StageId == Stages.CheckCompleted && ((CheckStatus)colleague.StatusId == CheckStatus.UserRemovedCheck || (CheckStatus)colleague.StatusId == CheckStatus.SystemRemovedCheck)));
        }

        public async Task<Colleague> GetLastColleagueEntryAsync(string personNumber)
        {
             return await _context.Colleague.Where(x => x.PersonNumber==personNumber)
                                           .OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Colleague>> GetInitialStageColleagueListAsync(int count)
        {
            var nextColleague = await _context.Colleague.Where(x => (Stages)x.StageId == Stages.InitalStage)
                                                        .OrderBy(x => x.StartDate).Take(count).ToListAsync();
            return nextColleague;
        }

        public async Task<IEnumerable<Colleague>> GetLatestCheckRequestFailedListAsync(DateTime? lastIncidentRecordedDate)
        {
            var failedColleagues = await _context.Colleague.Where(x => (Stages)x.StageId == Stages.CheckRequestFailed &&
                                                              (x.LastUpdateOn > lastIncidentRecordedDate || lastIncidentRecordedDate == null)).ToListAsync();
            return failedColleagues;
        }

        public async Task<IEnumerable<Colleague>> GetCheckSuccessfullyCompletedColleagueListAsync(int count)
        {
            var statusIdCheckSuccess =  _context.Status.Where(y => y.StatusName == CheckStatus.Successful.ToString()).Select(y=> y.Id).FirstOrDefault();

            var nextColleague = await _context.Colleague.Where(x => ((Stages)x.StageId == Stages.CheckCompleted) && x.StatusId == statusIdCheckSuccess)
                                                        .OrderByDescending(x => x.LastUpdateOn).Take(count).ToListAsync();
            return nextColleague;
        }

        public async Task<IEnumerable<Colleague>> GetHCMUpdatedColleagueListAsync(int count, DateTime cutOffDate)
        {
            var statusIdCheckSuccess = _context.Status.Where(y => y.StatusName == CheckStatus.Successful.ToString()).Select(y => y.Id).FirstOrDefault();

            var nextColleague = await _context.Colleague.Where(x => ((Stages)x.StageId == Stages.HCMDocUploaded) && x.StatusId == statusIdCheckSuccess && x.LastUpdateOn < cutOffDate)
                                                        .OrderByDescending(x => x.LastUpdateOn).Take(count).ToListAsync();
            return nextColleague;
        }

        public async Task<bool> AbleToCreateDocumentCheckRequestForColleagueAsync(string personNumber)
        {
            var colleague = await _context.Colleague.Where(c => c.PersonNumber == personNumber)
                                        .OrderByDescending(x => x.Id)
                                        .Select(r => r)
                                        .FirstOrDefaultAsync();

            return (colleague == null ||
                    ((Stages)colleague.StageId != Stages.CheckRequested && (Stages)colleague.StageId != Stages.InitalStage));
        }
    }
}
