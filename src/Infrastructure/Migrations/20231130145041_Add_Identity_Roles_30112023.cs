using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Identity_Roles_30112023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b3f2d71-be7f-4bbe-8884-9e47c64e2538", "78b83fa0-4bbb-4e93-9d90-edd7274e2f72", "Admin", "ADMIN" },
                    { "66c6309f-5573-4541-85c0-4014884edd7d", "4e185610-1df1-41b0-b658-6c647b0c5e76", "Owner", "OWNER" },
                    { "c9a1b441-d3a1-45df-90bc-a2cb4e0b497f", "413d1fa2-e86d-4d4a-bb6e-c287b98b9ca5", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5b3f2d71-be7f-4bbe-8884-9e47c64e2538");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "66c6309f-5573-4541-85c0-4014884edd7d");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c9a1b441-d3a1-45df-90bc-a2cb4e0b497f");
        }
    }
}
