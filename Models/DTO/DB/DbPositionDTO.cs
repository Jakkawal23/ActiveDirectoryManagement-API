using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryManagement_API.Models.DTO.DB
{
    public class DbPositionDTO
    {
        public string PositionCode { get; set; }

        public string PositionNameTH { get; set; }

        public string PositionNameEN { get; set; }

        public bool Active { get; set; }
    }
}
