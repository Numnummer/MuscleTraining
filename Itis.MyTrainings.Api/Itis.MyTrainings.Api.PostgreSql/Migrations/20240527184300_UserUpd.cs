using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class UserUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RegisteredWithGoogle",
                schema: "public",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Зарегистрирован через Google");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredWithGoogle",
                schema: "public",
                table: "users");
        }
    }
}
