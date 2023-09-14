using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.Domain.DB
{
    public class DbPosition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PositionId { get; set; }

        [Key]
        [StringLength(20)]
        public string PositionCode { get; set; }

        [StringLength(200)]
        public string PositionNameTH { get; set; }

        [StringLength(200)]
        public string PositionNameEN { get; set; }

        public bool Active { get; set; }
    }
}
