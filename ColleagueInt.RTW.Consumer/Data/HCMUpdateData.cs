using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Data
{
    public class HCMUpdateData
    {
        public string PersonNumber { get; set; }
        //public string Status { get; set; }
        //public string EmployeeNumber { get; set; }       
    }

    public class HCMCitizenship
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Citizenship { get; set; }   // list of Nationality

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CitizenshipStatus { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CitizenshipLegislationCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FromDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ToDate { get; set; }
    }

    public class HCMPassports
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IssuingCountry { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PassportType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PassportNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IssuingAuthority { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IssueDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExpirationDate { get; set; }
    }

    public class HCMVisas
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VisaPermitType { get; set; }   // list of types
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VisaPermitCategory { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VisaPermitNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VisaPermitStatus { get; set; } // (Active, Pending, Inactive)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IssuingAuthority { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IssuingCountry { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IssuingLocation { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? EffectiveStartDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? IssueDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? EntryDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? ExpirationDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ECSCheckDateReceived { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ECSResult { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string T4MaximumWorkingHours { get; set; }
    }

    public class HCMDocumentRecords
    {
        public List<HCMDocumentRecord> Documents { get; set; }

        public HCMDocumentRecords()
        {
            Documents = new List<HCMDocumentRecord>();
        }
    }

    public class HCMDocumentRecord
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PersonNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object DateTo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? DocumentTypeId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Attachment> attachments { get; set; } 
    }

    public class Attachment
    {
        public string UploadedFileName { get; set; }
        public string FileContents { get; set; }

        public Attachment(string filename, string content)
        {
            UploadedFileName = filename;
            FileContents = content;
        }
    }
}
