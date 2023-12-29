using System.Reflection;
using SensorFlow.Domain.Entities.Persons;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using SensorFlow.Infrastructure.Services.DbInit;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Entities.Dashboards;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SensorFlow.Domain.Models;
using Microsoft.AspNetCore.Http;
using SensorFlow.Infrastructure.Services.Auth;
using SensorFlow.Domain.Entities.Devices;

/* Define our DBContext */
namespace SensorFlow.Infrastructure.DbContexts
{
    public class SensorFlowDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private readonly UserResolverService _userResolverService;
        public SensorFlowDbContext() { }

        public SensorFlowDbContext(DbContextOptions<SensorFlowDbContext> options, UserResolverService userResolverService) : base(options)
        {
            _userResolverService = userResolverService;
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Device> Devices { get; set; }

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changeSet = ChangeTracker.Entries<Entity<string>>();

            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    if (entry.Entity.OwnerId == null)
                        entry.Entity.OwnerId = _userResolverService.GetNameIdentifier();

                    entry.Entity.LastModifiedTimestamp = DateTime.UtcNow;
                    entry.Entity.ModifiedById = _userResolverService.GetNameIdentifier();
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}