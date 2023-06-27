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
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private new readonly RTWContext _context;

        public CountryRepository(RTWContext context) : base(context)
        {

            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllCountryAsync()
        {
            return await _context.Country.ToListAsync();
        }

    }
}
