using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { "17ad750b-3567-4d08-bdcb-002a849e7f6b", "b0b1dd96-c532-4ff3-b7d5-d5657b2dad47", "Admin", "ADMIN" },
                    { "dc81d9e5-627b-49c1-b681-ddb320624aea", "bfe52e5c-dd0f-4367-bde7-262664cbe675", "Owner", "OWNER" },
                    { "de7c01d7-5484-4b7d-8837-42972f0e23a8", "20a79236-19c3-4743-b2c1-ce0f389d096e", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "17ad750b-3567-4d08-bdcb-002a849e7f6b");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dc81d9e5-627b-49c1-b681-ddb320624aea");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "de7c01d7-5484-4b7d-8837-42972f0e23a8");

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
        }
    }
}
