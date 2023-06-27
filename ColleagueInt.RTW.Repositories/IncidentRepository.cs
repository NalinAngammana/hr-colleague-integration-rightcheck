using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Repositories
{
	public class IncidentRepository : BaseRepository<Incident>, IIncidentRepository
	{
		private new readonly RTWContext _context;

		public IncidentRepository(RTWContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IncidentDetail> GetIncidentDetailsAsync(string errorCode)
		{
			return await _context.IncidentDetail.FirstOrDefaultAsync(x => x.ErrorCode == errorCode);
		}
		public async Task<Incident> GetLastRecordedIncidentByErrorCodeAsync(string errorCode)
		{
			return await _context.IncidentDetail
								.Join(_context.Incident, de => de.Id, inc => inc.IncidentDetailId, (de, inc) => new { de, inc })
								.Where(r => r.de.ErrorCode == errorCode)
								.OrderBy(r => r.inc.CreationTime)
								.Select(r => r.inc).LastOrDefaultAsync();
		}
	}
}
