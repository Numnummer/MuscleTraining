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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
