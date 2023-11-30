using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddressIdNullable_30112023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAddresses_AddressId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAddresses_AddressId",
                schema: "Identity",
                table: "Users",
                column: "AddressId",
                principalSchema: "Identity",
                principalTable: "UserAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAddresses_AddressId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAddresses_AddressId",
                schema: "Identity",
                table: "Users",
                column: "AddressId",
                principalSchema: "Identity",
                principalTable: "UserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
