using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Infrastructure.DbContexts;
using SensorFlow.Infrastructure.Models.Identity;

namespace SensorFlow.Infrastructure.Services.DbInit
{
    internal class DatabaseInitService
    {
        private readonly ModelBuilder _modelBuilder;

        public DatabaseInitService(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            _modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole() { Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" },
                new ApplicationRole() { Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole() { Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString(), Name = "Owner", NormalizedName = "OWNER" });
        }
    }
}