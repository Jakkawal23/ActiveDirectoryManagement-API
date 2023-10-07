namespace ActiveDirectoryManagement_API.Models.DTO.Document
{
    public class DocumentManagementDTO
    {
        public class ManageDocument
        {
            public int ManagementId { get; set; }

            public string EmpCode { get; set; }

            public bool Password { get; set; }
        }
    }
}
