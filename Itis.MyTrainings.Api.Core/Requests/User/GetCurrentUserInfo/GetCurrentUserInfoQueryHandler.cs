using Itis.MyTrainings.Api.Contracts.Requests.User.GetCurrentUserInfo;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.User.GetResetPasswordCode;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.GetCurrentUserInfo;

/// <summary>
/// Обработчик запроса <see cref="SendResetPasswordQuery"/>
/// </summary>
public class GetCurrentUserInfoQueryHandler:
    IRequestHandler<GetCurrentUserInfoQuery, GetCurrentUserInfoResponse>
{
    private IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    public GetCurrentUserInfoQueryHandler(
        IUserService userService)
    {
        _userService = userService;
    }
    
    /// <inheritdoc />
    public async Task<GetCurrentUserInfoResponse> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new ArgumentNullException(nameof(request.UserId));
            
        var user = await _userService.FindUserByIdAsync(request.UserId.Value)
            ?? throw new EntityNotFoundException<Entities.User>("Пользователи не найдены");

        var userProfile = user.ProfileId != null
            ? await _userService.FindUserProfileById(user.ProfileId, cancellationToken)
            : null;

        return new GetCurrentUserInfoResponse
        {
            UserId = user.Id,
            UserProfileId = userProfile?.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Gender = userProfile?.Gender,
            DateOfBirth = userProfile?.DateOfBirth,
            PhoneNumber = userProfile?.PhoneNumber,
            Height = userProfile?.Height,
            Weight = userProfile?.Weight,
            StartDate = userProfile?.StartDate,
            WeeklyTrainingFrequency = userProfile?.WeeklyTrainingFrequency,
            MedicalSickness = userProfile?.MedicalSickness,
            DietaryPreferences = userProfile?.DietaryPreferences,
        };
    }
}