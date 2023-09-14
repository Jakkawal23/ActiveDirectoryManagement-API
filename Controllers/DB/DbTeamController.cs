using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.DTO.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActiveDirectoryManagement_API.Controllers.DB
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbTeamController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DbTeamController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<DbTeam> GetTeams()
        {
            return dbContext.DbTeams.ToList().OrderBy(x => x.TeamCode);
        }

        [HttpGet("Detail")]
        public DbTeam GetTeamById(int TeamId)
        {
            DbTeam team = new DbTeam();
            team = dbContext.DbTeams.Where(x => x.TeamId == TeamId).FirstOrDefault();
            return team;
        }

        [HttpGet("Search")]
        public IEnumerable<DbTeam> SearchTeams(string keyword)
        {
            var team = dbContext.DbTeams
                .Where(x => x.TeamCode.Contains(keyword) ||
                            x.TeamNameTH.Contains(keyword) ||
                            x.TeamNameEN.Contains(keyword) ||
                            (x.Active == true && (keyword == "true" || keyword == "1")) ||
                            (x.Active == false && (keyword == "false" || keyword == "0")))
                .OrderBy(x => x.TeamCode)
                .ToList();

            return team;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTeam(DbTeamDTO request)
        {
            if (dbContext.DbTeams.Where(x => x.TeamCode == request.TeamCode).FirstOrDefault() is DbTeam teamOld)
            {
                DbTeam teamNull = new DbTeam();
                return Ok(teamNull);
            }
            var team = new DbTeam
            {
                TeamCode = request.TeamCode,
                TeamNameTH = request.TeamNameTH,
                TeamNameEN = request.TeamNameEN,
                Active = request.Active,
            };

            await dbContext.DbTeams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            return Ok(team);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<DbTeam>> EditTeam(DbTeam request)
        {
            if (dbContext.DbTeams.Where(x => x.TeamId == request.TeamId).FirstOrDefault() is DbTeam team)
            {
                team.TeamNameTH = request.TeamNameTH;
                team.TeamNameEN = request.TeamNameEN;
                team.Active = request.Active;
                dbContext.Set<DbTeam>().Attach(team);
                dbContext.SaveChanges();
                return team;
            }
            //return "Error 500";
            return StatusCode(StatusCodes.Status500InternalServerError, "Error editing team");
        }

        [HttpDelete("Delete")]
        public String DeleteTeam(int TeamId)
        {
            if (dbContext.DbTeams.Where(x => x.TeamId == TeamId).FirstOrDefault() is DbTeam team)
            {
                dbContext.DbTeams.Remove(team);
                dbContext.SaveChanges();
                return TeamId.ToString();
            }
            return "Error delete team";
        }
        
    }
}
