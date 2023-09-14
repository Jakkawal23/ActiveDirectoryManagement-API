using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActiveDirectoryManagement_API.Models.Domain.DB
{
    public class DbDepartment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Key]
        [StringLength(20)]
        public string DepartmentCode { get; set; }

        [StringLength(200)]
        public string DepartmentNameTH { get; set; }

        [StringLength(200)]
        public string DepartmentNameEN { get; set; }

        public bool Active { get; set; }

        //public virtual ICollection<DbEmployee> DbEmployees { get; set; }
    }
}
