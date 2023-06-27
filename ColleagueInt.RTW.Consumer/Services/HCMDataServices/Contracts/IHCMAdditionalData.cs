
using System;
using System.Threading.Tasks;
using static ColleagueInt.RTW.Consumer.Services.HCMDataServices.HCMAdditionalData;

namespace ColleagueInt.RTW.Consumer.Services.HCMDataServices.Contracts
{
    public interface IHCMAdditionalData
    {
        Task<BiReportFields> GetBIReportValuesAsync(string _soapUrl,
             long? personId, string assignmentEffectiveDate, long? assignmentId);
        Task<string> GetColleagueCostCentre(string colleaguePersonNumber);
        Task<string> GetColleagueCostCentre(long colleaguePersonId, long colleagueAssignmentId);
        Task<Tuple<long, long>> GetAssignmentDetailsAsync(string colleagueId);
    }
}
