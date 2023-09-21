namespace ActiveDirectoryManagement_API.Models.DTO.System
{
    public class LoginDTO
    {
        public class LoginRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

    }
}
