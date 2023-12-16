using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedRelationships_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Workspaces_WorkspaceId",
                schema: "Sflow",
                table: "Dashboards");

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
                    { "0cc7f82e-551e-4669-a64f-3a474e6a318b", "e599c41f-4b36-4e25-b00d-1cc0113b1068", "Admin", "ADMIN" },
                    { "43f5d9d9-3175-4482-9f28-3d8a5ca52fb5", "4fc7add1-301c-452e-8d6b-615c4c1801cf", "User", "USER" },
                    { "5c82e02c-8a6a-4edd-be96-4a04170da727", "70d419b3-3122-4c5e-92a9-dc090b7d3231", "Owner", "OWNER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Workspaces_WorkspaceId",
                schema: "Sflow",
                table: "Dashboards",
                column: "WorkspaceId",
                principalSchema: "Sflow",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Workspaces_WorkspaceId",
                schema: "Sflow",
                table: "Dashboards");

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
                    { "17ad750b-3567-4d08-bdcb-002a849e7f6b", "b0b1dd96-c532-4ff3-b7d5-d5657b2dad47", "Admin", "ADMIN" },
                    { "dc81d9e5-627b-49c1-b681-ddb320624aea", "bfe52e5c-dd0f-4367-bde7-262664cbe675", "Owner", "OWNER" },
                    { "de7c01d7-5484-4b7d-8837-42972f0e23a8", "20a79236-19c3-4743-b2c1-ce0f389d096e", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Workspaces_WorkspaceId",
                schema: "Sflow",
                table: "Dashboards",
                column: "WorkspaceId",
                principalSchema: "Sflow",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
