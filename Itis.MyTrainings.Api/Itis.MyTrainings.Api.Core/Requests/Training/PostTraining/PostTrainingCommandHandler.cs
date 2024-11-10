using System.Diagnostics;
using Itis.MyTrainings.Api.Contracts.Requests.Training.PostTraining;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Training.PostTraining;

/// <summary>
/// Обработчик запроса <see cref="PostTrainingCommand"/>
/// </summary>
public class PostTrainingCommandHandler: IRequestHandler<PostTrainingCommand, PostTrainingResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст EF Core для приложения</param>
    /// <param name="userService">Сервис для работы с пользователем</param>
    public PostTrainingCommandHandler(IDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }
    
    /// <inheritdoc />
    public async Task<PostTrainingResponse> Handle(
        PostTrainingCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userService.FindUserByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        var exercises = await _dbContext.Exercises
            .Where(x => request.ExerciseIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (exercises.Count != request.ExerciseIds.Count)
            throw new ApplicationExceptionBase("Не все упражнения были найдены в системе");

        var training = new Entities.Training(user.Id, request.TrainingDate)
        {
            Name = request.Name,
            Exercises = exercises
        };

        _dbContext.Trainings.Add(training);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostTrainingResponse(training.Id);
    }
}