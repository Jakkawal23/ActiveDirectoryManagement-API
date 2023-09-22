using ActiveDirectoryManagement_API.Models.Domain.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.Document
{
    public class ChangePassword
    {
        public int PasswordId { get; set; }

        public string EmpCode { get; set; }

        public string Password { get; set; }

        public string StatusCode { get; set; }

        public string ApproveEmpCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ApproveDate { get; set; }

    }

    public class ChangePasswordRequest
    {
        public string EmpCode { get; set; }

        public string Password { get; set; }

        public string StatusCode { get; set; }
    }

    public class ChangePasswordList
    {
        public int PasswordId { get; set; }

        public string EmpCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string PositionCode { get; set; }

        public string DepartmentCode { get; set; }

        //public string Password { get; set; }

        public string StatusCode { get; set; }

        public string? ApproveEmpCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ApproveDate { get; set; }

    }
}
