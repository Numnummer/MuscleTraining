using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="Exercise"/>>
/// </summary>
internal class ExerciseConfiguration: EntityBaseConfiguration<Exercise>
{
    /// <inheritdoc />
    public override void ConfigureChild(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("exercises", "public")
            .HasComment("Упражнения");
        
        builder.Property(p => p.Name)
            .HasComment("Наименование упражнения");
        
        builder.Property(p => p.Description)
            .HasComment("Описание");
        
        builder.Property(p => p.Approaches)
            .HasComment("Кол-во подходов");
        
        builder.Property(p => p.Repetitions)
            .HasComment("Кол-во повторений в подходе");
        
        builder.Property(p => p.ImplementationProgress)
            .HasComment("Ход выполнения");
        
        builder.Property(p => p.ExplanationVideo)
            .HasComment("Ссылка на видео с объяснением");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Exercises)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(x => x.Trainings)
            .WithMany(x => x.Exercises)
            .UsingEntity(builder =>
                builder.ToTable("training_exercise", "public")
                    .HasComment("Промежуточная таблица связи многие ко многим между тренировками и упражнениями"));
    }
}