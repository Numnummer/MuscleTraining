using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateduserProfile3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DietaryPreferences",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "MedicalSickness",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "WeeklyTrainingFrequency",
                table: "UserProfiles");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "DietaryPreferences",
                table: "UserProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalSickness",
                table: "UserProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyTrainingFrequency",
                table: "UserProfiles",
                type: "integer",
                nullable: true);
        }
    }
}
