using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.ViewModels
{
    public class ColleagueViewModel
    {
        public int Id { get; set; }
        public string PersonNumber { get; set; }
        public string JsonData { get; set; }
        public string TrackingReference { get; set; }
        public string ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public Stages StageId { get; set; }
        public CheckStatus? StatusId { get; set; }
        public DateTime LastUpdateOn { get; set; }
        public string  ErrorLog { get; set; }
    }
}
