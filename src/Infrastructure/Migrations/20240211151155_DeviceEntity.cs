using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeviceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkspaceDevice",
                schema: "Sflow");

            migrationBuilder.AddColumn<string>(
                name: "WorkspaceId",
                schema: "Sflow",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_WorkspaceId",
                schema: "Sflow",
                table: "Devices",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Workspaces_WorkspaceId",
                schema: "Sflow",
                table: "Devices",
                column: "WorkspaceId",
                principalSchema: "Sflow",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Workspaces_WorkspaceId",
                schema: "Sflow",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_WorkspaceId",
                schema: "Sflow",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                schema: "Sflow",
                table: "Devices");

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

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceDevice_WorkspacesId",
                schema: "Sflow",
                table: "WorkspaceDevice",
                column: "WorkspacesId");
        }
    }
}
