using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainingById;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Training.GetTrainingById;

/// <summary>
/// Обработчик запроса <see cref="GetTrainingByIdQuery"/>>
/// </summary>
public class GetTrainingByIdQueryHandler : IRequestHandler<GetTrainingByIdQuery, GetTrainingByIdResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public GetTrainingByIdQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<GetTrainingByIdResponse> Handle(
        GetTrainingByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Trainings
            .Include(x => x.Exercises)
            .FirstOrDefaultAsync(x => x.Id == request.TrainingId, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.Training>(request.TrainingId);

        return new GetTrainingByIdResponse
        {
            Name = entity.Name,
            TrainingDate = entity.TrainingDate,
            Exercises = entity.Exercises.Select(x => new GetTrainingByIdExercise
            {
                Name = x.Name,
                Description = x.Description,
                Approaches = x.Approaches,
                Repetitions = x.Repetitions,
                ImplementationProgress = x.ImplementationProgress,
                ExplanationVideo = x.ExplanationVideo,
            }).ToList(),
        };
    }
}