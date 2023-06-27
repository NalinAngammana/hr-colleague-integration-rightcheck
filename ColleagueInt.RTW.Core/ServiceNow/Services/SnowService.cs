
namespace ColleagueInt.RTW.Core.ServiceNow.Services
{
    using ColleagueInt.RTW.Database.Constants;
    using ColleagueInt.RTW.ViewModels;
    using Contracts;
    using Data;
    using RestSharp;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// This is Service Now related 
    /// </summary>
    public class SnowIncidentService : ISnowIncidentService
    {
		private string ServiceNowUrl { get; set; }

		public SnowIncidentService(string serviceNowUrl)
		{
			ServiceNowUrl = serviceNowUrl;
		}

		public async Task<string> CreateIncidentAsync(IncidentViewModel incidentViewModel)
		{
            incidentViewModel.SchemaId = IncidentDetailAction.Instance.GetIncidentErrorCode(IncidentDetailActionType.Create);
            var incidentJson = IncidentInput.GetJson(incidentViewModel);

            var restResponse = await PostResponseAsync(ServiceNowUrl, incidentJson);
            if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
                return string.Empty;

            var incidentDetails = JsonHelper.ToClass<IncidentOutput>(restResponse.Content);
            return incidentDetails.result?.FirstOrDefault()?.display_value;
        }

        public async Task<IncidentOutput> GetIncidentAsync(IncidentViewModel incidentViewModel)
        {
            incidentViewModel.SchemaId = IncidentDetailAction.Instance.GetIncidentErrorCode(IncidentDetailActionType.Read);
            var incidentJson = IncidentInput.GetJson(incidentViewModel);

            var restResponse = await PostResponseAsync(ServiceNowUrl, incidentJson);
            if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            var incidentDetails = JsonHelper.ToClass<IncidentOutput>(restResponse.Content);
            return incidentDetails;           
        }

        public async Task<string> GetIncidentStatusAsync(IncidentViewModel incidentViewModel)
        {
            incidentViewModel.SchemaId = IncidentDetailAction.Instance.GetIncidentErrorCode(IncidentDetailActionType.Read);
            var incidentJson = IncidentInput.GetJson(incidentViewModel);

            var restResponse = await PostResponseAsync(ServiceNowUrl, incidentJson);
            if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            var incidentDetails = JsonHelper.ToClass<IncidentOutput>(restResponse.Content);
            var result = incidentDetails.result;

            return result?.FirstOrDefault()?.state?.display_value;
        }

        async Task<IRestResponse> PostResponseAsync(string url, string jsonString)
		{
            var client = new RestClient(url)
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);

			request.AddHeader("Content-Type", "application/json");

			request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
			return await client.ExecuteAsync(request);
		}
	}
}
