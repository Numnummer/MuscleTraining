using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.DeleteExercise;

/// <summary>
/// Обработчик запроса <see cref="DeleteExerciseCommand"/>
/// </summary>
public class DeleteExerciseCommandHandler: IRequestHandler<DeleteExerciseCommand>
{
    private readonly IDbContext _dbContext;

    public DeleteExerciseCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Exercises
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.Exercise>(request.Id);

        _dbContext.Exercises.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return default;
    }
}