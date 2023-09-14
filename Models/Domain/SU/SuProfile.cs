using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.Domain.SU
{
    public class SuProfile
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        [Key]
        [StringLength(20)]
        public string ProfileCode { get; set; }
        
        [StringLength(200)]
        public string ProfileNameTH { get; set; }

        [StringLength(200)]
        public string ProfileNameEN { get; set; }

        public bool Active { get; set; }

        //public virtual ICollection<SuUser> Users { get; set; }
    }
}
