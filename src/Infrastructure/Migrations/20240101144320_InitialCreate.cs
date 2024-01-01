using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sflow");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "Devices",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "varchar(64)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(64)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workspaces",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workspaces_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "Sflow",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "Sflow",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserAddresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Identity",
                        principalTable: "UserAddresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Dashboards",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    WorkspaceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dashboards_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "Sflow",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceDevice",
                schema: "Sflow",
                columns: table => new
                {
                    DevicesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspacesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceDevice", x => new { x.DevicesId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_WorkspaceDevice_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalSchema: "Sflow",
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceDevice_Workspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalSchema: "Sflow",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceUser",
                schema: "Sflow",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspacesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceUser", x => new { x.UsersId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_WorkspaceUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkspaceUser_Workspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalSchema: "Sflow",
                        principalTable: "Workspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08aed62a-7b78-47cf-9472-fa755c04f241", "b1f5336e-cad3-4c51-b025-d810d6942db5", "Admin", "ADMIN" },
                    { "29d7ebeb-6706-460b-993a-152edfd19efd", "6e95fe98-be6d-4405-ac14-67b4746e724f", "Owner", "OWNER" },
                    { "51ba0c4e-0772-4b90-808e-c7dad3fe4342", "f6759e18-32d3-4dfc-a58e-c2c47819b366", "User", "USER" }
                });

            migrationBuilder.InsertData(
                schema: "Sflow",
                table: "Tenants",
                columns: new[] { "Id", "CreatedTimestamp", "LastModifiedTimestamp", "ModifiedById", "Name", "OwnerId" },
                values: new object[] { "7ec39a7f-fe7e-4dd0-9f42-d187562e9875", new DateTime(2023, 12, 31, 13, 54, 48, 237, DateTimeKind.Utc).AddTicks(2369), null, null, "Neil Rutherford", null });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a0dd767b-908d-42de-84f5-b55a68920a04", 0, null, "7e821b98-e164-4396-9713-8c30457265d9", "neilr@hotmail.com", false, "Neil", true, "Rutherford", true, null, "NEILR@HOTMAIL.COM", "NEILR@HOTMAIL.COM", "AQAAAAIAAYagAAAAELaguSZ3I+utb3vgRy/lnU+XTfVT9R/F4+roSf3859lrthJ+hphjmcrikWdYNpMA1Q==", null, false, "2JKC7MM4IT23GQA2C26EYIXLHGIIFXUH", "7ec39a7f-fe7e-4dd0-9f42-d187562e9875", false, "neilr@hotmail.com" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "29d7ebeb-6706-460b-993a-152edfd19efd", "a0dd767b-908d-42de-84f5-b55a68920a04" });

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_WorkspaceId",
                schema: "Sflow",
                table: "Dashboards",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                schema: "Identity",
                table: "Users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                schema: "Identity",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceDevice_WorkspacesId",
                schema: "Sflow",
                table: "WorkspaceDevice",
                column: "WorkspacesId");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_TenantId",
                schema: "Sflow",
                table: "Workspaces",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUser_WorkspacesId",
                schema: "Sflow",
                table: "WorkspaceUser",
                column: "WorkspacesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dashboards",
                schema: "Sflow");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "Sflow");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "WorkspaceDevice",
                schema: "Sflow");

            migrationBuilder.DropTable(
                name: "WorkspaceUser",
                schema: "Sflow");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Devices",
                schema: "Sflow");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Workspaces",
                schema: "Sflow");

            migrationBuilder.DropTable(
                name: "UserAddresses",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "Sflow");
        }
    }
}
