using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PutUserProfile;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.UserProfile.PutUserProfile;

public class PutUserProfileCommandHandler: IRequestHandler<PutUserProfileCommand, PutUserProfileResponse>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="dbContext">Контекст БД</param>
    public PutUserProfileCommandHandler(
        IUserService userService,
        IDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<PutUserProfileResponse> Handle(PutUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userService.FindUserProfileByUserId(request.UserId, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.UserProfile>("Не найден профиль для данного пользователя");

        userProfile.PhoneNumber = request.PhoneNumber;
        userProfile.Gender = request.Gender;
        userProfile.DateOfBirth = request.DateOfBirth;
        userProfile.Height = request.Height;
        userProfile.Weight = request.Weight;
    
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PutUserProfileResponse(userProfile.Id);
    }
}