using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Configuration
{
    public class KeyVaultSettings
    {
        public string KeyVaultUrl { get; set; }
        public string KeyVaultCertificateUrl { get; set; }
        public string HcmFeedUserName { get; set; }
        public string ServiceNowIntegrationUrl { get; set; }
    }
} 