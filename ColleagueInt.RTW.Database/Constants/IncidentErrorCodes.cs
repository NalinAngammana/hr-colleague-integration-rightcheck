using System;
using System.Collections.Generic;

namespace ColleagueInt.RTW.Database.Constants
{
    public class IncidentErrorCodes
    {
        public static readonly Lazy<IncidentErrorCodes> lazyInstance = new Lazy<IncidentErrorCodes>(() => new IncidentErrorCodes());
        private static readonly Dictionary<IncidentErrorDescription, string> _incidentErrorCodesDictionary = new Dictionary<IncidentErrorDescription, string>();

        private IncidentErrorCodes()
        {
            _incidentErrorCodesDictionary.Add(IncidentErrorDescription.AzureEventHubError,  "AZRCCP_RTW_HR_APP_101001");
            _incidentErrorCodesDictionary.Add(IncidentErrorDescription.AzureDatabaseError,  "AZRCCP_RTW_HR_APP_102001");
            _incidentErrorCodesDictionary.Add(IncidentErrorDescription.AzureKeyVaultError,  "AZRCCP_RTW_HR_APP_103001");
            _incidentErrorCodesDictionary.Add(IncidentErrorDescription.RTWAPIOutboundError, "AZRCCP_RTW_HR_APP_104001");
            _incidentErrorCodesDictionary.Add(IncidentErrorDescription.RTWAPIInboundError,  "AZRCCP_RTW_HR_APP_105001");
            _incidentErrorCodesDictionary.Add(IncidentErrorDescription.HCMUpdateError,      "AZRCCP_RTW_HR_APP_106001");
            _incidentErrorCodesDictionary.Add(IncidentErrorDescription.GenericException,    "AZRCCP_RTW_HR_APP_110001");
        }

        public static IncidentErrorCodes Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        public string GetIncidentErrorCode(IncidentErrorDescription errorDescription)
        {
            return _incidentErrorCodesDictionary.ContainsKey((errorDescription)) ? _incidentErrorCodesDictionary[errorDescription] : string.Empty;
        }
    }

    public enum IncidentErrorDescription
    {
        NoIncident,
        AzureEventHubError,
        AzureDatabaseError,
        AzureKeyVaultError,
        RTWAPIOutboundError,
        RTWAPIInboundError,
        HCMUpdateError,
        GenericException,
    }
}
