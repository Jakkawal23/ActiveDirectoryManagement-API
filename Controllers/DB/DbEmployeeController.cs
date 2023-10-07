using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbEmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DbEmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEmployee(EmployeeSaveDTO request)
        {
            if (dbContext.DbEmployees.Where(x => x.EmployeeCode == request.EmployeeCode).FirstOrDefault() is DbEmployee Employees)
            {
                DbEmployee employeesOld = new DbEmployee();
                return Ok(employeesOld);
            }
            var employee = new DbEmployee
            {
                EmployeeCode = request.EmployeeCode,
                PositionCode = request.PositionCode,
                DepartmentCode = request.DepartmentCode,
                TeamCode = request.TeamCode,
                Name = request.Name,
                LastName = request.LastName,
                FullName = request.Name + "  " + request.LastName,
            };

            var user = new SuUser
            {
                EmpCode = request.EmployeeCode,
                UserName = request.EmployeeCode,
                Password = request.Password,
                MobilePhoneNo = request.MobilePhoneNo,
                Email = request.Email,
                ProfileCode = "EMP",
                Active = true
            };

            await dbContext.DbEmployees.AddAsync(employee);
            await dbContext.SuUsers.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut("Create/Edit")]
        public async Task<ActionResult<EmployeeSaveDTO>> EditEmployee(EmployeeSaveDTO request)
        {
            if (dbContext.DbEmployees.Where(x => x.EmployeeCode == request.EmployeeCode).FirstOrDefault() is DbEmployee employee)
            {
                employee.PositionCode = request.PositionCode;
                employee.DepartmentCode = request.DepartmentCode;
                employee.TeamCode = request.TeamCode;
                employee.Name = request.Name;
                employee.LastName = request.LastName;
                employee.FullName = request.Name + "  " + request.LastName;

                dbContext.Set<DbEmployee>().Attach(employee);

                if (dbContext.SuUsers.Where(x => x.EmpCode == request.EmployeeCode).FirstOrDefault() is SuUser user)
                {
                    //user.EmpCode = request.EmployeeCode;
                    user.UserName = request.EmployeeCode;
                    user.Password = request.Password;
                    user.MobilePhoneNo = request.MobilePhoneNo;
                    user.Email = request.Email;
                    //user.ProfileCode = "Admin";
                    //user.Active = true;
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
    }
}
