using ColleagueInt.RTW.Database.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ColleagueInt.RTW.Database.Entities
{

    [Table("Incidents")]
    public class Incident : IdentityEntity
    {
        [Required]
        public int IncidentDetailId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Number { get; set; }

        public IncidentStatus Status { get; set; }

        public DateTime CreationTime { get; set; }

        public string ServiceNowDescription { get; set; }

        public virtual IncidentDetail IncidentDetails { get; set; }
    }
}
