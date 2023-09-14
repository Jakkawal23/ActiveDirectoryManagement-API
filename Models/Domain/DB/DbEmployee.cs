using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ActiveDirectoryManagement_API.Models.Domain.Document;

namespace ActiveDirectoryManagement_API.Models.Domain.DB
{
    public class DbEmployee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Key]
        [StringLength(20)]
        public string EmployeeCode { get; set; }

        //[ForeignKey("DepartmentCode")]
        [StringLength(20)]
        public string DepartmentCode { get; set; }

        //[ForeignKey("TeamCode")]
        [StringLength(20)]
        public string TeamCode { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        [StringLength(400)]
        public string FullName { get; set; }


        [ForeignKey(nameof(DepartmentCode))]
        public DbDepartment? DbDepartment { get; set; }

        [ForeignKey(nameof(TeamCode))]
        public DbTeam? DbTeam { get; set; }


        //public DbDepartment DbDepartment { get; set; }

        //public virtual DbDepartment Department { get; set; }
        //public virtual DbTeam Team { get; set; }
        //public virtual ICollection<DocumentPassword> DocumentPasswords { get; set; }

    }
}
