using RestSharp;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Core.RestApiService.Contracts
{
    public interface IAzureApiService
    {
        Task<IRestResponse> PostResponseAsync(string url, string jsonDataString, Method method = Method.POST);
        Task<IRestResponse> GetResponseAsync(string url, string jsonDataString);
    }
}