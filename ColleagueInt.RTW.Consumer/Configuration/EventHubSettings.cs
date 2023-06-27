using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Configuration
{
    public class EventHubSettings
    {
        public string EHNS_ConnectionString { get; set; }
        public string EH_Stg_ConnectionString { get; set; }
        public string EHName_EnrichedPublished { get; set; }
        public string EHName_EnrichedUpdated { get; set; }
        public string EHBlobContainer_sub_EnrichedPublished { get; set; }
        public string EHBlobContainer_sub_EnrichedUpdated { get; set; }
        public string EHConsumerGroup_sub_EnrichedPublished { get; set; }
        public string EHConsumerGroup_sub_EnrichedUpdated { get; set; }
    }
}
