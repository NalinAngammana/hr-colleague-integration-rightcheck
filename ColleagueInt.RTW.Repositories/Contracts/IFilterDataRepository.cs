using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Repositories.Contracts
{
    public interface IFilterDataRepository : IBaseRepository<FilterData>
    {
        Task<IEnumerable<FilterData>> GetAllValidFiltersAsync();
    }
}
