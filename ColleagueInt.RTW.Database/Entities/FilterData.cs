using ColleagueInt.RTW.Database.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleagueInt.RTW.Database.Entities
{
    public class FilterData : IdentityEntity
    {
       
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string BusinessUnitName { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; }        

    }
}
