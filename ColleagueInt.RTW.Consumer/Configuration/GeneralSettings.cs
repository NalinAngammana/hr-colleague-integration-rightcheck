using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Configuration
{
    using System;
    public class GeneralSettings
    {
        public string RTW_Get_ManagerCostCentre_ForBUs { get; set; }
        public string RTW_AllowedColleagueTypes { get; set; }
        public int CleanUpAfterDays { get; set; }
        public bool EnableDetailedLogging { get; set; }
        public bool AllowServiceNowIntegration { get; set; }
        public TimeSpan UpdateIncidentStatusInterval { get; set; }
        public string SslCertificateKeyName { get; set; }
        public string SslCertificateIssuerName { get; set; }               
        public int APIErrorRecordInterval { get; set; }
        public int APIErrorRecordLimit { get; set; }
        public string LookupRefreshIntervalInDays { get; set; }
        public string DocumentOfRecordsReferenceField { get; set; }
    }
}