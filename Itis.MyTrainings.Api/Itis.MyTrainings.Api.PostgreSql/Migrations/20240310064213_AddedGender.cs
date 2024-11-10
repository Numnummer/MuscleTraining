using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddedGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "public",
                table: "user_profiles",
                type: "integer",
                nullable: true,
                comment: "Гендер");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "public",
                table: "user_profiles");
        }
    }
}
