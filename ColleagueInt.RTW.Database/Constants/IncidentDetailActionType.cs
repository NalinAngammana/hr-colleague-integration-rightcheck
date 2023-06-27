

namespace ColleagueInt.RTW.Database.Constants
{
    using System;
    using System.Collections.Generic;

    public class IncidentDetailAction
    {
        public static readonly Lazy<IncidentDetailAction> lazyInstance = new Lazy<IncidentDetailAction>(() => new IncidentDetailAction());
        private static readonly Dictionary<IncidentDetailActionType, string> _incidentDetailsCodeDictionary = new Dictionary<IncidentDetailActionType, string>();

        private IncidentDetailAction()
        {
            _incidentDetailsCodeDictionary.Add(IncidentDetailActionType.Create, "ServiceNowNative");
            _incidentDetailsCodeDictionary.Add(IncidentDetailActionType.Read, "ServiceNowQuery");
            _incidentDetailsCodeDictionary.Add(IncidentDetailActionType.Update, "ServiceNowNative");
            _incidentDetailsCodeDictionary.Add(IncidentDetailActionType.Delete, "ServiceNowNative");
        }

        public static IncidentDetailAction Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        public string GetIncidentErrorCode(IncidentDetailActionType detailAction)
        {
            return _incidentDetailsCodeDictionary.ContainsKey((detailAction)) ? _incidentDetailsCodeDictionary[detailAction] : string.Empty;
        }
    }

    public enum IncidentDetailActionType
	{
		Create  = 1,
		Read = 2,
		Update = 3,
		Delete = 4
	}
}
