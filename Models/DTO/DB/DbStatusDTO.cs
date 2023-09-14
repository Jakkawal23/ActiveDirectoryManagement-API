using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.DB
{
    public class DBStatusDTO
    {
        public string StatusCode { get; set; }

        public string StatusNameTH { get; set; }

        public string StatusNameEN { get; set; }

        public bool Active { get; set; }
    }
}
