using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.Document;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using ActiveDirectoryManagement_API.Models.DTO.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActiveDirectoryManagement_API.Controllers.Document
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordDocumentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public PasswordDocumentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpPost("Change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
            {
                return Ok("Error: Invalid password");
            }

            var documentPassword = new DocumentPassword
            {
                EmpCode = request.EmpCode,
                Password = request.Password,
                StatusCode = request.StatusCode,
                CreateDate = DateTime.Now,
                ApproveEmpCode = null,
                ApproveDate = null,
            };

            await dbContext.DocumentPasswords.AddAsync(documentPassword);
            await dbContext.SaveChangesAsync();

            return Ok(request);
        }
    }
}
