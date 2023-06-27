using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Configuration
{
    using System;
    public class HcmSettings
    {
        public int WebServiceTimeOut { get; set; }
        public string HcmServerHost { get; set; }
        public int HcmServerPort { get; set; }
        public string HcmBiReportTemplate { get; set; }
        public string HcmCostCentreReportAbsolutePath { get; set; }
        public string HcmBiReportURLPath { get; set; }
    }
}