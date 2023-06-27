using ColleagueInt.RTW.Database.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("InboundErrorLog")]
    public class InboundErrorLog : IdentityEntity
    {
        [Required]
        public DateTime Logged { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string PersonNumber { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ClientId { get; set; }

        [Required]
        public IncidentErrorDescription ErrorType { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
