using Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExerciseById;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.GetExerciseById;

/// <summary>
/// Обработчик запроса <see cref="GetExerciseByIdQuery"/>>
/// </summary>
public class GetExerciseByIdQueryHandler : IRequestHandler<GetExerciseByIdQuery, GetExerciseByIdResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст EF Core для приложения</param>
    public GetExerciseByIdQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetExerciseByIdResponse> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Exercises
            .FirstOrDefaultAsync(x => x.Id == request.ExerciseId, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.Exercise>(request.ExerciseId);

        return new GetExerciseByIdResponse
        {
            Name = entity.Name,
            Description = entity.Description,
            Approaches = entity.Approaches,
            Repetitions = entity.Repetitions,
            ImplementationProgress = entity.ImplementationProgress,
            ExplanationVideo = entity.ExplanationVideo,
        };
    }
}