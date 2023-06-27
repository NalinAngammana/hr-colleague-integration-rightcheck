using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("AppSettings")]
    public class AppSettings : IdentityEntity
    {

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Key { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Value { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }
    }
}
