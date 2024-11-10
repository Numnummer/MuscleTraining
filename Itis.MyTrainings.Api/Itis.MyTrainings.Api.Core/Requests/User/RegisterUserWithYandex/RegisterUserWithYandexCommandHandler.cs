using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithYandex;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithYandex;

/// <summary>
/// Обработчик запроса для <see cref="RegisterUserWithYandexCommand"/> 
/// </summary>
public class RegisterUserWithYandexCommandHandler 
: IRequestHandler<RegisterUserWithYandexCommand, RegisterUserWithYandexResponse>
{
    private readonly IUserService _userService;
    private readonly IYandexService _yandexService;
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="yandexService">Сервис для работы с Яндексом</param>
    /// <param name="jwtService">Сервис для работы с JWT</param>
    public RegisterUserWithYandexCommandHandler(
        IUserService userService,
        IYandexService yandexService,
        IJwtService jwtService)
    {
        _userService = userService;
        _yandexService = yandexService;
        _jwtService = jwtService;
    }

    /// <inheritdoc />
    public async Task<RegisterUserWithYandexResponse> Handle(
        RegisterUserWithYandexCommand request,
        CancellationToken cancellationToken)
    {
        var user = await GetUserFromYandexAsync(request.Code!, cancellationToken);

        if (user == null)
            return new RegisterUserWithYandexResponse(IdentityResult.Failed(), "");

        var findedUser = await _userService.FindUserByEmailAsync(user.Email!);
        if (findedUser != null)
            return new RegisterUserWithYandexResponse(
                IdentityResult.Success,
                _jwtService.GenerateJwt(findedUser.Id, Roles.User, findedUser.Email));
         
        try
        {
            var result = await _userService.RegisterUserAsync(user);

            //Роли падают чинить лень
            if (result.Succeeded) 
                await _userService.AddUserRoleAsync(user, Roles.User);

            var claims = new List<Claim>
            {
                new (ClaimTypes.Role, Roles.User)
            };

            if (result.Succeeded)
                await _userService.AddClaimsAsync(user, claims);

            return new RegisterUserWithYandexResponse(result, _jwtService.GenerateJwt(user.Id, Roles.User, user.Email));
        }
        catch (Exception)
        {
            return new RegisterUserWithYandexResponse(IdentityResult.Failed(), "");
        }
        
    }
    
    private async Task<Entities.User> GetUserFromYandexAsync(string code, CancellationToken cancellationToken)
    {
        var accessToken = await _yandexService.GetAccessTokenAsync(code, cancellationToken);
        
        if (accessToken == null) 
            return null!;
        
        var info = await _yandexService.GetUserInfoAsync(accessToken.AccessToken, cancellationToken);
        
        if (info == null) 
            return null!;
        
        var user = new Entities.User()
        {
            FirstName = info.FirstName,
            LastName = info.LastName,
            PhoneNumber = info.Phone.Number,
            UserName = info.Login,
            Email = info.DefaultEmail,
        };
        return user;
    }
}