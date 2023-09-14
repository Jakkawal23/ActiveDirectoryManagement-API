using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.DB
{
    public class DbTeamDTO
    {
        public string TeamCode { get; set; }

        public string TeamNameTH { get; set; }

        public string TeamNameEN { get; set; }

        public bool Active { get; set; }
    }
}
