using AutoMapper;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
    public class CountryService : ICountryService
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;
        
        public CountryService(IMapper mapper, ICountryRepository documentTypeRepository)
        {
            _mapper = mapper;
            _countryRepository = documentTypeRepository;
        }

        public async Task<IEnumerable<CountryViewModel>> GetAllCountryAsync()
        {
            var allContries = await _countryRepository.GetAllCountryAsync();
            return _mapper.Map<List<CountryViewModel>>(allContries);
        }
    }
}
