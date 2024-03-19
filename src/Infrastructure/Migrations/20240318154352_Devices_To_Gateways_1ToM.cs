using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Devices_To_Gateways_1ToM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GatewayId",
                schema: "Sflow",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GatewayId",
                schema: "Sflow",
                table: "Devices",
                column: "GatewayId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Devices_Gateways_GatewayId",
            //    schema: "Sflow",
            //    table: "Devices",
            //    column: "GatewayId",
            //    principalSchema: "Sflow",
            //    principalTable: "Gateways",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Gateways_GatewayId",
                schema: "Sflow",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_GatewayId",
                schema: "Sflow",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "GatewayId",
                schema: "Sflow",
                table: "Devices");
        }
    }
}
