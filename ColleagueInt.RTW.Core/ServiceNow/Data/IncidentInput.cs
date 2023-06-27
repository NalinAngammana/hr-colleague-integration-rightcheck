namespace ColleagueInt.RTW.Core.ServiceNow.Data
{
	using ColleagueInt.RTW.Database.Constants;
	using ColleagueInt.RTW.ViewModels;
	using Newtonsoft.Json;

	public class IncidentInput
	{
		[JsonProperty("schemaId", NullValueHandling = NullValueHandling.Ignore)]
		public string schemaId { get; set; }

		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public IncidentData data { get; set; }

		public static string GetJson(IncidentViewModel incidentViewModel)
		{
			IncidentInput incidentInput = new IncidentInput();
			incidentInput.schemaId = incidentViewModel.SchemaId;

			IncidentData incidentData = new IncidentData
            {
                number = incidentViewModel.Number,
				description = incidentViewModel.ServiceNowDescription,
                cmdb_ci = incidentViewModel.ConfigurationItem,
                undefined_caller = incidentViewModel.UndefinedCaller,
                undefined_location = incidentViewModel.UndefinedLocation,
                short_description = incidentViewModel.Summary,
                business_service = incidentViewModel.BusinessService
            };
            incidentInput.data = incidentData;

			return JsonHelper.FromClass(incidentInput);
		}

		public static string GetIncidentStatusJson(IncidentViewModel incidentViewModel)
		{
			IncidentInput incidentInput = new IncidentInput 
			{ 
				schemaId = IncidentDetailAction.Instance.GetIncidentErrorCode(IncidentDetailActionType.Read) 
			};

			IncidentData incidentData = new IncidentData
			{
				number = incidentViewModel.Number
			};

			incidentInput.data = incidentData;

			return JsonHelper.FromClass(incidentInput);
		}
	}

	public class IncidentData
	{
		[JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
		public string description { get; set; }

		[JsonProperty("cmdb_ci", NullValueHandling = NullValueHandling.Ignore)]
		public string cmdb_ci { get; set; }

		[JsonProperty("undefined_caller", NullValueHandling = NullValueHandling.Ignore)]
		public string undefined_caller { get; set; }

		[JsonProperty("undefined_location", NullValueHandling = NullValueHandling.Ignore)]
		public string undefined_location { get; set; }

		[JsonProperty("short_description", NullValueHandling = NullValueHandling.Ignore)]
		public string short_description { get; set; }

		[JsonProperty("business_service", NullValueHandling = NullValueHandling.Ignore)]
		public string business_service { get; set; }

		[JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
		public string number { get; set; }
	}
}