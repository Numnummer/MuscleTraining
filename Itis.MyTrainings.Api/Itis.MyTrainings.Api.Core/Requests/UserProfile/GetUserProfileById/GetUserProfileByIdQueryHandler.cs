using System.ComponentModel.DataAnnotations;
using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;

/// <summary>
/// Обработчик запроса на получение профиля пользователя
/// </summary>
public class GetUserProfileByIdQueryHandler 
: IRequestHandler<GetUserProfileByIdQuery, GetUserProfileByIdResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст EF Core для приложения</param>
    /// <param name="userService">Сервис для работы с пользователем</param>
    public GetUserProfileByIdQueryHandler(
        IDbContext dbContext,
        IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }
    
    /// <inheritdoc />
    public async Task<GetUserProfileByIdResponse> Handle(
        GetUserProfileByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var entity = await _userService.FindUserProfileByUserId(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.UserProfile>(request.Id);
        
        var response = new GetUserProfileByIdResponse
        {
            Gender = entity.Gender,
            Height = entity.Height,
            Weight = entity.Weight,
            PhoneNumber = entity.PhoneNumber,
            DateOfBirth = entity.DateOfBirth,
            CreateDate = entity.CreateDate
        };

        return response;
    }
}