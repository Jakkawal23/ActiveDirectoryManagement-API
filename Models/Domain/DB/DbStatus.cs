using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ActiveDirectoryManagement_API.Models.Domain.Document;

namespace ActiveDirectoryManagement_API.Models.Domain.DB
{
    public class DbStatus
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [Key]
        [StringLength(20)]
        public string StatusCode { get; set; }

        [StringLength(200)]
        public string StatusNameTH { get; set; }

        [StringLength(200)]
        public string StatusNameEN { get; set; }

        public bool Active { get; set; }

        //public virtual ICollection<DocumentPassword> DocumentPasswords { get; set; }
    }
}
