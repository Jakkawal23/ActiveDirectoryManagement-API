using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.Document;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ActiveDirectoryManagement_API.Models.DTO.Document.DocumentManagementDTO;

namespace ActiveDirectoryManagement_API.Controllers.Document
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementDocumentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ManagementDocumentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<EmployeeListDTO> GetEmployee()
        {
            List<EmployeeListDTO> employeeLists = new List<EmployeeListDTO>();

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

        [HttpGet("EmployeeDetail")]
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

        [HttpGet("DocumentDetail")]
        public DocumentManagement GetDocumentDetail(string EmployeeCode)
        {
            DocumentManagement document = new DocumentManagement();
            document = dbContext.DocumentManagements.Where(x => x.EmpCode == EmployeeCode).FirstOrDefault();
            return document;
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ManageDocument>> ManageDocument(ManageDocument request)
        {
            if(dbContext.DocumentManagements.Where(x => x.EmpCode == request.EmpCode).FirstOrDefault() is DocumentManagement doc)
            {
                doc.Password = request.Password;
                dbContext.Set<DocumentManagement>().Attach(doc);
                dbContext.SaveChanges();
                return Ok(request);
            }

            var document = new DocumentManagement
            {
                EmpCode = request.EmpCode,
                Password = request.Password,
            };

            await dbContext.DocumentManagements.AddAsync(document);
            await dbContext.SaveChangesAsync();

            return Ok(request);
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
