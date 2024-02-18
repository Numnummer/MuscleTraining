using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAutocomplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3a96e520-caf4-464d-85e8-304863711e7b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d64e70fa-8ace-4eba-8753-2c2f383c61b3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eedd2ec5-1b1d-4ba7-9001-16db15898319"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("94af9994-da17-41a9-9905-d17debd88169"), null, "Administrator", "Administrator" },
                    { new Guid("c7c7cb90-2a71-4363-b790-eb3ae4754b71"), null, "User", "User" },
                    { new Guid("d3487e0d-648c-4f67-b087-871d6a8a3f43"), null, "Coach", "Coach" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("94af9994-da17-41a9-9905-d17debd88169"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c7c7cb90-2a71-4363-b790-eb3ae4754b71"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d3487e0d-648c-4f67-b087-871d6a8a3f43"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3a96e520-caf4-464d-85e8-304863711e7b"), null, "Administrator", null },
                    { new Guid("d64e70fa-8ace-4eba-8753-2c2f383c61b3"), null, "Coach", null },
                    { new Guid("eedd2ec5-1b1d-4ba7-9001-16db15898319"), null, "User", null }
                });
        }
    }
}
