using Itis.MyTrainings.Api.Contracts.Requests.Exercise.PostExercise;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.PostExercise;

/// <summary>
/// Обработчик запроса для <see cref="PostExerciseCommand"/>
/// </summary>
public class PostExerciseCommandHandler: IRequestHandler<PostExerciseCommand, PostExerciseResponse>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="dbContext">Контекст EF Core для приложения</param>
    public PostExerciseCommandHandler(IUserService userService, IDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<PostExerciseResponse> Handle(
        PostExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userService.FindUserByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        var exercise = new Entities.Exercise(user.Id, request.Name)
        {
            Description = request.Description,
            Approaches = request.Approaches,
            Repetitions = request.Repetitions,
            ImplementationProgress = request.ImplementationProgress,
            ExplanationVideo = request.ExplanationVideo
        };

        _dbContext.Exercises.Add(exercise);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostExerciseResponse(exercise.Id);
    }
}