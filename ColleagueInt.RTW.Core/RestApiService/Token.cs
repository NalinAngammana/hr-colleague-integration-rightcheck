
namespace ColleagueInt.RTW.Core.RestApiService
{
    using Newtonsoft.Json;
    using System;

    internal class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public DateTime GeneratedAt { get; set; }

        public bool Expired()
        {
            var shouldExpireAt = GeneratedAt.AddSeconds(ExpiresIn);
            var now = DateTime.Now.ToUniversalTime();
            return now > shouldExpireAt;
        }
    }
}
