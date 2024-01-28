using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DashboardGridWidgets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GridWidgets",
                schema: "Sflow",
                table: "Dashboards",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GridWidgets",
                schema: "Sflow",
                table: "Dashboards");
        }
    }
}
