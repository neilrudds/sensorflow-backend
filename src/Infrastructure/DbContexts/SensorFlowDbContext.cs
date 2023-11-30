using System.Reflection;
using SensorFlow.Domain.Entities.Persons;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using SensorFlow.Infrastructure.Services.DbInit;

/* Define our DBContext */
namespace SensorFlow.Infrastructure.DbContexts
{
    public class SensorFlowDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public SensorFlowDbContext() { }

        public SensorFlowDbContext(DbContextOptions<SensorFlowDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

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