using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class gateway_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateways",
                schema: "Sflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortNumber = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSLEnabled = table.Column<bool>(type: "bit", nullable: false),
                    WorkspaceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gateways_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "Sflow",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gateways_WorkspaceId",
                schema: "Sflow",
                table: "Gateways",
                column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gateways",
                schema: "Sflow");
        }
    }
}
