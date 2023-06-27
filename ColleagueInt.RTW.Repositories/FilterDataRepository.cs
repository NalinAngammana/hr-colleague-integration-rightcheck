using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Repositories.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ColleagueInt.RTW.Repositories
{
    public class FilterDataRepository : BaseRepository<FilterData>, IFilterDataRepository
    {
        private new readonly RTWContext _context;

        public FilterDataRepository(RTWContext context) : base(context)
        {

            _context = context;
        }

        public async Task<IEnumerable<FilterData>> GetAllValidFiltersAsync()
        {
            return await _context.FilterData.ToListAsync();
        }

    }
}
