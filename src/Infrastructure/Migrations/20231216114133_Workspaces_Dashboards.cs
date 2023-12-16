using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Workspaces_Dashboards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "114bd651-23fb-42bf-8abb-5a5d021fbf2d");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d63a4e38-4f03-4037-bcfe-9a976c3a6262");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f0868621-0572-472a-81af-d068c079680a");

            migrationBuilder.CreateTable(
                name: "Workspaces",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dashboards",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19edf386-ba44-49de-89e7-00e665fbc5fb", "1ff48fd0-9666-4fd9-81e7-d516ac027028", "User", "USER" },
                    { "9f784b8a-b2cd-482d-949b-de02c94d4a2a", "1d766614-3111-4006-91d9-2e53afe8f400", "Admin", "ADMIN" },
                    { "a54631a2-7ad2-416a-905f-ff478ba077e1", "217e5fef-4d5a-4004-9d1b-e8a80b413a2b", "Owner", "OWNER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_WorkspaceId",
                schema: "Sflow",
                table: "Dashboards",
                column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dashboards",
                schema: "Sflow");

            migrationBuilder.DropTable(
                name: "Workspaces",
                schema: "Sflow");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "19edf386-ba44-49de-89e7-00e665fbc5fb");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9f784b8a-b2cd-482d-949b-de02c94d4a2a");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a54631a2-7ad2-416a-905f-ff478ba077e1");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "114bd651-23fb-42bf-8abb-5a5d021fbf2d", "6bb8295b-af8f-4475-a387-f5fb8da9a2f7", "Admin", "ADMIN" },
                    { "d63a4e38-4f03-4037-bcfe-9a976c3a6262", "18e9383a-973a-4999-aeed-a21e60c597b9", "Owner", "OWNER" },
                    { "f0868621-0572-472a-81af-d068c079680a", "a5a8b689-6c02-429e-bb3b-04df11853406", "User", "USER" }
                });
        }
    }
}
