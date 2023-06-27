using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;

namespace ColleagueInt.RTW.Repositories
{
    public class AppSettingsRepository : BaseRepository<AppSettings>, IAppSettingsRepository
    {
        private new readonly RTWContext _context;

        public AppSettingsRepository(RTWContext context) : base(context)
        {
            _context = context;
        }
    }
}
