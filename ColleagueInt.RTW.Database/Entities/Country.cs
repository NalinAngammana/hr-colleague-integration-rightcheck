using ColleagueInt.RTW.Database.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("Countries")]
    public class Country : IdentityEntity
    {
        [Required]
        public string RTWCountryCode{ get; set; }

        [Required]
        public string RTWCountryName { get; set; }

        [Required]
        public string HCMCountryCode { get; set; }

        [Required]
        public string HCMCountryName { get; set; }

    }
}
