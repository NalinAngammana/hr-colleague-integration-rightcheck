using AutoMapper;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly IAppSettingsRepository _appSettingRepository;
        private IMapper _mapper;

        public AppSettingsService(IAppSettingsRepository appSettingRepository, IMapper mapper)
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppSetttingsViewModel>> GetAppSetttingsAsync()
        {
            var appSettings = await _appSettingRepository.GetAllAsync();
            return _mapper.Map<List<AppSetttingsViewModel>>(appSettings);
        }

        public async Task<AppSetttingsViewModel> GetAppSetttingForKeyAsync(string key)
        {
            var appSetting = await _appSettingRepository.FirstOrDefaultAsync(x => x.Key == key);
            return _mapper.Map<AppSetttingsViewModel>(appSetting);
        }

        public async Task<AppSetttingsViewModel> GetAppSetttingByIdAsync(int Id)
        {
            var appSetting = await _appSettingRepository.GetByIdAsync(Id);
            return _mapper.Map<AppSetttingsViewModel>(appSetting);
        }

        public async Task AddAppSetttingAsync(AppSetttingsViewModel appSetttingViewModel)
        {
            var appSetting = _mapper.Map<AppSettings>(appSetttingViewModel);
            await _appSettingRepository.AddAsync(appSetting);
        }

        public async Task UpdateAppSetttingAsync(AppSetttingsViewModel appSetttingViewModel)
        {
            var appSetting = _mapper.Map<AppSettings>(appSetttingViewModel);
            await _appSettingRepository.UpdateAsync(appSetting, appSetting.Id);
        }
    }
}