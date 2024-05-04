﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SensorFlow.Infrastructure.DbContexts;

#nullable disable

namespace SensorFlow.Infrastructure.Migrations
{
    [DbContext(typeof(SensorFlowDbContext))]
    [Migration("20240504113906_DaviceLocationField")]
    partial class DaviceLocationField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "Identity");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Dashboards.Dashboard", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("GridLayout")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GridWidgets")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Dashboards", "Sflow");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Devices.Device", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fields")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GatewayId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastModifiedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GatewayId");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Devices", "Sflow");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Gateways.Gateway", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PortNumber")
                        .HasColumnType("int");

                    b.Property<bool>("SSLEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Gateways", "Sflow");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Tenants.Tenant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tenants", "Sflow");

                    b.HasData(
                        new
                        {
                            Id = "7ec39a7f-fe7e-4dd0-9f42-d187562e9875",
                            CreatedTimestamp = new DateTime(2023, 12, 31, 13, 54, 48, 237, DateTimeKind.Utc).AddTicks(2369),
                            Name = "Neil Rutherford"
                        });
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Users.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", "Identity");

                    b.HasData(
                        new
                        {
                            Id = "51ba0c4e-0772-4b90-808e-c7dad3fe4342",
                            ConcurrencyStamp = "f6759e18-32d3-4dfc-a58e-c2c47819b366",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "08aed62a-7b78-47cf-9472-fa755c04f241",
                            ConcurrencyStamp = "b1f5336e-cad3-4c51-b025-d810d6942db5",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "29d7ebeb-6706-460b-993a-152edfd19efd",
                            ConcurrencyStamp = "6e95fe98-be6d-4405-ac14-67b4746e724f",
                            Name = "Owner",
                            NormalizedName = "OWNER"
                        });
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("TenantId");

                    b.ToTable("Users", "Identity");

                    b.HasData(
                        new
                        {
                            Id = "a0dd767b-908d-42de-84f5-b55a68920a04",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7e821b98-e164-4396-9713-8c30457265d9",
                            Email = "neilr@hotmail.com",
                            EmailConfirmed = false,
                            FirstName = "Neil",
                            IsActive = true,
                            LastName = "Rutherford",
                            LockoutEnabled = true,
                            NormalizedEmail = "NEILR@HOTMAIL.COM",
                            NormalizedUserName = "NEILR@HOTMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAELaguSZ3I+utb3vgRy/lnU+XTfVT9R/F4+roSf3859lrthJ+hphjmcrikWdYNpMA1Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2JKC7MM4IT23GQA2C26EYIXLHGIIFXUH",
                            TenantId = "7ec39a7f-fe7e-4dd0-9f42-d187562e9875",
                            TwoFactorEnabled = false,
                            UserName = "neilr@hotmail.com"
                        });
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Users.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "Identity");

                    b.HasData(
                        new
                        {
                            UserId = "a0dd767b-908d-42de-84f5-b55a68920a04",
                            RoleId = "29d7ebeb-6706-460b-993a-152edfd19efd"
                        });
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Workspaces.Workspace", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Workspaces", "Sflow");
                });

            modelBuilder.Entity("SensorFlow.Domain.ValueObjects.Address", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAddresses", "Identity");
                });

            modelBuilder.Entity("UserWorkspace", b =>
                {
                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WorkspacesId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UsersId", "WorkspacesId");

                    b.HasIndex("WorkspacesId");

                    b.ToTable("WorkspaceUser", "Sflow");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Dashboards.Dashboard", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Workspaces.Workspace", "Workspace")
                        .WithMany("Dashboards")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Devices.Device", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Gateways.Gateway", "Gateway")
                        .WithMany("Devices")
                        .HasForeignKey("GatewayId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SensorFlow.Domain.Entities.Workspaces.Workspace", "Workspace")
                        .WithMany("Devices")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gateway");

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Gateways.Gateway", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Workspaces.Workspace", "Workspace")
                        .WithMany("Gateways")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Users.User", b =>
                {
                    b.HasOne("SensorFlow.Domain.ValueObjects.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("SensorFlow.Domain.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId");

                    b.Navigation("Address");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Users.UserRole", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Users.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SensorFlow.Domain.Entities.Users.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Workspaces.Workspace", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("Workspaces")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("UserWorkspace", b =>
                {
                    b.HasOne("SensorFlow.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SensorFlow.Domain.Entities.Workspaces.Workspace", null)
                        .WithMany()
                        .HasForeignKey("WorkspacesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Gateways.Gateway", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Tenants.Tenant", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("Workspaces");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Users.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Users.User", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("SensorFlow.Domain.Entities.Workspaces.Workspace", b =>
                {
                    b.Navigation("Dashboards");

                    b.Navigation("Devices");

                    b.Navigation("Gateways");
                });
#pragma warning restore 612, 618
        }
    }
}
