using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.Exercise.DeleteExercise;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Training.DeleteTraining;

/// <summary>
/// Обработчик запроса <see cref="DeleteExerciseCommand"/>
/// </summary>
public class DeleteTrainingCommandHandler: IRequestHandler<DeleteTrainingCommand>
{
    private readonly IDbContext _dbContext;

    public DeleteTrainingCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Trainings
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.Exercise>(request.Id);

        _dbContext.Trainings.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return default;
    }
}