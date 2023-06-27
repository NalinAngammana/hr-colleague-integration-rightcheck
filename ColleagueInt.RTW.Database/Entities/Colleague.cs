using ColleagueInt.RTW.Database.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("Colleagues")]
    public class Colleague : IdentityEntity
    {
        [Column(TypeName = "varchar(50)")]
        public string PersonNumber { get; set; }
        
        public string JsonData { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string TrackingReference { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ClientId { get; set; }

        [Required] 
        public DateTime StartDate { get; set; }

        [Required]
        public int StageId { get; set; }

        public int ? StatusId { get; set; }

        [Required]
        public DateTime LastUpdateOn { get; set; }

        public string?  ErrorLog { get; set; }

        public virtual Stage IncidentDetails { get; set; }

    }
}
