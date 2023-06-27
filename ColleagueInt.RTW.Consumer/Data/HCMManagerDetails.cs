namespace ColleagueInt.RTW.Consumer.Data
{
    public class ColleagueManagerDetails
    {
        public ItemManager[] items { get; set; }
        public int count { get; set; }
        public bool hasMore { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public LinkManager[] links { get; set; }
    }

    public class ItemManager
    {
        public long PersonId { get; set; }
        public Assignment[] assignments { get; set; }
    }

    public class Assignment
    {
        public long AssignmentId { get; set; }
        public string AssignmentStatus { get; set; }
        public string EffectiveStartDate { get; set; }
    }

    public class LinkManager
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
    }
}