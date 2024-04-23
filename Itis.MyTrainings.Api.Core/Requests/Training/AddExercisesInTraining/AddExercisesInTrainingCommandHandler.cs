using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Training.AddExercisesInTraining;

/// <summary>
/// Обработчик запроса <see cref="AddExercisesInTrainingCommand"/>
/// </summary>
public class AddExercisesInTrainingCommandHandler : IRequestHandler<AddExercisesInTrainingCommand>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext"></param>
    public AddExercisesInTrainingCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(AddExercisesInTrainingCommand request, CancellationToken cancellationToken)
    {
        var exercises = await _dbContext.Exercises
            .Where(x => request.ExerciseIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (exercises.Count != request.ExerciseIds.Count)
            throw new EntityNotFoundException<Entities.Exercise>(request.ExerciseIds);
        
        var training = await _dbContext.Trainings
            .FirstOrDefaultAsync(x => x.Id == request.TrainingId, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.Training>(request.TrainingId);
        
        training.Exercises.AddRange(exercises);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return default;
    }
}