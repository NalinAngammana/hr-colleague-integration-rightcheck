using CloudColleaguePublisher.Core;
using ColleagueInt.RTW.Consumer.Configuration;
using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Consumer.Services.HCMDataServices.Contracts;
using ColleagueInt.RTW.Core;
using ColleagueInt.RTW.Core.RestApiService.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueInt.RTW.Consumer.Services.HCMDataServices
{
    public class HCMAdditionalData : IHCMAdditionalData
    {
        private readonly HcmSettings _hcmSettings;
        private readonly IJwTokenService _jwTokenService;

        public HCMAdditionalData(IOptions<HcmSettings> hcmSettings, IJwTokenService jwTokenService)
        {
            _hcmSettings = hcmSettings.Value;
            _jwTokenService = jwTokenService;
        }

        public async Task<BiReportFields> GetBIReportValuesAsync(string _soapUrl,
             long? personId, string assignmentEffectiveDate, long? assignmentId)
        {
            var reportAbsolutePath = _hcmSettings.HcmCostCentreReportAbsolutePath;
            var reportTemplate = _hcmSettings.HcmBiReportTemplate;

            BiReportFields report = new BiReportFields
            {
                Successful = false
            };

            var soapEnvelopeOpen = @"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:pub=""http://xmlns.oracle.com/oxp/service/PublicReportService"">";
            var soapHeader = @"<soap:Header/>";
            var soapBody = @"<soap:Body><pub:runReport><pub:reportRequest><pub:attributeFormat>xml</pub:attributeFormat>" +
            $"<pub:attributeLocale>English(United States)</pub:attributeLocale><pub:attributeTemplate>{reportTemplate}</pub:attributeTemplate><pub:parameterNameValues>" +
            $"<!--Zero or more repetitions:--><pub:item><pub:name>P_EFF_DATE</pub:name><pub:values><!--Zero or more repetitions:--><pub:item>" +
            $"{assignmentEffectiveDate}</pub:item></pub:values></pub:item><pub:item><pub:name>P_ASSIGNMENT_ID</pub:name>" +
            $"<pub:values><!--Zero or more repetitions:--><pub:item>{assignmentId}</pub:item></pub:values>" +
            $"</pub:item><pub:item><pub:name>P_PERSON_ID</pub:name><pub:values><!--Zero or more repetitions:--><pub:item>{personId}</pub:item>" +
            $"</pub:values></pub:item></pub:parameterNameValues><pub:reportAbsolutePath>{reportAbsolutePath}</pub:reportAbsolutePath>" +
            $"<pub:sizeOfDataChunkDownload>-1</pub:sizeOfDataChunkDownload></pub:reportRequest></pub:runReport></soap:Body>";
            var soapEnvelopeClose = @"</soap:Envelope>";

            string soapAction = "runReportRequest";

            StringBuilder soapPayload = new StringBuilder();
            soapPayload = soapPayload.Append(soapEnvelopeOpen).Append(soapHeader).Append(soapBody).Append(soapEnvelopeClose);

            var restResponse = await _jwTokenService.GetSoapResponseWithJWTokenAsync(_soapUrl, CacheConstants.CloudApplicationCertificate, soapPayload.ToString(), soapAction);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Envelope newSoapResponse = XmlHelper.ToClass<Envelope>(restResponse.Content);

                var encodedString = newSoapResponse.Body.runReportResponse.runReportReturn.reportBytes;
                string decodedXmlString = Encoding.UTF8.GetString(encodedString);


                report.CostCenter = await XmlHelper.GetValueFromXml(decodedXmlString, "COST_CENTER");
                report.BusinessUnit = await XmlHelper.GetValueFromXml(decodedXmlString, "BUSINESSUNIT_NAME");

                report.Successful = true;
            }
            else
            {
                throw new Exception(report.ErrorResponse = $"Error while calling reportAbsolutePath: {reportAbsolutePath}, Status Code: {restResponse.StatusCode} {restResponse.StatusDescription}, PersonId: {personId}.");
            }

            return await Task.FromResult(report);
        }
        public async Task<string> GetColleagueCostCentre(string colleaguePersonNumber)
        {
            try
            {
                Tuple<long, long> collAssignmentDetails = await GetAssignmentDetailsAsync(colleaguePersonNumber);
                if (collAssignmentDetails != null)
                {
                    var soapUrl = $"{_hcmSettings.HcmServerHost}:{_hcmSettings.HcmServerPort}{_hcmSettings.HcmBiReportURLPath}";
                    var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                    BiReportFields biReportFields = await GetBIReportValuesAsync
                        (soapUrl, collAssignmentDetails.Item1, currentDate, collAssignmentDetails.Item2);
                    if (biReportFields.Successful)
                    {
                        return biReportFields.CostCenter;
                    }
                    else if (!string.IsNullOrEmpty(biReportFields.ErrorResponse))
                    {
                        throw new Exception($"Error: {biReportFields.ErrorResponse} while calling Additional Details BI Report.");                           
                    }
                    else
                    {
                        throw new Exception($"Error while calling Additional Details BI Report.");                        
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving assignment details for colleague : {colleaguePersonNumber}; Message : {ex.Message}");                
            }
        }

        public async Task<string> GetColleagueCostCentre(long colleaguePersonId, long colleagueAssignmentId)
        {
            try
            {
                var soapUrl = $"{_hcmSettings.HcmServerHost}:{_hcmSettings.HcmServerPort}{_hcmSettings.HcmBiReportURLPath}";
                var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                BiReportFields biReportFields = await GetBIReportValuesAsync
                    (soapUrl, colleaguePersonId, currentDate, colleagueAssignmentId);
                if (biReportFields.Successful)
                {
                    return biReportFields.CostCenter;
                }
                else if (!string.IsNullOrEmpty(biReportFields.ErrorResponse))
                {
                    throw new Exception($"Error: {biReportFields.ErrorResponse} while calling Additional Details BI Report.");
                }
                else
                {
                    throw new Exception($"Error while calling Additional Details BI Report.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving assignment details for colleague : {colleaguePersonId}; Message : {ex.Message}");
            }
        }
        public async Task<Tuple<long, long>> GetAssignmentDetailsAsync(string colleagueId)
        {
            try
            {
                // Get the manager details
                var hcmServerHost = _hcmSettings.HcmServerHost;
                var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

                var colleagueDetailsUrl =
                        $"{hcmServerHost}/hcmRestApi/resources/latest/emps?" +
                        $"q=PersonNumber={colleagueId}&effectiveDate={currentDate}" +
                        $"&onlyData=true&fields=PersonId;assignments:AssignmentId,AssignmentStatus,EffectiveStartDate";

                var restResponse = await _jwTokenService.GetResponseWithJWTokenAsync(colleagueDetailsUrl, CacheConstants.CloudApplicationCertificate);
                if (restResponse.IsSuccessful)
                {
                    try
                    {
                        var result = JsonHelper.ToClass<ColleagueManagerDetails>(restResponse.Content);
                        if (result.count == 0)
                        {
                            throw new Exception($"Failed to retrieve assignment details for colleague : {colleagueId}, Error details : Person not found");
                        }

                        Assignment collAssignment = new Assignment();
                        var collDetails = result.items.ToList().FirstOrDefault();
                        var assignmentCount = collDetails.assignments.Count();
                        // If there is only one assignment, Pick the Inactive one too.
                        if (assignmentCount == 0)
                        {
                            throw new Exception($"Failed to retrieve assignment details for colleague : {colleagueId}, No assignment details found");
                            
                        }
                        else if (assignmentCount == 1)
                        {
                            collAssignment = collDetails.assignments.FirstOrDefault();
                        }
                        else
                        {
                            var sortedAssignments = collDetails.assignments.OrderByDescending(x => x.EffectiveStartDate).ToList();
                            // For multiple assignments, get the first active assignment if any,
                            // after sorting by EffectiveStartDate descending
                            collAssignment = sortedAssignments.Any(x => x.AssignmentStatus == "ACTIVE")
                                ? sortedAssignments.FirstOrDefault(x => x.AssignmentStatus == "ACTIVE")
                                : sortedAssignments.FirstOrDefault();
                        }

                        return Tuple.Create<long, long>(collDetails.PersonId, collAssignment.AssignmentId);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error occurred while retrieving assignment details for colleague : {colleagueId}; Message : {ex.Message}; Response : {restResponse.Content}");                       
                    }
                }
                else
                {
                  throw new Exception($"Error occurred while retrieving assignment details for colleague : {colleagueId}, Return Data : {restResponse.Content}");                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving assignment details for colleague : {colleagueId}; Message : {ex.Message}");               
            }
        }

        
        public class BiReportFields
        {
            public string CostCenter { get; set; }
            public bool LongTermAbsence { get; set; }
            public string BusinessUnit { get; set; }
            public string LocationName { get; set; }
            public string GeographicHierarchy { get; set; }
            public bool Successful { get; set; }
            public string ErrorResponse { get; set; }
        }
    }
}
