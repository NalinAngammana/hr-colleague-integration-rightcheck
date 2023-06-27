namespace ColleagueInt.RTW.Core.ServiceNow.Data
{
    public class IncidentOutput
	{
		public string import_set { get; set; }
		public string staging_table { get; set; }
		public ResultOutput[] result { get; set; }
	}

	public class ResultOutput
	{
		public string transform_map { get; set; }
		public string table { get; set; }
		public string display_name { get; set; }
		public string display_value { get; set; }
		public string record_link { get; set; }
		public string status { get; set; }
		public string sys_id { get; set; }
		public Short_Description short_description { get; set; }
		public Close_Code close_code { get; set; }
		public Assignment_Group assignment_group { get; set; }
		public Cmdb_Ci cmdb_ci { get; set; }
		public Impact impact { get; set; }
		public Description description { get; set; }
		public Close_Notes close_notes { get; set; }
		public Number number { get; set; }
		public U_Location_Not_Found u_location_not_found { get; set; }
		public Urgency urgency { get; set; }
		public U_Undefined_Location u_undefined_location { get; set; }
		public Location location { get; set; }
		public State state { get; set; }
		public Category category { get; set; }
		public U_Closure_Configuration_Item u_closure_configuration_item { get; set; }
		public Subcategory subcategory { get; set; }
		public Assigned_To assigned_to { get; set; }
	}

	public class Short_Description
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Close_Code
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Assignment_Group
	{
		public string display_value { get; set; }
		public string link { get; set; }
		public string value { get; set; }
	}

	public class Cmdb_Ci
	{
		public string display_value { get; set; }
		public string link { get; set; }
		public string value { get; set; }
	}

	public class Impact
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Description
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Close_Notes
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Number
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class U_Location_Not_Found
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Urgency
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class U_Undefined_Location
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Location
	{
		public string display_value { get; set; }
		public string link { get; set; }
		public string value { get; set; }
	}

	public class State
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Category
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class U_Closure_Configuration_Item
	{
		public string display_value { get; set; }
		public string link { get; set; }
		public string value { get; set; }
	}

	public class Subcategory
	{
		public string display_value { get; set; }
		public string value { get; set; }
	}

	public class Assigned_To
	{
		public string display_value { get; set; }
		public string link { get; set; }
		public string value { get; set; }
	}
}
