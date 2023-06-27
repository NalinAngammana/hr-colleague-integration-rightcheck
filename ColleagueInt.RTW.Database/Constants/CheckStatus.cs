using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Database.Constants
{
    public enum CheckStatus
    {
        CheckNotCompleted = 1,
        Successful = 2,
        Failed = 3,
        PartialCheck = 4,
        UserRemovedCheck = 5,
        SystemRemovedCheck = 6
    }
}
