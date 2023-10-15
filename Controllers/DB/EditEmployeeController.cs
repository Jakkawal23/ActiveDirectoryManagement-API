using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditEmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EditEmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("Detail")]
        public EditEmployeeDTO GetEmployeeDetail(string EmployeeCode)
        {
            EditEmployeeDTO employeeDetail = new EditEmployeeDTO();

            var employees = dbContext.DbEmployees.Where(x => x.EmployeeCode == EmployeeCode).FirstOrDefault();
            var users = dbContext.SuUsers.Where(x => x.EmpCode == employees.EmployeeCode).FirstOrDefault();

            employeeDetail.EmployeeCode = employees.EmployeeCode;
            employeeDetail.Name = employees.Name;
            employeeDetail.LastName = employees.LastName;
            employeeDetail.PositionCode = employees.PositionCode;
            employeeDetail.DepartmentCode = employees.DepartmentCode;
            employeeDetail.TeamCode = employees.TeamCode;


            if (users != null)
            {
                employeeDetail.UserName = users.UserName;
                employeeDetail.Password = users.Password;
                employeeDetail.MobilePhoneNo = users.MobilePhoneNo;
                employeeDetail.Email = users.Email;
            }
            else
            {
                return null;
            }

            return employeeDetail;
        }


        [HttpPut("Edit")]
        public async Task<ActionResult<EditEmployeeDTO>> EditEmployee(EditEmployeeDTO request)
        {
            if (dbContext.SuUsers.Where(x => EF.Functions.Collate(x.UserName, "Latin1_General_BIN") == request.UserName).FirstOrDefault() is SuUser oldUser)
            {
                if(request.EmployeeCode != oldUser.EmpCode)
                {
                    request.UserName = null;
                    return request;
                }
            }
                if (dbContext.DbEmployees.Where(x => x.EmployeeCode == request.EmployeeCode).FirstOrDefault() is DbEmployee employee)
            {
                employee.Name = request.Name;
                employee.LastName = request.LastName;
                employee.FullName = request.Name + "  " + request.LastName;

                dbContext.Set<DbEmployee>().Attach(employee);

                string hashedPassword = HashPassword(request.Password);

                if (dbContext.SuUsers.Where(x => x.EmpCode == request.EmployeeCode).FirstOrDefault() is SuUser user)
                {
                    //user.EmpCode = request.EmployeeCode;
                    //user.UserName = request.UserName;
                    user.Password = hashedPassword;
                    user.MobilePhoneNo = request.MobilePhoneNo;
                    user.Email = request.Email;
                    dbContext.Set<SuUser>().Attach(user);
                    dbContext.SaveChanges();
                    return request;
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error editing employee");
            }

            //return "Error 500";
            return StatusCode(StatusCodes.Status500InternalServerError, "Error editing employee");
        }

        [HttpGet("Master/Positions")]
        public IEnumerable<DbPosition> GetPositions()
        {
            return dbContext.DbPositions.Where(x => x.Active).ToList().OrderBy(x => x.PositionNameEN);
        }

        [HttpGet("Master/Departments")]
        public IEnumerable<DbDepartment> GetDepartments()
        {
            return dbContext.DbDepartments.Where(x => x.Active).ToList().OrderBy(x => x.DepartmentNameEN);
        }

        [HttpGet("Master/Teams")]
        public IEnumerable<DbTeam> GetTeams()
        {
            return dbContext.DbTeams.Where(x => x.Active).ToList().OrderBy(x => x.TeamNameEN);
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
