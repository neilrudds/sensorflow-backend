using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DaviceLocationField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons",
                schema: "Sflow");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Sflow",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Sflow",
                table: "Devices");

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });
        }
    }
}
