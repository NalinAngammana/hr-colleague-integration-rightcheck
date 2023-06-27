using System;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Services.Contracts
{
    public interface IErrorService
    {
        Task AddErrorAsync(Exception ex);
        Task RemoveErrorsAsync(DateTime fromDateTime);
    }
}
