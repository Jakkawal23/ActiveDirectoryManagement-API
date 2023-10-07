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
using System.Reflection.Emit;

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

            if (request.StatusCode == "COMPLETE")
            {
                documentPassword.ApproveEmpCode = request.EmpCode;
                documentPassword.ApproveDate = DateTime.Now;
            }

            await dbContext.DocumentPasswords.AddAsync(documentPassword);
            await dbContext.SaveChangesAsync();

            return Ok(documentPassword);
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
            return passwordLists.OrderByDescending(x => x.CreateDate);
        }

        [HttpGet("Document")]
        public IEnumerable<ChangePasswordList> GetPasswordDocument(string empCode)
        {
            List<ChangePasswordList> passwordLists = new List<ChangePasswordList>();

            if (empCode == null)
            {
                return Enumerable.Empty<ChangePasswordList>();
            }
            var passwords = dbContext.DocumentPasswords.Where(x => x.EmpCode == empCode).ToList();
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
            return passwordLists.OrderByDescending(x => x.CreateDate);
        }

        [HttpDelete("DeleteDocument")]
        public String DeletePassDocument(int passwordId)
        {
            if (dbContext.DocumentPasswords.Where(x => x.PasswordId == passwordId).FirstOrDefault() is DocumentPassword docPass)
            {
                dbContext.DocumentPasswords.Remove(docPass);
                dbContext.SaveChanges();
                return passwordId.ToString();
            }
            return "Error delete password docuent";
        }

        [HttpGet("Approve")]
        public DocumentPassword ApproveDocument(int documentId,string empCode)
        {
            if (dbContext.DocumentPasswords.Where(x => x.PasswordId == documentId).FirstOrDefault() is DocumentPassword document)
            {
                document.StatusCode = "COMPLETE";
                document.ApproveEmpCode = empCode;
                document.ApproveDate = DateTime.Now;
                dbContext.Set<DocumentPassword>().Attach(document);
                dbContext.SaveChanges();
                return document;
            }

            return null;
        }

        [HttpGet("Cancle")]
        public DocumentPassword CancleDocument(int documentId, string empCode)
        {
            if (dbContext.DocumentPasswords.Where(x => x.PasswordId == documentId).FirstOrDefault() is DocumentPassword document)
            {
                document.StatusCode = "CANCEL";
                document.ApproveEmpCode = empCode;
                document.ApproveDate = DateTime.Now;
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
