using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using ActiveDirectoryManagement_API.Models.DTO.SU;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbDepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DbDepartmentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<DbDepartment> GetDepartments()
        {
            return dbContext.DbDepartments.ToList().OrderBy(x=> x.DepartmentCode);
        }

        [HttpGet("Detail")]
        public DbDepartment GetDepartmentById(int DepartmentId)
        {
            DbDepartment department = new DbDepartment();
            department = dbContext.DbDepartments.Where(x => x.DepartmentId == DepartmentId).FirstOrDefault();
            return department;
        }

        [HttpGet("Search")]
        public IEnumerable<DbDepartment> SearchDepartments(string keyword)
        {
            var department = dbContext.DbDepartments
                .Where(x => x.DepartmentCode.Contains(keyword) ||
                            x.DepartmentNameTH.Contains(keyword)||
                            x.DepartmentNameEN.Contains(keyword)||
                            (x.Active == true && (keyword == "true" || keyword == "1"))||
                            (x.Active == false && (keyword == "false" || keyword == "0")))
                .OrderBy(x => x.DepartmentCode)
                .ToList();

            return department;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateDepartment(DbDepartmentDTO request)
        {
            if (dbContext.DbDepartments.Where(x => x.DepartmentCode == request.DepartmentCode).FirstOrDefault() is DbDepartment departmentOld)
            {
                DbDepartment departmentNull = new DbDepartment();
                return Ok(departmentNull);
            }
            var department = new DbDepartment
            {
                DepartmentCode = request.DepartmentCode,
                DepartmentNameTH = request.DepartmentNameTH,
                DepartmentNameEN = request.DepartmentNameEN,
                Active = request.Active,
            };

            await dbContext.DbDepartments.AddAsync(department);
            await dbContext.SaveChangesAsync();

            return Ok(department);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<DbDepartment>> EditDepartment(DbDepartment request)
        {
            if (dbContext.DbDepartments.Where(x => x.DepartmentId == request.DepartmentId).FirstOrDefault() is DbDepartment department)
            {
                department.DepartmentNameTH = request.DepartmentNameTH;
                department.DepartmentNameEN = request.DepartmentNameEN;
                department.Active = request.Active;
                dbContext.Set<DbDepartment>().Attach(department);
                dbContext.SaveChanges();
                return department;
            }
            //return "Error 500";
            return StatusCode(StatusCodes.Status500InternalServerError, "Error editing department");
        }

        [HttpDelete("Delete")]
        public String DeleteDepartment(int DepartmentId)
        {
            if (dbContext.DbDepartments.Where(x => x.DepartmentId == DepartmentId).FirstOrDefault() is DbDepartment department)
            {
                dbContext.DbDepartments.Remove(department);
                dbContext.SaveChanges();
                return DepartmentId.ToString();
            }
            return "Error delete department";
        }

    }
}
