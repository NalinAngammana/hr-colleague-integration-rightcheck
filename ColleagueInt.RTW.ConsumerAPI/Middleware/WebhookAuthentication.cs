using ColleagueInt.RTW.ConsumerAPI.Configuration;
using ColleagueInt.RTW.ConsumerAPI.Misc;
using ColleagueInt.RTW.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.ConsumerAPI.Middleware
{
    public class WebhookAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<WebhookAuthentication> _logger;
        private readonly KeyVaultSettings _keyVaultSettings;

        private readonly string authSignedHeader = "HMAC-SHA256 SignedHeaders=x-ms-date;host;x-ms-content-sha256&Signature=";
        private const string signatureDate = "x-ms-date"; 
        private const string signatureSha256 = "x-ms-content-sha256";
        private string _authSecretKey { get; set; }

        public WebhookAuthentication(
            RequestDelegate next,
            ILogger<WebhookAuthentication> logger,
            IOptions<KeyVaultSettings> keyVaultSettings,
            IKeyVaultService keyVaultService)
        {
            _next = next;
            _logger = logger;
            _keyVaultSettings = keyVaultSettings.Value;
            var keyName = _keyVaultSettings.RTWCallBackAuthSecretKey;
            _authSecretKey = keyVaultService.GetSecretValue(keyName).GetAwaiter().GetResult();
            Logging.LogInformation(_logger, "Webhook Authentication class configuration completed");
        }
        public async Task InvokeAsync(HttpContext context)
        {

            if (!context.Request.Headers.TryGetValue("Authorization", out var extractedAuthHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Please provide a valid signature for authentication");
                Logging.LogInformation(_logger, "Webhook call received. Status : Authentication failed. Reason : No authorization header received.");
                return;
            }
            if (!context.Request.Headers.TryGetValue(signatureDate, out var extractedDate))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid header. Please provide a valid signature for authentication");
                Logging.LogInformation(_logger, "Webhook call received. Status : Authentication failed. Reason : No date field in header.");
                return;
            }
            if (!context.Request.Headers.TryGetValue(signatureSha256, out var extractedSha256))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid header. Please provide a valid signature for authentication");
                Logging.LogInformation(_logger, "Webhook call received. Status : Authentication failed. Reason : No sha256 signature in header.");
                return;
            }

            var authHeader = extractedAuthHeader.ToString().Split(authSignedHeader);
            if ((!(authHeader.Length == 2 && authHeader[0] == "")) || (String.IsNullOrEmpty(authHeader[1])))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Authorisation Header. Please provide a valid signature for authentication");
                Logging.LogInformation(_logger, "Webhook call received. Status : Authentication failed. Reason : Invalid Authorisation Header.");
                return;
            }
            var extractedSignature = authHeader[1];
           
            String stringToSign = context.Request.Method + '\n' + context.Request.Path + '\n' + extractedDate + ';' + context.Request.Host + ';' + extractedSha256;
            var byteSignature = Encoding.UTF8.GetBytes(stringToSign);

            //key is already in base64 format, so commenting the below line
            //var encodedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(_authSecretKey));

            using (HMACSHA256 hmac = new HMACSHA256(Convert.FromBase64String(_authSecretKey)))
            {
                byte[] signatureBytes = hmac.ComputeHash(byteSignature);
                var generatedSignature = Convert.ToBase64String(signatureBytes);

                if (!extractedSignature.Equals(generatedSignature))
                {
                    await context.Response.WriteAsync("Unauthorized request. Please provide a valid signature for authentication");
                    Logging.LogInformation(_logger, "Webhook call received. Status : Authentication failed. Reason : Signature didn't match. Unauthorized request !");
                    return;
                }
            }

            Logging.LogInformation(_logger, "Webhook call received. Status : Authentication check completed successfully!");
            //continue to the controller classes.
            await _next(context);
        }
    }
}
