using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbStatusController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DbStatusController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<DbStatus> GetStatuses()
        {
            return dbContext.DbStatuses.ToList().OrderBy(x => x.StatusCode);
        }

        [HttpGet("Detail")]
        public DbStatus GetStatusById(int StatusId)
        {
            DbStatus Status = new DbStatus();
            Status = dbContext.DbStatuses.Where(x => x.StatusId == StatusId).FirstOrDefault();
            return Status;
        }

        [HttpGet("Search")]
        public IEnumerable<DbStatus> SearchStatuses(string keyword)
        {
            var status = dbContext.DbStatuses
                .Where(x => x.StatusCode.Contains(keyword) ||
                            x.StatusNameTH.Contains(keyword) ||
                            x.StatusNameEN.Contains(keyword) ||
                            (x.Active == true && (keyword == "true" || keyword == "1")) ||
                            (x.Active == false && (keyword == "false" || keyword == "0")))
                .OrderBy(x => x.StatusCode)
                .ToList();

            return status;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateStatus(DBStatusDTO request)
        {
            if (dbContext.DbStatuses.Where(x => x.StatusCode == request.StatusCode).FirstOrDefault() is DbStatus StatusOld)
            {
                DbStatus statusNull = new DbStatus();
                return Ok(statusNull);
            }
            var Status = new DbStatus
            {
                StatusCode = request.StatusCode,
                StatusNameTH = request.StatusNameTH,
                StatusNameEN = request.StatusNameEN,
                Active = request.Active,
            };

            await dbContext.DbStatuses.AddAsync(Status);
            await dbContext.SaveChangesAsync();

            return Ok(Status);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<DbStatus>> EditStatus(DbStatus request)
        {
            if (dbContext.DbStatuses.Where(x => x.StatusId == request.StatusId).FirstOrDefault() is DbStatus Status)
            {
                Status.StatusNameTH = request.StatusNameTH;
                Status.StatusNameEN = request.StatusNameEN;
                Status.Active = request.Active;
                dbContext.Set<DbStatus>().Attach(Status);
                dbContext.SaveChanges();
                return Status;
            }
            //return "Error 500";
            return StatusCode(StatusCodes.Status500InternalServerError, "Error editing Status");
        }

        [HttpDelete("Delete")]
        public String DeleteStatus(int StatusId)
        {
            if (dbContext.DbStatuses.Where(x => x.StatusId == StatusId).FirstOrDefault() is DbStatus Status)
            {
                dbContext.DbStatuses.Remove(Status);
                dbContext.SaveChanges();
                return StatusId.ToString();
            }
            return "Error delete Status";
        }

    }
}
