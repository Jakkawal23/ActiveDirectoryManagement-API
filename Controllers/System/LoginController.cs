using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ActiveDirectoryManagement_API.Models.DTO.System.LoginDTO;

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
            var user = dbContext.SuUsers.FirstOrDefault(x =>
                EF.Functions.Collate(x.UserName, "Latin1_General_BIN") == loginRequest.UserName &&
                EF.Functions.Collate(x.Password, "Latin1_General_BIN") == loginRequest.Password);

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

    }
}
