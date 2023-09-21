using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProfileController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeProfileController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("Detail")]
        public ProfileDetailDTO GetEmployeeDetail(string EmployeeCode)
        {
        ProfileDetailDTO profileDetail = new ProfileDetailDTO();

            var employees = dbContext.DbEmployees.Where(x => x.EmployeeCode == EmployeeCode).FirstOrDefault();
            var users = dbContext.SuUsers.Where(x => x.EmpCode == employees.EmployeeCode).FirstOrDefault();

            profileDetail.EmployeeCode = employees.EmployeeCode;
            profileDetail.Name = employees.Name;
            profileDetail.LastName = employees.LastName;
            profileDetail.PositionCode = employees.PositionCode;
            profileDetail.DepartmentCode = employees.DepartmentCode;
            profileDetail.TeamCode = employees.TeamCode;


            if (users != null)
            {
                profileDetail.UserName = users.UserName;
                profileDetail.MobilePhoneNo = users.MobilePhoneNo;
                profileDetail.Email = users.Email;
                profileDetail.ProfileCode = users.ProfileCode;
                profileDetail.Active = users.Active;
            }
            else
            {
                return null;
            }

            return profileDetail;
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
