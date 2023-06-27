using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.ConsumerAPI.Configuration
{
    public class KeyVaultSettings
    {
        public string KeyVaultUrl { get; set; }
        public string RTWCallBackAuthSecretKey { get; set; } 
        public string RTWApiSubscriptionKey { get; set; }
    }
} 