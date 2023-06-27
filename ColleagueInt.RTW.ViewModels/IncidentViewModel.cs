using ColleagueInt.RTW.Database.Constants;
using System;

namespace ColleagueInt.RTW.ViewModels
{
    public class IncidentViewModel
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public DateTime CreationTime { get; set; }
		public string Summary { get; set; }
		public IncidentErrorDescription ErrorDescription { get; set; }
		public string Description { get; set; }
		public IncidentStatus Status { get; set; }
		public string SchemaId { get; set; }
		public string ConfigurationItem { get; set; }
		public string UndefinedCaller { get; set; }
		public string UndefinedLocation { get; set; }
		public string BusinessService { get; set; }
		public IncidentType Type { get; set; }
		public string AssignmentGroup { get; set; }
		public string ServiceNowDescription { get; set; }
		public bool Active { get; set; }
	}
}
