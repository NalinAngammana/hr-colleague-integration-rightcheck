namespace ColleagueInt.RTW.Core.RestApiService.Contracts
{
    using RestSharp;
    using System.Threading.Tasks;

    public interface IRestApiService
    {
        public Task<IRestResponse> GetResponseWithOAuthAsync(string cacheName, string url);
        public Task<IRestResponse> GetResponseWithBasicAuthAsync(string url, string userName, string password);
        public Task<IRestResponse> PatchResponseAsync(string cacheName, string url, string jsonString);
        public Task<IRestResponse> PostResponseAsync(string cacheName, string url, string jsonString);
        public Task<IRestResponse> GetSoapResponseWithBasicAuthAsync(string url, string userName, string password, string soapPayload, string soapAction);
    }
}
