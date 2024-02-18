using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("160a6354-72a4-41e7-aa7f-c27f66aa9356"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("781c5c97-0eff-40d6-836d-3e725598986a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d1b7d1ab-a0c6-4f63-a752-9b4c5efdba86"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Users",
                type: "uuid",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "ProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("160a6354-72a4-41e7-aa7f-c27f66aa9356"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("781c5c97-0eff-40d6-836d-3e725598986a"), null, "User", "USER" },
                    { new Guid("d1b7d1ab-a0c6-4f63-a752-9b4c5efdba86"), null, "Coach", "COACH" }
                });
        }
    }
}
