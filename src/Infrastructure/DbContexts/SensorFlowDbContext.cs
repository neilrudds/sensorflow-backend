using System.Reflection;
using SensorFlow.Domain.Entities.Persons;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using SensorFlow.Infrastructure.Services.DbInit;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Entities.Dashboards;
using System.Reflection.Emit;

/* Define our DBContext */
namespace SensorFlow.Infrastructure.DbContexts
{
    public class SensorFlowDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public SensorFlowDbContext() { }

        public SensorFlowDbContext(DbContextOptions<SensorFlowDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=sensorflow;User=sa;Password=Sensorflow123;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            new DatabaseInitService(builder).Seed();
        }

        
    }
}