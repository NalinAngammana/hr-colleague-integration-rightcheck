using System.Collections.Generic;


namespace ColleagueInt.RTW.Consumer.Data
{

    public class ColleagueEnrichedData
    {
        public ColleagueEntry[] items { get; set; }
        public int count { get; set; }
        public bool hasMore { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public ColleagueLink[] links { get; set; }
    }

    public class ColleagueEntry
    {
        public long PersonId { get; set; }
        public string PersonNumber { get; set; }
        public object PreferredName { get; set; }
        public object PreferredLastName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public object MiddleName { get; set; }
        public object NameSuffix { get; set; }
        public string WorkEmail { get; set; }
        public string WorkerType { get; set; }
        public string Salutation { get; set; }
        public object NationalId { get; set; }
        public string DateOfBirth { get; set; }
        public string Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public object Region2 { get; set; }
        public string PostalCode { get; set; }
        public string HireDate { get; set; }
        public string TerminationDate { get; set; }
        public string ManagerPersonNumber { get; set; }
        public bool IsFutureDatedData { get; set; }
        public string Costcenter { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Grade { get; set; }
        public string Location { get; set; }
        public string LegalEntity { get; set; }
        public bool LongTermAbsence { get; set; }
        public bool IsApprentice { get; set; }
        public string PreviousTerminationDate { get; set; }
        public string PreviousStartDate { get; set; }
        public string PreviousBusinessUnitId { get; set; }
        public string PreviousPositionId { get; set; }
        public string BusinessUnit { get; set; }
        public string LocationName { get; set; }
        public List<ColleagueAssignment> assignments { get; set; }
    }

    public class ColleagueAssignment
    {
        public object OriginalHireDate { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentStatus { get; set; }
        public string ActionCode { get; set; }
        public string EffectiveStartDate { get; set; }
        public long? ManagerId { get; set; }
        public string ManagerPersonNumber { get; set; }
        public long? GradeId { get; set; }
        public long? LocationId { get; set; }
        public long? DepartmentId { get; set; }
        public long? PositionId { get; set; }
        public long? JobId { get; set; }
        public long? BusinessUnitId { get; set; }
        public object WorkerCategory { get; set; }
        //public object[] empreps { get; set; }
        public long? LegalEntityId { get; set; }
        public long? AssignmentId { get; set; }
        public string ProjectedStartDate { get; set; }
    }

    public class ColleagueLink
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
    }
}
