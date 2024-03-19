using Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExercises;
using Itis.MyTrainings.Api.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.GetExercises;

/// <summary>
/// Обработчик запроса для <see cref="GetExercisesQuery"/>
/// </summary>
public class GetExercisesQueryHandler: IRequestHandler<GetExercisesQuery, GetExercisesResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст EF Core для приложения</param>
    public GetExercisesQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task<GetExercisesResponse> Handle(
        GetExercisesQuery request,
        CancellationToken cancellationToken)
    {
        var exercises = _dbContext.Exercises
            .Where(x => x.UserId == request.UserId);

        var count = await exercises.CountAsync(cancellationToken);

        var result = await exercises
            .Select(x => new GetExercisesResponseItem
            {
                Name = x.Name,
                Description = x.Description,
                Approaches = x.Approaches,
                Repetitions = x.Repetitions,
                ImplementationProgress = x.ImplementationProgress,
                ExplanationVideo = x.ExplanationVideo,
            })
            .ToListAsync(cancellationToken);

        return new GetExercisesResponse(count, result);
    }
}