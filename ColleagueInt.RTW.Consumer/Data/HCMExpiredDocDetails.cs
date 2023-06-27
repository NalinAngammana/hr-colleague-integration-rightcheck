using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Data
{
    public class HCMExpiredDocDetails
    {
        public Item[] items { get; set; }
        public int totalResults { get; set; }
        public int count { get; set; }
        public bool hasMore { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public Link[] links { get; set; }
       

        public class Item
        {
            public string PersonId { get; set; }
            public string PersonNumber { get; set; }
            public Name[] names { get; set; }
            public Visaspermit[] visasPermits { get; set; }
            public Workrelationship[] workRelationships { get; set; }
        }

        public class Name
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Visaspermit
        {
            public long VisaPermitId { get; set; }
            public string VisaPermitType { get; set; }
            public string ExpirationDate { get; set; }
        }

        public class Workrelationship
        {
            public string LegalEmployerName { get; set; }

            public string WorkerType { get; set; }
            
            public Assignment[] assignments { get; set; }
        }

        public class Assignment
        {
            public string AssignmentId { get; set; }
            public string AssignmentNumber { get; set; }
            public string AssignmentStatusType { get; set; }
            public string BusinessUnitName { get; set; }
            public string SystemPersonType { get; set; }
            public string EffectiveStartDate{get; set;}
            public Manager[] managers { get; set; }
        }

        public class Manager
        {
            public string ManagerAssignmentNumber { get; set; }
        }
    
            public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
            public string name { get; set; }
            public string kind { get; set; }
        }
    }
}
