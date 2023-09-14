using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbPositionController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DbPositionController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<DbPosition> GetPositions()
        {
            return dbContext.DbPositions.ToList().OrderBy(x => x.PositionCode);
        }

        [HttpGet("Detail")]
        public DbPosition GetPositionById(int PositionId)
        {
            DbPosition position = new DbPosition();
            position = dbContext.DbPositions.Where(x => x.PositionId == PositionId).FirstOrDefault();
            return position;
        }

        [HttpGet("Search")]
        public IEnumerable<DbPosition> SearchPositions(string keyword)
        {
            var position = dbContext.DbPositions
                .Where(x => x.PositionCode.Contains(keyword) ||
                            x.PositionNameTH.Contains(keyword) ||
                            x.PositionNameEN.Contains(keyword) ||
                            (x.Active == true && (keyword == "true" || keyword == "1")) ||
                            (x.Active == false && (keyword == "false" || keyword == "0")))
                .OrderBy(x => x.PositionCode)
                .ToList();

            return position;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePosition(DbPositionDTO request)
        {
            if (dbContext.DbPositions.Where(x => x.PositionCode == request.PositionCode).FirstOrDefault() is DbPosition positionOld)
            {
                DbPosition positionNull = new DbPosition();
                return Ok(positionNull);
            }
            var position = new DbPosition
            {
                PositionCode = request.PositionCode,
                PositionNameTH = request.PositionNameTH,
                PositionNameEN = request.PositionNameEN,
                Active = request.Active,
            };

            await dbContext.DbPositions.AddAsync(position);
            await dbContext.SaveChangesAsync();

            return Ok(position);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<DbPosition>> EditPosition(DbPosition request)
        {
            if (dbContext.DbPositions.Where(x => x.PositionId == request.PositionId).FirstOrDefault() is DbPosition position)
            {
                position.PositionNameTH = request.PositionNameTH;
                position.PositionNameEN = request.PositionNameEN;
                position.Active = request.Active;
                dbContext.Set<DbPosition>().Attach(position);
                dbContext.SaveChanges();
                return position;
            }
            //return "Error 500";
            return StatusCode(StatusCodes.Status500InternalServerError, "Error editing position");
        }

        [HttpDelete("Delete")]
        public String DeletePosition(int PositionId)
        {
            if (dbContext.DbPositions.Where(x => x.PositionId == PositionId).FirstOrDefault() is DbPosition position)
            {
                dbContext.DbPositions.Remove(position);
                dbContext.SaveChanges();
                return PositionId.ToString();
            }
            return "Error delete position";
        }
    }
}
