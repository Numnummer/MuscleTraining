using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddedTrainingsWithExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exercises",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "Наименование упражнения"),
                    Description = table.Column<string>(type: "text", nullable: true, comment: "Описание"),
                    Approaches = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во подходов"),
                    Repetitions = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во повторений в подходе"),
                    ImplementationProgress = table.Column<string>(type: "text", nullable: true, comment: "Ход выполнения"),
                    ExplanationVideo = table.Column<string>(type: "text", nullable: true, comment: "Ссылка на видео с объяснением"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exercises_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id");
                },
                comment: "Упражнения");

            migrationBuilder.CreateTable(
                name: "trainings",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    Name = table.Column<string>(type: "text", nullable: true, comment: "Наименование тренировки"),
                    TrainingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата тренировки"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trainings_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id");
                },
                comment: "Тренировки");

            migrationBuilder.CreateTable(
                name: "training_exercise",
                schema: "public",
                columns: table => new
                {
                    ExercisesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainingsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_training_exercise", x => new { x.ExercisesId, x.TrainingsId });
                    table.ForeignKey(
                        name: "FK_training_exercise_exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalSchema: "public",
                        principalTable: "exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_training_exercise_trainings_TrainingsId",
                        column: x => x.TrainingsId,
                        principalSchema: "public",
                        principalTable: "trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Промежуточная таблица связи многие ко многим между тренировками и упражнениями");

            migrationBuilder.CreateIndex(
                name: "IX_exercises_UserId",
                schema: "public",
                table: "exercises",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_training_exercise_TrainingsId",
                schema: "public",
                table: "training_exercise",
                column: "TrainingsId");

            migrationBuilder.CreateIndex(
                name: "IX_trainings_UserId",
                schema: "public",
                table: "trainings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "training_exercise",
                schema: "public");

            migrationBuilder.DropTable(
                name: "exercises",
                schema: "public");

            migrationBuilder.DropTable(
                name: "trainings",
                schema: "public");
        }
    }
}
