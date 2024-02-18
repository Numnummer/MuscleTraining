using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAutocomplete2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("35bcc1ba-82e7-48e7-8256-99557deca611"), null, "User", "USER" },
                    { new Guid("80b7d93b-b186-48a6-a3bb-f00a5a4b9a29"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("9039d8c1-dfa9-461b-a4a2-f6e5cc47a5e0"), null, "Coach", "COACH" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("35bcc1ba-82e7-48e7-8256-99557deca611"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("80b7d93b-b186-48a6-a3bb-f00a5a4b9a29"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9039d8c1-dfa9-461b-a4a2-f6e5cc47a5e0"));

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
    }
}
