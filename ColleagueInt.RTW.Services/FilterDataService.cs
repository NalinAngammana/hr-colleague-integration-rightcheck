using AutoMapper;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
    public class FilterDataService : IFilterDataService
    {
        private readonly IMapper _mapper;
        private readonly IFilterDataRepository _filterDataRepository;
        
        public FilterDataService(IMapper mapper, IFilterDataRepository filterDataRepository)
        {
            _mapper = mapper;
            _filterDataRepository = filterDataRepository;
        }

        public async Task<IEnumerable<FilterDataViewModel>> GetAllValidFiltersAsync()
        {
            var allLookUpData = await _filterDataRepository.GetAllValidFiltersAsync();
            return _mapper.Map<List<FilterDataViewModel>>(allLookUpData);
        }
    }
}
