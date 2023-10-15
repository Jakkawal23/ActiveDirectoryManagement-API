using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static ActiveDirectoryManagement_API.Models.DTO.System.LoginDTO;
using System.Security.Cryptography;
using System.Text;
using Azure.Core;

namespace ActiveDirectoryManagement_API.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public LoginController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("IsUser")]
        public ActionResult<bool> Login(LoginRequest loginRequest)
        {
            string hashedPassword = HashPassword(loginRequest.Password);

            var user = dbContext.SuUsers.FirstOrDefault(x =>
                EF.Functions.Collate(x.UserName, "Latin1_General_BIN") == loginRequest.UserName &&
                EF.Functions.Collate(x.Password, "Latin1_General_BIN") == hashedPassword);

            if (user != null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpGet("EmployeeCode")]
        public IActionResult GetEmployeeCode(String UserName)
        {
            var user = dbContext.SuUsers.FirstOrDefault(x => x.UserName == UserName);
            if (user != null)
            {
                return Ok(new { EmployeeCode = user.EmpCode });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("EmployeeName")]
        public IActionResult GetEmployeeName(String EmployeeCode)
        {
            var user = dbContext.DbEmployees.FirstOrDefault(x => x.EmployeeCode == EmployeeCode);
            if (user != null)
            {
                return Ok(new { EmployeeName = user.FullName });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("EmployeeProfile")]
        public IActionResult GetEmployeeProfile(String UserName)
        {
            var user = dbContext.SuUsers.FirstOrDefault(x => x.UserName == UserName);
            if (user != null)
            {
                return Ok(new { profileCode = user.ProfileCode });
            }
            else
            {
                return NotFound();
            }
        }

        private string HashPassword(string password)
        {
            string hashedPassword;

            // Hash the new password using SHA-256
            byte[] newPasswordBytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedPasswordBytes = sha256.ComputeHash(newPasswordBytes);
                hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
            }
            return hashedPassword;
        }

    }
}
