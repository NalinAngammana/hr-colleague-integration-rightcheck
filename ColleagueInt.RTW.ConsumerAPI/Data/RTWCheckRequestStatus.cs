using System;

namespace ColleagueInt.RTW.ConsumerAPI.Data
{
    public class RTWCheckRequestStatus
    {
        public string TrackingReference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string ClientId { get; set; }
        public DateTime TimeCompleted { get; set; }
    }

    public class RTWReviewStatus
    {
        public string ReviewStatus { get; set; }
    }
}
