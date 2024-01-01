using Microsoft.EntityFrameworkCore;
using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Users;

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
            _modelBuilder.Entity<Role>().HasData(
                new Role() { Id = "51ba0c4e-0772-4b90-808e-c7dad3fe4342", ConcurrencyStamp = "f6759e18-32d3-4dfc-a58e-c2c47819b366", Name = "User", NormalizedName = "USER" },
                new Role() { Id = "08aed62a-7b78-47cf-9472-fa755c04f241", ConcurrencyStamp = "b1f5336e-cad3-4c51-b025-d810d6942db5", Name = "Admin", NormalizedName = "ADMIN" },
                new Role() { Id = "29d7ebeb-6706-460b-993a-152edfd19efd", ConcurrencyStamp = "6e95fe98-be6d-4405-ac14-67b4746e724f", Name = "Owner", NormalizedName = "OWNER" });

            _modelBuilder.Entity<Tenant>().HasData(
                new Tenant() { Id = "7ec39a7f-fe7e-4dd0-9f42-d187562e9875", Name = "Neil Rutherford", CreatedTimestamp = new DateTime(2023, 12, 31, 13, 54, 48, 237, DateTimeKind.Utc).AddTicks(2369) }
                );

            _modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = "a0dd767b-908d-42de-84f5-b55a68920a04",
                    FirstName = "Neil",
                    LastName = "Rutherford",
                    IsActive = true,
                    UserName = "neilr@hotmail.com",
                    NormalizedUserName = "NEILR@HOTMAIL.COM",
                    Email = "neilr@hotmail.com",
                    NormalizedEmail = "NEILR@HOTMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAELaguSZ3I+utb3vgRy/lnU+XTfVT9R/F4+roSf3859lrthJ+hphjmcrikWdYNpMA1Q==",
                    SecurityStamp = "2JKC7MM4IT23GQA2C26EYIXLHGIIFXUH",
                    ConcurrencyStamp = "7e821b98-e164-4396-9713-8c30457265d9",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TenantId = "7ec39a7f-fe7e-4dd0-9f42-d187562e9875"
                });

            _modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { UserId = "a0dd767b-908d-42de-84f5-b55a68920a04", RoleId = "29d7ebeb-6706-460b-993a-152edfd19efd" });
        }
    }
}