using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbEmployeeManagementController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DbEmployeeManagementController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<EmployeeListDTO> GetEmployee()
        {
            List<EmployeeListDTO> employeeLists =new List<EmployeeListDTO>();

            var employees = dbContext.DbEmployees.ToList();
            var users = dbContext.SuUsers.ToList();

            foreach (var e in employees)
            {
                EmployeeListDTO employeeList = new EmployeeListDTO
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeCode = e.EmployeeCode,
                    Name = e.Name,
                    LastName = e.LastName,
                    PositionCode = e.PositionCode,
                    DepartmentCode = e.DepartmentCode,
                    //TeamCode = e.TeamCode
                };

                var user = users.FirstOrDefault(u => u.EmpCode == e.EmployeeCode);

                if (user != null)
                {
                    employeeList.Active = user.Active;
                    employeeList.ProfileCode = user.ProfileCode;
                }

                employeeLists.Add(employeeList);
            }
            return employeeLists;
        }

        [HttpGet("Detail")]
        public EmployeeDetailDTO GetEmployeeDetail(int EmployeeId)
        {
            EmployeeDetailDTO employeeDetail = new EmployeeDetailDTO();

            var employees = dbContext.DbEmployees.Where(x => x.EmployeeId == EmployeeId).FirstOrDefault();
            var users = dbContext.SuUsers.Where(x => x.EmpCode == employees.EmployeeCode).FirstOrDefault();

            employeeDetail.EmployeeId = employees.EmployeeId;
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
                employeeDetail.ProfileCode = users.ProfileCode;
                employeeDetail.Active = users.Active;
            }
            else
            {
                return null;
            }

            return employeeDetail;
        }

        [HttpGet("Search")]
        public IEnumerable<EmployeeListDTO> SearchEmployees(string keyword)
        {
            List<EmployeeListDTO> employeeLists = new List<EmployeeListDTO>();

            var positions = dbContext.DbPositions.Where(x => x.PositionNameEN.Contains(keyword)).Select(x => x.PositionCode).ToList();
            var departments = dbContext.DbDepartments.Where(x => x.DepartmentNameEN.Contains(keyword)).Select(x => x.DepartmentCode).ToList();
            var profiles = dbContext.SuProfiles.Where(x => x.ProfileNameEN.Contains(keyword)).Select(x => x.ProfileCode).ToList();
            var users = dbContext.SuUsers.Where(x => (x.Active == true && (keyword == "true" || keyword == "1")) ||
                                                     (x.Active == false && (keyword == "false" || keyword == "0")) ||
                                                     profiles.Contains(x.ProfileCode)).Select(x => x.EmpCode).ToList();

            var employees = dbContext.DbEmployees
                .Where(x => x.EmployeeCode.Contains(keyword) ||
                            x.Name.Contains(keyword) ||
                            x.LastName.Contains(keyword) ||
                            positions.Contains(x.PositionCode) ||
                            departments.Contains(x.DepartmentCode) ||
                            users.Contains(x.EmployeeCode))
                .ToList();

            var userLists = dbContext.SuUsers.ToList();

            foreach (var e in employees)
            {
                EmployeeListDTO employeeList = new EmployeeListDTO
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeCode = e.EmployeeCode,
                    Name = e.Name,
                    LastName = e.LastName,
                    PositionCode = e.PositionCode,
                    DepartmentCode = e.DepartmentCode,
                    //TeamCode = e.TeamCode
                };

                var user = userLists.FirstOrDefault(u => u.EmpCode == e.EmployeeCode);

                if (user != null)
                {
                    employeeList.Active = user.Active;
                    employeeList.ProfileCode = user.ProfileCode;
                }

                employeeLists.Add(employeeList);
            }
            return employeeLists;
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<EmployeeDetailDTO>> EditEmployeeDetail(EmployeeDetailDTO request)
        {
            if (dbContext.DbEmployees.Where(x => x.EmployeeId == request.EmployeeId).FirstOrDefault() is DbEmployee employee)
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
                    user.UserName = request.UserName;
                    user.Password = request.Password;
                    user.MobilePhoneNo = request.MobilePhoneNo;
                    user.Email = request.Email;
                    user.ProfileCode = request.ProfileCode;
                    user.Active = request.Active;
                    dbContext.Set<SuUser>().Attach(user);
                    dbContext.SaveChanges();
                    return request;
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error editing employee detil");
            }

            //return "Error 500";
            return StatusCode(StatusCodes.Status500InternalServerError, "Error editing employee detail");
        }

        [HttpDelete("Delete")]
        public String DeleteEmployee(int EmployeeId)
        {
            if (dbContext.DbEmployees.Where(x => x.EmployeeId == EmployeeId).FirstOrDefault() is DbEmployee employee)
            {
                if(dbContext.SuUsers.Where(x => x.EmpCode == employee.EmployeeCode).FirstOrDefault() is SuUser user)
                {
                    dbContext.DbEmployees.Remove(employee);
                    dbContext.SuUsers.Remove(user);
                    dbContext.SaveChanges();
                }
                return EmployeeId.ToString();
            }
            return "Error delete department";
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

        [HttpGet("Master/Profiles")]
        public IEnumerable<SuProfile> GetProfiles()
        {
            return dbContext.SuProfiles.Where(x => x.Active).ToList().OrderBy(x => x.ProfileNameEN);
        }
    }
}
