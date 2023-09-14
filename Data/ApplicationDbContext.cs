using ActiveDirectoryManagement_API.Models.Domain.DB;
using ActiveDirectoryManagement_API.Models.Domain.Document;
using ActiveDirectoryManagement_API.Models.Domain.SU;
using Microsoft.EntityFrameworkCore;

namespace ActiveDirectoryManagement_API.Data
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<SuUser> SuUsers { get; set; }
        public DbSet<SuProfile> SuProfiles { get; set; }
        public DbSet<DbEmployee> DbEmployees { get; set; }
        public DbSet<DbPosition> DbPositions { get; set; }
        public DbSet<DbDepartment> DbDepartments { get; set; }
        public DbSet<DbTeam> DbTeams { get; set; }
        public DbSet<DbStatus> DbStatuses { get; set; }
        public DbSet<DocumentPassword> DocumentPasswords { get; set; }

    }
}
