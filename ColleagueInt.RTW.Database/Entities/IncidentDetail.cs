using ColleagueInt.RTW.Database.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("IncidentDetails")]
    public class IncidentDetail : IdentityEntity
    {
        [Column(TypeName = "varchar(50)")]
        public string ErrorCode { get; set; }

        [Required]
        public IncidentType Type { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string AssignmentGroup { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ConfigurationItem { get; set; }

        public string UndefinedCaller { get; set; }

        public string UndefinedLocation { get; set; }

        public string BusinessService { get; set; }

        [Required]
        public int Impact { get; set; }

        [Required]
        public bool Active { get; set; }

    }
}
