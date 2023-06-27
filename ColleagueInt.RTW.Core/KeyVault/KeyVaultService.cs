

namespace ColleagueInt.RTW.Core
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Azure;
    using Azure.Identity;
    using System;
    using Azure.Security.KeyVault.Certificates;
    using Azure.Security.KeyVault.Secrets;

    public class KeyVaultService : IKeyVaultService
    {
        private string KeyVaultUrl { get; set; }

        private static readonly ConcurrentDictionary<string, KeyVaultSecret> SecretsCache = new ConcurrentDictionary<string, KeyVaultSecret>();
        public KeyVaultService(string keyVaultUrl)
        {
            KeyVaultUrl = keyVaultUrl;
        }

        public async Task<string> GetSecretValue(string secretName)
        {
            // Return from cache, if exists
            if (SecretsCache.ContainsKey(secretName))
            {
                var keyVaultSecretFromCache = SecretsCache[secretName];
                return keyVaultSecretFromCache.Value;
            }

            var keyVaultClient = new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());
            var secretBundle = await keyVaultClient.GetSecretAsync(secretName);

            // Add To Cache
            KeyVaultSecret keyVaultSecret = secretBundle.Value;
            SecretsCache.TryAdd(secretName, keyVaultSecret);
            return secretBundle == null ? string.Empty : keyVaultSecret.Value;
        }

        public async Task<string> GetCertificate(string secretName)
        {
            // Return from cache, if exists
            if (SecretsCache.ContainsKey(secretName))
            {
                var keyVaultSecretFromCache = SecretsCache[secretName];
                return keyVaultSecretFromCache.Value;
            }

            var secretClient = new SecretClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());
            var certClient = new CertificateClient(new Uri(KeyVaultUrl), new DefaultAzureCredential());

            Response<KeyVaultCertificateWithPolicy> certResponse = await certClient.GetCertificateAsync(secretName);

            var segments = certResponse.Value.SecretId.Segments;
            string secretKey = segments[2].Trim('/');
            string version = segments[3].TrimEnd('/');

            Response<KeyVaultSecret> secretResponse = await secretClient.GetSecretAsync(secretKey, version);

            // Add To Cache
            KeyVaultSecret keyVaultSecret = secretResponse.Value;
            SecretsCache.TryAdd(secretName, keyVaultSecret);
            return keyVaultSecret.Value;
        }
    }
}
