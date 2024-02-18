using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("111d964d-1ff6-4951-bf4c-947decef4c32"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7c071a69-38bf-438c-a2d2-01c47c04a639"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b7877145-c75f-4bd8-9026-b80acd190277"));

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: true),
                    Goals = table.Column<List<string>>(type: "text[]", nullable: true),
                    TrainingPreference = table.Column<List<string>>(type: "text[]", nullable: true),
                    PreferredWorkoutTypes = table.Column<List<string>>(type: "text[]", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WeeklyTrainingFrequency = table.Column<int>(type: "integer", nullable: true),
                    MedicalSickness = table.Column<string>(type: "text", nullable: true),
                    DietaryPreferences = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("160a6354-72a4-41e7-aa7f-c27f66aa9356"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("781c5c97-0eff-40d6-836d-3e725598986a"), null, "User", "USER" },
                    { new Guid("d1b7d1ab-a0c6-4f63-a752-9b4c5efdba86"), null, "Coach", "COACH" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProfiles_Id",
                table: "Users",
                column: "Id",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProfiles_Id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserProfiles");

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("111d964d-1ff6-4951-bf4c-947decef4c32"), null, "Coach", "COACH" },
                    { new Guid("7c071a69-38bf-438c-a2d2-01c47c04a639"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("b7877145-c75f-4bd8-9026-b80acd190277"), null, "User", "USER" }
                });
        }
    }
}
