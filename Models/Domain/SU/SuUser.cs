using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ActiveDirectoryManagement_API.Models.Domain.DB;

namespace ActiveDirectoryManagement_API.Models.Domain.SU
{
    public class SuUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        //[ForeignKey("EmpCode")]
        [StringLength(20)]
        public string EmpCode { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(20)]
        public string MobilePhoneNo { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        //[ForeignKey("ProfileCode")]
        [StringLength(20)]
        public string ProfileCode { get; set; }

        public bool Active { get; set; }

        [ForeignKey(nameof(EmpCode))]
        public DbEmployee? DbEmployee { get; set; }

        [ForeignKey(nameof(ProfileCode))]
        public SuProfile? SuProfile { get; set; }



        //public virtual DbEmployee Employee { get; set; }
        //public virtual SuProfile Profile { get; set; }
    }
}
