using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProfiles_Id",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11f83919-addd-442b-826c-5d043c017970"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a9f13ee5-ccc4-477b-93f0-e9a6c314cf54"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f68b0271-cbce-459b-9b01-aa58a1f1e904"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("544df796-5527-483d-aff4-861350001156"), null, "User", "USER" },
                    { new Guid("e4c5bdf2-f1c9-4e5e-9b09-efb082ff44be"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("eac3dc6a-1c24-4561-ae22-7254f57c98eb"), null, "Coach", "COACH" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                table: "Users",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProfiles_ProfileId",
                table: "Users",
                column: "ProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProfiles_ProfileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("544df796-5527-483d-aff4-861350001156"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e4c5bdf2-f1c9-4e5e-9b09-efb082ff44be"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eac3dc6a-1c24-4561-ae22-7254f57c98eb"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserProfiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11f83919-addd-442b-826c-5d043c017970"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("a9f13ee5-ccc4-477b-93f0-e9a6c314cf54"), null, "User", "USER" },
                    { new Guid("f68b0271-cbce-459b-9b01-aa58a1f1e904"), null, "Coach", "COACH" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProfiles_Id",
                table: "Users",
                column: "Id",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
