using ActiveDirectoryManagement_API.Models.Domain.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.Domain.Document
{
    public class DocumentPhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }

        //[ForeignKey("EmpCode")]
        [StringLength(20)]
        public string EmpCode { get; set; }

        [StringLength(200)]
        public string Phone { get; set; }

        //[ForeignKey("StatusCode")]

        [StringLength(20)]
        public string StatusCode { get; set; }

        [StringLength(20)]
        public string? ApproveEmpCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ApproveDate { get; set; }


        [ForeignKey(nameof(EmpCode))]
        public DbEmployee? DbEmployee { get; set; }

        [ForeignKey(nameof(StatusCode))]
        public DbStatus? DbStatus { get; set; }
    }
}
