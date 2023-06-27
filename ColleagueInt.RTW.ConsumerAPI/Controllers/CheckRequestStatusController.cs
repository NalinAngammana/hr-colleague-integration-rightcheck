using System;
using System.Threading.Tasks;
using ColleagueInt.RTW.ConsumerAPI.Configuration;
using ColleagueInt.RTW.ConsumerAPI.Data;
using ColleagueInt.RTW.ConsumerAPI.Misc;
using ColleagueInt.RTW.Core;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Services.Contracts;
using ColleagueInt.RTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ColleagueInt.RTW.ConsumerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckRequestStatusController : Controller
    {

        private readonly ILogger<CheckRequestStatusController> _logger;
        private readonly IColleagueService _colleagueService;
        private readonly IAzureApiService _restApiService;
        private readonly RTWSettings _rtwSettings;
        private readonly IServiceProvider _serviceProvider;

        public CheckRequestStatusController(
            ILogger<CheckRequestStatusController> logger,
            IColleagueService colleagueService,
            IAzureApiService restApiService,
            IOptions<RTWSettings> rtwSettings,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _colleagueService = colleagueService;
            _restApiService = restApiService;
            _rtwSettings = rtwSettings.Value;
            _serviceProvider = serviceProvider;
        }


        [HttpPost("StatusUpdate")]
        public async Task<IActionResult> UpdateRequestStatusAsync([FromBody] RTWCheckRequestStatus request)
        {
            try
            {
                if (request != null)
                {
                    var statusid = (CheckStatus)Enum.Parse(typeof(CheckStatus), request.Status.Replace(" ", ""));
                    var colleague = await _colleagueService.GetColleagueRecordByTrackingRefAsync(request.TrackingReference);
                    if (colleague != null)
                    {
                        if (colleague.StageId == Stages.InitalStage || colleague.StageId == Stages.CheckRequested)
                        {
                            colleague.ClientId = request.ClientId;
                            colleague.StageId = Stages.CheckCompleted;
                            colleague.StatusId = statusid;
                            colleague.LastUpdateOn = DateTime.UtcNow;
                            await _colleagueService.UpdateColleagueAsync(colleague);
                            Logging.LogInformation(_logger, $"Webhook call received. ClientID: {request.ClientId} for tracking reference: {request.TrackingReference} updated in database.");
                        }
                        else
                        {
                            Logging.LogInformation(_logger, $"Webhook call received. Check request with tracking reference: {request.TrackingReference} already past the stage: {Stages.CheckRequested}.");
                        }
                    }
                    else
                    {
                        Logging.LogInformation(_logger, $"Webhook call received. Request ignored. Reason : No record found in database for ClientID: {request.ClientId}.");
                    }

                    //Update review status to 'My HR' for the person
                    var _getPersonDetailsBaseURL = _rtwSettings.PersonDetailsURL;
                    var getPersonDetailsURL = _getPersonDetailsBaseURL.Replace("{personId}", request.ClientId);
                    RTWReviewStatus rtwReviewStatus = new RTWReviewStatus
                    {
                        ReviewStatus = "My HR"
                    };
                    var jsonData = JsonHelper.FromClass(rtwReviewStatus);
                    IRestResponse restResponse = await _restApiService.PostResponseAsync(getPersonDetailsURL, jsonData, Method.PUT);
                    if (!restResponse.IsSuccessful )
                    {
                        Logging.LogError(_logger, $"Webhook call received. Failed to update review status in RTW portal for ClientID: {request.ClientId}, TrackingReference : {request.TrackingReference}.");                        
                    }
                    else
                    {
                        Logging.LogInformation(_logger, $"Webhook call received. Review status updated in RTW portal for ClientID: {request.ClientId}, TrackingReference : {request.TrackingReference}.");
                    }

                    return Accepted("", "Message received successfully.");
                }
                Logging.LogError(_logger, "Invalid message received.");
                return ValidationProblem("Invalid message received.");
            }
            catch (Exception ex)
            {
                await Logging.LogExceptionAsync(_serviceProvider, _logger, ex, IncidentErrorDescription.GenericException);
                return ValidationProblem(ex.Message);
            }
        }
    }
}

