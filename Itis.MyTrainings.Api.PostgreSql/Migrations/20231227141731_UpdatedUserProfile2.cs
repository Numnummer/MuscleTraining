using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserProfile2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Goals",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PreferredWorkoutTypes",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "TrainingPreference",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("aaed8239-88b2-41df-a174-3aabc82c6c58"), null, "User", "USER" },
                    { new Guid("b2790f59-7141-4979-bc42-5474008bb17d"), null, "Coach", "COACH" },
                    { new Guid("dfee8cc1-a4fa-4a5e-9ab6-723fcf49c9c2"), null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aaed8239-88b2-41df-a174-3aabc82c6c58"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b2790f59-7141-4979-bc42-5474008bb17d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dfee8cc1-a4fa-4a5e-9ab6-723fcf49c9c2"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Goals",
                table: "UserProfiles",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "PreferredWorkoutTypes",
                table: "UserProfiles",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "TrainingPreference",
                table: "UserProfiles",
                type: "text[]",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("544df796-5527-483d-aff4-861350001156"), null, "User", "USER" },
                    { new Guid("e4c5bdf2-f1c9-4e5e-9b09-efb082ff44be"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("eac3dc6a-1c24-4561-ae22-7254f57c98eb"), null, "Coach", "COACH" }
                });
        }
    }
}
