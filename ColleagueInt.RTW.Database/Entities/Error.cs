using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("Errors")]
    public class Error : IdentityEntity
    {
        public DateTime Logged { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string StackTrace { get; set; }
    }
}
