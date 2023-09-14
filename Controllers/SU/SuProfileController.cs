using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using ActiveDirectoryManagement_API.Models.DTO.SU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActiveDirectoryManagement_API.Controllers.SU
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuProfileController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public SuProfileController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<SuProfile> GetProfiles()
        {
            return dbContext.SuProfiles.ToList().OrderBy(x => x.ProfileCode);
        }

        [HttpGet("Detail")]
        public SuProfile GetProfileById(int ProfileId)
        {
            SuProfile profile = new SuProfile();
            profile = dbContext.SuProfiles.Where(x => x.ProfileId == ProfileId).FirstOrDefault();
            return profile;
        }

        [HttpGet("Search")]
        public IEnumerable<SuProfile> SearchProfiles(string keyword)
        {
            var profile = dbContext.SuProfiles
                .Where(x => x.ProfileCode.Contains(keyword) ||
                            x.ProfileNameTH.Contains(keyword) ||
                            x.ProfileNameEN.Contains(keyword) ||
                            (x.Active == true && (keyword == "true" || keyword == "1")) ||
                            (x.Active == false && (keyword == "false" || keyword == "0")))
                .OrderBy(x => x.ProfileCode)
                .ToList();

            return profile;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProfile(SuProfileDTO request)
        {
            if (dbContext.SuProfiles.Where(x => x.ProfileCode == request.ProfileCode).FirstOrDefault() is SuProfile profileOld)
            {
                SuProfile profileNull = new SuProfile();
                return Ok(profileNull);
            }
            var profile = new SuProfile
            {
                ProfileCode = request.ProfileCode,
                ProfileNameTH = request.ProfileNameTH,
                ProfileNameEN = request.ProfileNameEN,
                Active = request.Active,
            };

            await dbContext.SuProfiles.AddAsync(profile);
            await dbContext.SaveChangesAsync();

            return Ok(profile);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<SuProfile>> EditProfile(SuProfile request)
        {
            if (dbContext.SuProfiles.Where(x => x.ProfileId == request.ProfileId).FirstOrDefault() is SuProfile profile)
            {
                profile.ProfileNameTH = request.ProfileNameTH;
                profile.ProfileNameEN = request.ProfileNameEN;
                profile.Active = request.Active;
                dbContext.Set<SuProfile>().Attach(profile);
                dbContext.SaveChanges();
                return profile;
            }
            //return "Error 500";
            return StatusCode(StatusCodes.Status500InternalServerError, "Error editing profile");
        }

        [HttpDelete("Delete")]
        public String DeleteProfile(int ProfileId)
        {
            if (dbContext.SuProfiles.Where(x => x.ProfileId == ProfileId).FirstOrDefault() is SuProfile profile)
            {
                dbContext.SuProfiles.Remove(profile);
                dbContext.SaveChanges();
                return ProfileId.ToString();
            }
            return "Error delete profile";
        }

    }
}
