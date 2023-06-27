using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.ViewModels
{
    public class InboundErrorLogViewModel
    {
        public int Id { get; set; }
        public string PersonNumber { get; set; }
        public string ClientId { get; set; }
        public IncidentErrorDescription ErrorType { get; set; }
        public string Description { get; set; }
        public DateTime Logged { get; set; }
    }
}
