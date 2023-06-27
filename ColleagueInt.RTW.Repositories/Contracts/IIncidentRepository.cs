using ColleagueInt.RTW.Database.Entities;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Repositories.Contracts
{
	public interface IIncidentRepository : IBaseRepository<Incident>
	{
		Task<IncidentDetail> GetIncidentDetailsAsync(string errorCode);
		Task<Incident> GetLastRecordedIncidentByErrorCodeAsync(string errorCode);
	}
}