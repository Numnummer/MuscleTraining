using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class MessageFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_RecieverId",
                schema: "public",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_RecieverId",
                schema: "public",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                schema: "public",
                table: "messages");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "messages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserId",
                schema: "public",
                table: "messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_UserId",
                schema: "public",
                table: "messages",
                column: "UserId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_UserId",
                schema: "public",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_UserId",
                schema: "public",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "public",
                table: "messages");

            migrationBuilder.AddColumn<Guid>(
                name: "RecieverId",
                schema: "public",
                table: "messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id поулчателя");

            migrationBuilder.CreateIndex(
                name: "IX_messages_RecieverId",
                schema: "public",
                table: "messages",
                column: "RecieverId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_RecieverId",
                schema: "public",
                table: "messages",
                column: "RecieverId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
