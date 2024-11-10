using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="Training"/>
/// </summary>
internal class TrainingConfiguration: EntityBaseConfiguration<Training>
{
    /// <inheritdoc />
    public override void ConfigureChild(EntityTypeBuilder<Training> builder)
    {
        builder.ToTable("trainings", "public")
            .HasComment("Тренировки");
        
        builder.Property(p => p.Name)
            .HasComment("Наименование тренировки");
        
        builder.Property(p => p.TrainingDate)
            .HasComment("Дата тренировки");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Trainings)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(x => x.Exercises)
            .WithMany(x => x.Trainings)
            .UsingEntity(builder =>
                builder.ToTable("training_exercise", "public")
                    .HasComment("Промежуточная таблица связи многие ко многим между тренировками и упражнениями"));
    }
}