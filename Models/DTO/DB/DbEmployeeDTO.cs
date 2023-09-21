using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.DB
{
    public class DbEmployeeDTO
    {
        public string EmployeeCode { get; set; }

        public string PositionCode { get; set; }

        public string DepartmentCode { get; set; }

        public string TeamCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
    }

    public class EmployeeSaveDTO
    {
        public string EmployeeCode { get; set; }

        public string PositionCode { get; set; }

        public string DepartmentCode { get; set; }

        public string TeamCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string MobilePhoneNo { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }
    }

    public class EmployeeListDTO
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string PositionCode { get; set; }

        public string DepartmentCode { get; set; }

        //public string TeamCode { get; set; }

        public string ProfileCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public bool Active { get; set; }
    }

    public class EmployeeDetailDTO
    {
        //Employee
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string PositionCode { get; set; }

        public string DepartmentCode { get; set; }

        public string TeamCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        //User
        public string UserName { get; set; }

        public string Password { get; set; }

        public string MobilePhoneNo { get; set; }

        public string Email { get; set; }

        public string ProfileCode { get; set; }

        public bool Active { get; set; }
    }

    public class ProfileDetailDTO
    {
        //Employee
        //public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string PositionCode { get; set; }

        public string DepartmentCode { get; set; }

        public string TeamCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        //User
        public string UserName { get; set; }

        //public string Password { get; set; }

        public string MobilePhoneNo { get; set; }

        public string Email { get; set; }

        public string ProfileCode { get; set; }

        public bool Active { get; set; }
    }

    public class EditEmployeeDTO
    {
        public string EmployeeCode { get; set; }

        public string PositionCode { get; set; }

        public string DepartmentCode { get; set; }

        public string TeamCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        //User
        public string UserName { get; set; }

        public string MobilePhoneNo { get; set; }

        public string Email { get; set; }
    }
}
