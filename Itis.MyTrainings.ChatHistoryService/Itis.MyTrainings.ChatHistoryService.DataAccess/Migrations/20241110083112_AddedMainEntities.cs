using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddedMainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageText = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SenderEmail = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    SendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GroupName = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnicastChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FromEmail = table.Column<string>(type: "text", nullable: false),
                    ToEmail = table.Column<string>(type: "text", nullable: false),
                    MessageText = table.Column<string>(type: "text", nullable: false),
                    SendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnicastChatMessages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "UnicastChatMessages");
        }
    }
}
