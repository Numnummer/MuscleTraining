using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class Add_Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "messages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    SendDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата отправки"),
                    MessageText = table.Column<string>(type: "text", nullable: false, comment: "Текст сообщения"),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id отправителя"),
                    RecieverId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id поулчателя")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messages_users_RecieverId",
                        column: x => x.RecieverId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Сообщения");

            migrationBuilder.CreateIndex(
                name: "IX_messages_RecieverId",
                schema: "public",
                table: "messages",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_SenderId",
                schema: "public",
                table: "messages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages",
                schema: "public");
        }
    }
}
