using static ColleagueInt.RTW.Consumer.Data.RTWData;

namespace ColleagueInt.RTW.Consumer.Data.Contracts
{
    public interface IDataMapper
    {
        PersonCheckRequest GenerateRTWColleagueObject(ColleagueEntry colleagueEntry, string trackingReference);
        PersonCheckRequest GenerateRTWColleagueObject(ColleagueEntry colleagueEntry, string trackingReference, string lineManagerCostCentre);
        PersonStatus GenerateRTWColleagueStatusObject(bool status);
    }
}
