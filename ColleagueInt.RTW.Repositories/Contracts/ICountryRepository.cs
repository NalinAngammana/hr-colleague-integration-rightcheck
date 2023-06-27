using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Repositories.Contracts
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
        Task<IEnumerable<Country>> GetAllCountryAsync();
    }
}
