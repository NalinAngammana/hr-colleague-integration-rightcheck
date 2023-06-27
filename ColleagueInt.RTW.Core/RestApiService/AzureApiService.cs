using ColleagueInt.RTW.Core.RestApiService.Contracts;
using RestSharp;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Core.RestApiService
{
    public class AzureApiService : IAzureApiService
    {
        private readonly string _subscriptionKey;

        public AzureApiService(string subscriptionKey)
        {
            _subscriptionKey = subscriptionKey;
        }

        public async Task<IRestResponse> PostResponseAsync(string url, string jsonDataString, Method method = Method.POST)
        {
            var client = new RestClient(url) { Timeout = 30000 };

            var request = new RestRequest(method);
            request.AddHeader("Ocp-Apim-Subscription-Key", _subscriptionKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonDataString, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }


        public async Task<IRestResponse> GetResponseAsync(string url, string jsonDataString = null)
        {
            var client = new RestClient(url) { Timeout = 30000 };

            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", _subscriptionKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonDataString, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }
    }
}
