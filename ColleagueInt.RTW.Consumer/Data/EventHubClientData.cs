namespace ColleagueInt.RTW.Consumer.Data
{
    public class EventHubClientData
    {
        public string EHNS_ConnectionString { get; set; }
        public string EH_Name { get; set; }
        public string? EH_Stg_ConnectionString { get; set; }
        public string? EH_BlobContainerName { get; set; }
        public string? EH_ConsumerGroup { get; set; }
    }
}
