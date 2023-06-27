using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Configuration
{
    public class RTWSettings
    {
        private string createRequestURL;
        public string OrganisationId { get; set; }
        public string CreateRequestURL
        {
            get
            {
                if (OrganisationId == null)
                    return createRequestURL;
                else
                    return createRequestURL.Replace("{organizationId}", OrganisationId);
            }
            set
            {
                createRequestURL = value;
            }
        }

        public string PersonDetailsURL { get; set; }

        public string PersonCurrentDocumentSetURL { get; set; }
    }
}
