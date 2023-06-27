namespace ColleagueInt.RTW.Consumer.Data
{
    class HCMColleagueDetails
    {
        public Item[] items { get; set; }
        public int count { get; set; }
        public bool hasMore { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public Link1[] links { get; set; }
    }

    public class Item
    {
        public long PersonNumber { get; set; }
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

    public class Properties
    {
        public string changeIndicator { get; set; }
    }

    public class Link1
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
    }
}