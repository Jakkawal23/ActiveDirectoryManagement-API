using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.DB
{
    public class DbDepartmentDTO
    {
        public string DepartmentCode { get; set; }

        public string DepartmentNameTH { get; set; }

        public string DepartmentNameEN { get; set; }

        public bool Active { get; set; }
    }
}
