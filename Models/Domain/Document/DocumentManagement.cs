using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ActiveDirectoryManagement_API.Models.Domain.DB;

namespace ActiveDirectoryManagement_API.Models.Domain.Document
{
    public class DocumentManagement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagementId { get; set; }

        //[ForeignKey("EmpCode")]
        [StringLength(20)]
        public string EmpCode { get; set; }

        public bool Password { get; set; }



        [ForeignKey(nameof(EmpCode))]
        public DbEmployee? DbEmployee { get; set; }
    }
}
