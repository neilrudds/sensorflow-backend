using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class oneToMany_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0cc7f82e-551e-4669-a64f-3a474e6a318b");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "43f5d9d9-3175-4482-9f28-3d8a5ca52fb5");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5c82e02c-8a6a-4edd-be96-4a04170da727");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30a81e26-203b-4186-83ce-a317e85a5e6b", "a44a67f8-5488-40fd-a7bf-0c0676fc13d9", "User", "USER" },
                    { "497bc694-db33-462e-b357-b047a76cf5e0", "279923b8-55d2-4b84-b68a-c8b17f1c44a1", "Admin", "ADMIN" },
                    { "561f1d73-1578-47fe-83db-0acb8d1d0676", "24b65f58-1143-4e87-9739-361226841aea", "Owner", "OWNER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "30a81e26-203b-4186-83ce-a317e85a5e6b");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "497bc694-db33-462e-b357-b047a76cf5e0");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "561f1d73-1578-47fe-83db-0acb8d1d0676");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cc7f82e-551e-4669-a64f-3a474e6a318b", "e599c41f-4b36-4e25-b00d-1cc0113b1068", "Admin", "ADMIN" },
                    { "43f5d9d9-3175-4482-9f28-3d8a5ca52fb5", "4fc7add1-301c-452e-8d6b-615c4c1801cf", "User", "USER" },
                    { "5c82e02c-8a6a-4edd-be96-4a04170da727", "70d419b3-3122-4c5e-92a9-dc090b7d3231", "Owner", "OWNER" }
                });
        }
    }
}
