using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserInheritsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
