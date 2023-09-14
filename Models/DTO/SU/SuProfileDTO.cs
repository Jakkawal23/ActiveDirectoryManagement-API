using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.SU
{
    public class SuProfileDTO
    {
        public string ProfileCode { get; set; }

        public string ProfileNameTH { get; set; }

        public string ProfileNameEN { get; set; }

        public bool Active { get; set; }
    }
}
