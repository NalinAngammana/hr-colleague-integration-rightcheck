
namespace ColleagueInt.RTW.Core.RestApiService.Contracts
{
    using RestSharp;
    using System.Threading.Tasks;

    public interface IJwTokenService
    {
        public string GenerateJWTTokenAsync(string certificateName, string issuer, string subject, string cacheName);
        public string GetJWTTokenFromCache(string cacheName);
        public Task<IRestResponse> GetResponseWithJWTokenAsync(string url, string tokenCache);
        public Task<IRestResponse> PostDataWithJWTokenAsync(string url, string tokenCache, string jsonString);
        public Task<IRestResponse> PostDataWithJWTokenAsync(string url, string tokenCache, string payload, string contentType);
        public Task<IRestResponse> PatchDataWithJWTokenAsync(string url, string tokenCache, string jsonString, string effectiveOfHeader);
        public Task<IRestResponse> GetSoapResponseWithJWTokenAsync(string url, string tokenCache, string soapPayload, string soapAction);
    }
}
