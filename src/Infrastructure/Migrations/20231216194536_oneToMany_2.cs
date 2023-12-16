using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class oneToMany_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "037e8e18-1cd3-4f4b-8bf9-c2afd1c0398e", "5d721a95-5a76-40e0-9c96-f29d88ecaca1", "Admin", "ADMIN" },
                    { "3b78f36d-012b-4cfd-92be-d54c842ec42c", "f4982b39-df97-4631-9fd6-14c04f104661", "User", "USER" },
                    { "71e98fb0-5ca1-4939-ac28-432e218be1bc", "2fbf86e0-1757-41bc-afbb-96f5742906c4", "Owner", "OWNER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "037e8e18-1cd3-4f4b-8bf9-c2afd1c0398e");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3b78f36d-012b-4cfd-92be-d54c842ec42c");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "71e98fb0-5ca1-4939-ac28-432e218be1bc");

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
    }
}
