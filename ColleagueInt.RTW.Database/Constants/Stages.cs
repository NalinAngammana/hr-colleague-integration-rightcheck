using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Database.Constants
{
    public enum Stages
    {
        InitalStage = 1,
        CheckRequested = 2,
        CheckCompleted = 3,
        HCMUpdated = 4,
        CheckRequestFailed = 5,
        ReadDataFailed = 6,
        HCMUpdateFailed = 7,
        HCMDocUploaded = 8,
        HCMDocUploadFailed = 9,
        PassportPatchFailed = 10,
        PassportPatchIgnored = 11,
        PassportPatchCompleted = 12

    }
}
