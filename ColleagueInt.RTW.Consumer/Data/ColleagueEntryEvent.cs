using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Data
{
    using System;

    public class ColleagueEntryEvent : EventArgs
    {
        public string FeedType { get; set; }
        public ColleagueEntry ColleagueEntry { get; set; }
    }
}
