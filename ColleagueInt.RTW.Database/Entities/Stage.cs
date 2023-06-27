using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("Stages")]

    public class Stage : IdentityEntity
    {
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string StageName { get; set; }
        
     

    }
}
