using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithVk;

/// <summary>
/// Обработчик запроса для <see cref="RegisterUserWithVkCommand"/> 
/// </summary>
public class RegisterUserWithVkCommandHandler : IRequestHandler<RegisterUserWithVkCommand, RegisterUserWithVkResponse>
{
    private readonly IUserService _userService;
    private readonly IVkService _vkService;
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="vkService">Сервис работы с ВКонтакте</param>
    /// <param name="jwtService">Сервис для работы с Jwt</param>
    public RegisterUserWithVkCommandHandler(
        IUserService userService,
        IVkService vkService,
        IJwtService jwtService)
    {
        _userService = userService;
        _vkService = vkService;
        _jwtService = jwtService;
    }
    
    /// <inheritdoc /> TODO поффиксить роли
    public async Task<RegisterUserWithVkResponse> Handle(
        RegisterUserWithVkCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await GetUserFromVkAsync(request.Code!, cancellationToken);

        var findedUser = await _userService.FindUserByEmailAsync(user.Email!);
        if (findedUser != null)
            return new RegisterUserWithVkResponse(IdentityResult.Success, _jwtService.GenerateJwt(findedUser.Id, Roles.User, findedUser.Email));
        //TODO try catch
        var result = await _userService.RegisterUserAsync(user);

        if (result.Succeeded)
            await _userService.AddUserRoleAsync(user, Roles.User);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Role, Roles.User)
        };

        if (result.Succeeded)
            await _userService.AddClaimsAsync(user, claims);

        return new RegisterUserWithVkResponse(result, _jwtService.GenerateJwt(user.Id, Roles.User, user.Email));
    }

    private async Task<Entities.User> GetUserFromVkAsync(string code, CancellationToken cancellationToken)
    {
        await _vkService.GetAccessTokenAsync(code, cancellationToken);
        var info = (await _vkService.GetVkUserInfoAsync(cancellationToken)).Response;

        var user = new Entities.User()
        {
            FirstName = info.FirstName,
            LastName = info.LastName,
            PhoneNumber = info.Phone,
            UserName = info.Id.ToString(),
            Email = info.Mail,
        };
        return user;
    }
}