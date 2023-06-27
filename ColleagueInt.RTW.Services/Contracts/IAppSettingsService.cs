using ColleagueInt.RTW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services.Contracts
{
    public interface IAppSettingsService
    {
        Task<IEnumerable<AppSetttingsViewModel>> GetAppSetttingsAsync();
        Task<AppSetttingsViewModel> GetAppSetttingForKeyAsync(string key);
        Task<AppSetttingsViewModel> GetAppSetttingByIdAsync(int Id);
        Task AddAppSetttingAsync(AppSetttingsViewModel appSetttingViewModel);
        Task UpdateAppSetttingAsync(AppSetttingsViewModel appSetttingViewModel);
    }
}
