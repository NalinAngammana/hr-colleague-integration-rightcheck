namespace ColleagueInt.RTW.Core.RestApiService
{
    using ColleagueInt.RTW.Core.RestApiService.Contracts;
    using RestSharp;
    using System;
    using System.Collections.Concurrent;
    using System.Net;
    using System.Threading.Tasks;

    public class RestApiService : IRestApiService
    {
        private static readonly ConcurrentDictionary<string, Token> TokenCache =
            new ConcurrentDictionary<string, Token>();

        private readonly string _authorisationTokenUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public RestApiService()
        {

        }

        public RestApiService(string cacheName,
            string authorisationTokenUrl,
            string clientId,
            string clientSecret)
        {
            _authorisationTokenUrl = authorisationTokenUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
            var tokenValue = GetAccessTokenAsync(cacheName).GetAwaiter().GetResult();
            TokenCache.TryAdd(cacheName, tokenValue);
        }

        private Token GetTokenFromCache(string cacheName)
        {
            if (!TokenCache.ContainsKey(cacheName))
                return null;

            var token = TokenCache[cacheName];
            if (token.Expired())
            {
                Console.WriteLine($"Token from {cacheName} Expired is {token.Expired()} and going to be renewed.");
                TokenCache.TryRemove(cacheName, out _);
            }
            else
            {
                return TokenCache[cacheName];
            }

            return null;
        }

        private async Task<Token> GetAccessTokenAsync(string cacheName)
        {
            var cacheToken = GetTokenFromCache(cacheName);
            if (cacheToken != null)
                return cacheToken;

            var client = new RestClient(_authorisationTokenUrl);
            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded",
                "grant_type=client_credentials&scope=all&client_id=" + _clientId + "&client_secret=" + _clientSecret, ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            var accessToken = JsonHelper.ToClass<Token>(response.Content);
            accessToken.GeneratedAt = DateTime.Now.ToUniversalTime();
            return accessToken;
        }

        public async Task<IRestResponse> GetResponseWithOAuthAsync(string cacheName, string url)
        {
            var accessToken = await GetAccessTokenAsync(cacheName);

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", "Bearer " + accessToken.AccessToken);
            request.AddHeader("cache-control", "no-cache");

            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> GetResponseWithBasicAuthAsync(string url, string userName, string password)
        {
            var client = new RestClient(url) { Timeout = 30000 };

            var request = new RestRequest(Method.GET) { Credentials = new NetworkCredential(userName, password) };
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> PatchResponseAsync(string cacheName, string url, string jsonString)
        {
            var accessToken = await GetAccessTokenAsync(cacheName);

            var client = new RestClient(url) { Timeout = 30000 };
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("authorization", "Bearer " + accessToken.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> PostResponseAsync(string cacheName, string url, string jsonString)
        {
            var accessToken = await GetAccessTokenAsync(cacheName);

            var client = new RestClient(url) { Timeout = 30000 };

            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "Bearer " + accessToken.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> GetSoapResponseWithBasicAuthAsync(string url, string userName, string password,
            string soapPayload, string soapAction)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST) { Credentials = new NetworkCredential(userName, password) };

            request.AddHeader("Content-Type", "application/soap+xml");
            request.AddHeader("soapAction", soapAction);
            request.AddHeader("soapVersion", "1.1");
            request.AddParameter("application/soap+xml", soapPayload, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }
    }
}
