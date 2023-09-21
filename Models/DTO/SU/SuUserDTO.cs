using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.SU
{
    public class SuUserDTO
    {
        public string EmpCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string MobilePhoneNo { get; set; }

        public string Email { get; set; }

        public string ProfileCode { get; set; }

        public bool Active { get; set; }
    }
}
