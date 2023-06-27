using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Data
{
    public class RTWData
    {
        public class PersonCheckRequest
        {

            /// <summary>
            /// *The First name of the person to be checked
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// *The Last name of the person to be checked
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            /// *Must be a unique tracking that is used to make check for updates against the request later.
            /// </summary>
            public string TrackingReference { get; set; }

            public string LocationId { get; set; }

            public string AdditionalIdentifier { get; set; }
        }

        public class CheckRequestDetails
        {
            public string TrackingReference { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Status { get; set; }
            public string ClientId { get; set; }
            public string TimeCompleted { get; set; }
        }

        public class PersonStatus
        {
            public string ReviewStatus { get; set; }
        }
    }
}
