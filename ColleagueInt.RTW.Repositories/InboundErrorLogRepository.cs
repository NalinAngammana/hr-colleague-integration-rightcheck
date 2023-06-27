using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Repositories
{
    public class InboundErrorLogRepository : BaseRepository<InboundErrorLog>, IInboundErrorLogRepository
    {
        private new readonly RTWContext _context;

        public InboundErrorLogRepository(RTWContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<InboundErrorLog>> GetLatestErrorLogByErrorTypeAsync(IncidentErrorDescription errorType, DateTime? lastIncidentRecordedDate)
        {
            var failedColleagues = await _context.InboundErrorLog.Where(x => x.ErrorType == errorType &&
                                                              (x.Logged  > lastIncidentRecordedDate || lastIncidentRecordedDate == null)).ToListAsync();
            return failedColleagues;
        }

    }
}
