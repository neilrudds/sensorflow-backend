using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SensorFlow.Infrastructure.Services.DbInit;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Entities.Dashboards;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SensorFlow.Domain.Models;
using SensorFlow.Infrastructure.Services.Auth;
using SensorFlow.Domain.Entities.Devices;
using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Entities.Gateways;

/* Define our DBContext */
namespace SensorFlow.Infrastructure.DbContexts
{
    // SensorFlowDbContext class will inherit the IdentityDbContext from .NET, However, It will use our Domain Layer User & Role types to extend functionaly. It will also inherit the .NET
    // IdentityUserLogin, IdentityRoleClaim & IdentityUserToken classes.
    public class SensorFlowDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
        UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private readonly UserResolverService _userResolverService; // Used to determine if a user session is authenticated
        public SensorFlowDbContext() { }

        public SensorFlowDbContext(DbContextOptions<SensorFlowDbContext> options, UserResolverService userResolverService) : base(options)
        {
            _userResolverService = userResolverService;
        }
        public DbSet<Tenant> Tenants { get; set; } // Create Tenants Table from Tenant Domain Entity
        public DbSet<Workspace> Workspaces { get; set; } // Create Workspace Table from Workspace Domain Entity
        public DbSet<Dashboard> Dashboards { get; set; } // Create Dashboard Table from Dashboard Domain Entity
        public DbSet<Device> Devices { get; set; } // Create Device Table from Dashboard Device Entity
        public DbSet<Gateway> Gateways { get; set; } // Create Gateway Table from Gateway Domain Entity

        // Override the OnConfiguring EntityFramework method to set the SQL Server connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql_server2022;Database=sensorflow;User=sa;Password=Sensorflow123;TrustServerCertificate=true;");
        }

        // Override the OnModelCreating method to set a default DB Schema, apply all Dataset Configurations from the Configurations folder and Seed the initial Data from the DatabaseInitService
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            new DatabaseInitService(builder).Seed();
        }

        // Override the SaveChangesAsync method to add functionality to automatically populate the OwnerId, LastModifiedTimestamp & ModifiedById fields in the base Entity.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changeSet = ChangeTracker.Entries<Entity<string>>();

            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    if (entry.Entity.OwnerId == null)
                        entry.Entity.OwnerId = _userResolverService.GetNameIdentifier();
                }

                foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                {
                    entry.Entity.LastModifiedTimestamp = DateTime.UtcNow;
                    entry.Entity.ModifiedById = _userResolverService.GetNameIdentifier();
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}