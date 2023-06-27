using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;

namespace ColleagueInt.RTW.Repositories
{
    public class ErrorEntryRepository : BaseRepository<Error>, IErrorRepository
    {
        private new readonly RTWContext _context;

        public ErrorEntryRepository(RTWContext context) : base(context)
        {
            _context = context;
        }
    }
}
