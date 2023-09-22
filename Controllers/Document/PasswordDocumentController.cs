using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.Document;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using ActiveDirectoryManagement_API.Models.DTO.Document;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

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

        [HttpGet("List")]
        public IEnumerable<ChangePasswordList> GetPasswordList(string status)
        {
            List<ChangePasswordList> passwordLists = new List<ChangePasswordList>();

            if (status == null)
            {
                return Enumerable.Empty<ChangePasswordList>();
            }
            var passwords = dbContext.DocumentPasswords.Where(x => x.StatusCode == status).ToList();
            var employees = dbContext.DbEmployees.ToList();

            foreach (var p in passwords)
            {
                ChangePasswordList passwordList = new ChangePasswordList
                {
                    PasswordId = p.PasswordId,
                    EmpCode = p.EmpCode,
                    StatusCode = p.StatusCode,
                    ApproveEmpCode = p.ApproveEmpCode,
                    CreateDate = p.CreateDate,
                    ApproveDate = p.ApproveDate,
                };

                var employee = employees.FirstOrDefault(e => e.EmployeeCode == p.EmpCode);

                if (employee != null)
                {
                    passwordList.Name = employee.Name;
                    passwordList.LastName = employee.LastName;
                    passwordList.PositionCode = employee.PositionCode;
                    passwordList.DepartmentCode = employee.DepartmentCode;
                }

                passwordLists.Add(passwordList);
            }
            return passwordLists;
        }

        [HttpGet("Approve")]
        public DocumentPassword ApproveDocument(int documentId)
        {
            if (dbContext.DocumentPasswords.Where(x => x.PasswordId == documentId).FirstOrDefault() is DocumentPassword document)
            {
                document.StatusCode = "COMPLETE";
                dbContext.Set<DocumentPassword>().Attach(document);
                dbContext.SaveChanges();
                return document;
            }

            return null;
        }

        [HttpGet("Cancle")]
        public DocumentPassword CancleDocument(int documentId)
        {
            if (dbContext.DocumentPasswords.Where(x => x.PasswordId == documentId).FirstOrDefault() is DocumentPassword document)
            {
                document.StatusCode = "CANCEL";
                dbContext.Set<DocumentPassword>().Attach(document);
                dbContext.SaveChanges();
                return document;
            }

            return null;
        }

        [HttpGet("Master/Status")]
        public IEnumerable<DbStatus> GetStatus()
        {
            return dbContext.DbStatuses.Where(x => x.Active).ToList().OrderBy(x => x.StatusNameEN);
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
    }
}
