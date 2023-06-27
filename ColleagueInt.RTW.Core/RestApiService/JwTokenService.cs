
namespace ColleagueInt.RTW.Core.RestApiService
{
    using ColleagueInt.RTW.Core.RestApiService.Contracts;
    using Microsoft.IdentityModel.Tokens;
    using RestSharp;
    using System;
    using System.Collections.Concurrent;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    public class JwTokenService : IJwTokenService
    {
        private static readonly ConcurrentDictionary<string, string> JwtTokenCache = new ConcurrentDictionary<string, string>();

        public string GenerateJWTTokenAsync(string certificateKey, string issuer, string subject, string cacheName)
        {
            byte[] privateKeyBytes = Convert.FromBase64String(certificateKey);

            X509Certificate2 signingCert = new X509Certificate2
                (privateKeyBytes, "", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.EphemeralKeySet);

            X509SecurityKey privateKey = new X509SecurityKey(signingCert);

            var x5t = privateKey.X5t;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = privateKey.Certificate.NotAfter,
                Issuer = issuer,
                IssuedAt = privateKey.Certificate.NotBefore,
                TokenType = "JWT",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", subject)
                }),
                SigningCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256Signature)
            };

            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            securityToken.Header.Add("x5t", x5t);
            securityToken.Header.Remove("kid");
            securityToken.Payload.Remove("nbf");

            var jwtToken = tokenHandler.WriteToken(securityToken);

            JwtTokenCache.TryAdd(cacheName, jwtToken);
            return jwtToken;
        }

        public string GetJWTTokenFromCache(string cacheName)
        {
            if (JwtTokenCache.ContainsKey(cacheName))
                return JwtTokenCache[cacheName];

            throw new Exception($"{cacheName} not found in cache. Please restart the service.");
        }

        public async Task<IRestResponse> GetResponseWithJWTokenAsync(string url, string tokenCache)
        {
            string jwtToken = GetJWTTokenFromCache(tokenCache);

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("REST-Framework-Version", "2");
            request.AddHeader("Authorization", "Bearer " + jwtToken);
            request.AddHeader("cache-control", "no-cache");

            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> PostDataWithJWTokenAsync(string url, string tokenCache, string jsonString)
        {
            string jwtToken = GetJWTTokenFromCache(tokenCache);

            var client = new RestClient(url) { Timeout = 30000 };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + jwtToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> PatchDataWithJWTokenAsync(string url, string tokenCache, string jsonString, string effectiveOfHeader = null )
        {
            string jwtToken = GetJWTTokenFromCache(tokenCache);

            var client = new RestClient(url) { Timeout = 30000 };

            var request = new RestRequest(Method.PATCH);

            if (effectiveOfHeader != null)
                request.AddHeader("Effective-Of", effectiveOfHeader);
            request.AddHeader("Authorization", "Bearer " + jwtToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> GetSoapResponseWithJWTokenAsync(string url, string tokenCache, string soapPayload, string soapAction)
        {
            string jwtToken = GetJWTTokenFromCache(tokenCache);

            var client = new RestClient(url) { Timeout = -1 };

            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "Bearer " + jwtToken);
            request.AddHeader("Content-Type", "application/soap+xml");
            request.AddHeader("soapAction", soapAction);
            request.AddHeader("soapVersion", "1.1");

            request.AddParameter("application/soap+xml", soapPayload, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> PostDataWithJWTokenAsync(string url, string tokenCache, string payload, string contentType)
        {
            string jwtToken = GetJWTTokenFromCache(tokenCache);
            var client = new RestClient(url) { Timeout = -1 };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + jwtToken);
            request.AddHeader("Content-Type", contentType);
            request.AddParameter(contentType, payload, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }
    }
}

