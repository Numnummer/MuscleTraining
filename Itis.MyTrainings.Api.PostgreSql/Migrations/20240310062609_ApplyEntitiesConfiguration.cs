using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class ApplyEntitiesConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Users_UserId",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "user_profiles",
                newSchema: "public");

            migrationBuilder.AlterTable(
                name: "users",
                schema: "public",
                comment: "Профили пользователей");

            migrationBuilder.AlterTable(
                name: "roles",
                schema: "public",
                comment: "Роли");

            migrationBuilder.AlterTable(
                name: "user_profiles",
                schema: "public",
                comment: "Профили пользователей");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true,
                comment: "Никнейм пользователя",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "public",
                table: "users",
                type: "text",
                nullable: false,
                comment: "Фамилия",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "public",
                table: "users",
                type: "text",
                nullable: false,
                comment: "Имя",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true,
                comment: "Почта",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                schema: "public",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                schema: "public",
                table: "roles",
                type: "text",
                nullable: true,
                comment: "Нормализованное имя",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "public",
                table: "roles",
                type: "text",
                nullable: true,
                comment: "Наименование роли",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "roles",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                schema: "public",
                table: "user_profiles",
                type: "integer",
                nullable: true,
                comment: "Вес",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "public",
                table: "user_profiles",
                type: "text",
                nullable: true,
                comment: "Номер телефона",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                schema: "public",
                table: "user_profiles",
                type: "integer",
                nullable: true,
                comment: "Рост",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                schema: "public",
                table: "user_profiles",
                type: "timestamp without time zone",
                nullable: true,
                comment: "Дата рождения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "user_profiles",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "public",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                schema: "public",
                table: "roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_profiles",
                schema: "public",
                table: "user_profiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_UserId",
                schema: "public",
                table: "user_profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_profiles_users_UserId",
                schema: "public",
                table: "user_profiles",
                column: "UserId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_profiles_users_UserId",
                schema: "public",
                table: "user_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "public",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                schema: "public",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_profiles",
                schema: "public",
                table: "user_profiles");

            migrationBuilder.DropIndex(
                name: "IX_user_profiles_UserId",
                schema: "public",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                schema: "public",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "public",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "public",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "user_profiles",
                schema: "public",
                newName: "UserProfiles");

            migrationBuilder.AlterTable(
                name: "Users",
                oldComment: "Профили пользователей");

            migrationBuilder.AlterTable(
                name: "Roles",
                oldComment: "Роли");

            migrationBuilder.AlterTable(
                name: "UserProfiles",
                oldComment: "Профили пользователей");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Никнейм пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Фамилия");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Имя");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Почта");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Нормализованное имя");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Наименование роли");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Roles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "UserProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Вес");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "UserProfiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Номер телефона");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "UserProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Рост");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "UserProfiles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldComment: "Дата рождения");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserProfiles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Users_UserId",
                table: "UserProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
