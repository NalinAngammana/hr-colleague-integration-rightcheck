using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ColleagueInt.RTW.Database.Constants
{
    public enum IncidentStatus
    {
        [Description("None")]
        None,
        [Description("Assigned")]
        Assigned,
        [Description("In Progress")]
        InProgress,
        [Description("Pending")]
        Pending,
        [Description("Resolved")]
        Resolved
    }

    public enum IncidentType
    {
        None,
        Data,
        Application
    }
}
