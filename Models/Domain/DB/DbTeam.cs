using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.Domain.DB
{
    public class DbTeam
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }

        [Key]
        [StringLength(20)]
        public string TeamCode { get; set; }

        [StringLength(200)]
        public string TeamNameTH { get; set; }

        [StringLength(200)]
        public string TeamNameEN { get; set; }

        public bool Active { get; set; }

        //public virtual ICollection<DbEmployee> Employees { get; set; }
    }
}
