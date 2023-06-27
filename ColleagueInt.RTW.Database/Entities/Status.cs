using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("Statuses")]

    public class Status : IdentityEntity
    {
          
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string StatusName { get; set; }


        [Required]
        [Column(TypeName = "varchar(300)")]
        public string ExternalValue { get; set; }
 

    }
}
