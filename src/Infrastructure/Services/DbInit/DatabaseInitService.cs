using Microsoft.EntityFrameworkCore;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Entities.Workspaces;

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

            //_modelBuilder.Entity<User>().HasData(
            //    new User()
            //    {
            //        Id = "a0dd767b-908d-42de-84f5-b55a68920g76",
            //        FirstName = "Guest",
            //        LastName = "User",
            //        IsActive = false,
            //        UserName = "guest@sensorflow.com",
            //        NormalizedUserName = "GUEST@SENSORFLOW.COM",
            //        Email = "guest@sensorflow.com",
            //        NormalizedEmail = "GUEST@SENSORFLOW.COM",
            //        EmailConfirmed = false,
            //        PasswordHash = "AQAAAAIAAYagAAAAELaguSZ3I+utb3vgRy/lnU+XTfVT9R/F4+roSf3859lrthJ+hphjmcrikWdYNpMA1Q==",
            //        SecurityStamp = "2JKC7MM4IT23GQA2C26EYIXLHGIIFXUH",
            //        ConcurrencyStamp = "7e821b98-e164-4396-9713-8c30457265d9",
            //        PhoneNumberConfirmed = false,
            //        TwoFactorEnabled = false,
            //        LockoutEnabled = true,
            //        AccessFailedCount = 0,
            //        TenantId = ""
            //    });

            _modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { UserId = "a0dd767b-908d-42de-84f5-b55a68920a04", RoleId = "29d7ebeb-6706-460b-993a-152edfd19efd" });

            //_modelBuilder.Entity<Workspace>().HasData(
            //    new Workspace() { Id = "5f6aaf86-adf9-48f6-af30-3b4198f69d65", Name = "Belfast City Airport", TenantId = "7ec39a7f-fe7e-4dd0-9f42-d187562e9875", CreatedTimestamp = new DateTime(2024, 01, 01, 13, 54, 48, 237, DateTimeKind.Utc), OwnerId = "a0dd767b-908d-42de-84f5-b55a68920a04"},
            //    new Workspace() { Id = "b7789fe3-2008-44b1-a620-aff1f17eb788", Name = "Montgomery HVAC Systems", TenantId = "7ec39a7f-fe7e-4dd0-9f42-d187562e9875", CreatedTimestamp = new DateTime(2024, 01, 01, 13, 54, 48, 237, DateTimeKind.Utc), OwnerId = "a0dd767b-908d-42de-84f5-b55a68920a04" },
            //    new Workspace() { Id = "da5f297b-609f-44d2-9264-95f89c0130c4", Name = "Translink Relay Outposts", TenantId = "7ec39a7f-fe7e-4dd0-9f42-d187562e9875", CreatedTimestamp = new DateTime(2024, 01, 01, 13, 54, 48, 237, DateTimeKind.Utc), OwnerId = "a0dd767b-908d-42de-84f5-b55a68920a04" });

            //_modelBuilder.Entity<Dashboard>().HasData(
            //    new Dashboard() { Id = "5761dd79-0df4-4fe8-83eb-ba12173441b5", Name = "Gates 01-06", WorkspaceId = "5f6aaf86-adf9-48f6-af30-3b4198f69d65", CreatedTimestamp = new DateTime(2024, 01, 01, 13, 54, 48, 237, DateTimeKind.Utc), OwnerId = "a0dd767b-908d-42de-84f5-b55a68920a04" },
            //    new Dashboard() { Id = "66a21bde-87f8-4dd1-877b-d16ba1c77cc1", Name = "Gates 07-11", WorkspaceId = "5f6aaf86-adf9-48f6-af30-3b4198f69d65", CreatedTimestamp = new DateTime(2024, 01, 01, 13, 54, 48, 237, DateTimeKind.Utc), OwnerId = "a0dd767b-908d-42de-84f5-b55a68920a04" },
            //    new Dashboard() { Id = "6d0068c3-e8e4-4c71-b8a2-058e76e5cb4b", Name = "Gates 12-14", WorkspaceId = "5f6aaf86-adf9-48f6-af30-3b4198f69d65", CreatedTimestamp = new DateTime(2024, 01, 01, 13, 54, 48, 237, DateTimeKind.Utc), OwnerId = "a0dd767b-908d-42de-84f5-b55a68920a04" },
            //    new Dashboard() { Id = "7a3e42a4-7732-4d6c-bd3d-d1456fd0be25", Name = "Gates 15-20", WorkspaceId = "5f6aaf86-adf9-48f6-af30-3b4198f69d65", CreatedTimestamp = new DateTime(2024, 01, 01, 13, 54, 48, 237, DateTimeKind.Utc), OwnerId = "a0dd767b-908d-42de-84f5-b55a68920a04" });

        }
    }
}