using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryViewModel>> GetAllCountryAsync();
    }
}
