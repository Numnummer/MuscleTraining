using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainings;
using Itis.MyTrainings.Api.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Training.GetTrainings;

/// <summary>
/// Обработчик запроса <see cref="GetTrainingsQuery"/>
/// </summary>
public class GetTrainingQueryHandler: IRequestHandler<GetTrainingsQuery, GetTrainingsResponse>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="dbContext">Контекст EF Core для приложения</param>
    public GetTrainingQueryHandler(IUserService userService, IDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<GetTrainingsResponse> Handle(
        GetTrainingsQuery request,
        CancellationToken cancellationToken)
    {
        var trainings = _dbContext.Trainings
            .Where(x => x.UserId == request.UserId);

        var count = await trainings.CountAsync(cancellationToken);

        var result = await trainings
            .Select(x => new GetTrainingsResponseItem
            {
                Id = x.Id,
                Name = x.Name,
                TrainingDate = x.TrainingDate,
                Exercises = x.Exercises.Select(y => new GetTrainingsResponseExercise
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Description = y.Description,
                        Approaches = y.Approaches,
                        Repetitions = y.Repetitions,
                        ImplementationProgress = y.ImplementationProgress,
                        ExplanationVideo = y.ExplanationVideo,
                    })
                    .ToList()
            }).ToListAsync(cancellationToken);

        return new GetTrainingsResponse(count, result);
    }
}