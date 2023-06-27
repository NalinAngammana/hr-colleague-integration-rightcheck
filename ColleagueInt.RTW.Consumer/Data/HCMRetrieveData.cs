using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Data
{
    public class HCMRetrieveData
    {
        public class CitizenshipData
        {
            public Item[] items { get; set; }
        }

        public class Item
        {
            public long CitizenshipId { get; set; }
            public string Citizenship { get; set; }
            public string FromDate { get; set; }
            public object ToDate { get; set; }
            public string CitizenshipStatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreationDate { get; set; }
            public string LastUpdatedBy { get; set; }
            public DateTime LastUpdateDate { get; set; }
            

            public long PassportId { get; set; }
            public string IssuingCountry { get; set; }
            public string PassportNumber { get; set; }


            public long VisaPermitId { get; set; }
            public string EffectiveStartDate { get; set; }
            public string EffectiveEndDate { get; set; }
            public string VisaPermitType { get; set; }
            public string VisaPermitNumber { get; set; }
            public string ExpirationDate { get; set; }
            public string IssuingAuthority { get; set; }
            public object IssueDate { get; set; }
            public object EntryDate { get; set; }
            public object VisaPermitCategory { get; set; }
            public Link[] links { get; set; }

        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
            public string name { get; set; }
            public string kind { get; set; }
            public Properties properties { get; set; }
        }

    }
}
