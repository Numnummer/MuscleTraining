using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Extensions;
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
    
    /// <inheritdoc />
    public async Task<RegisterUserWithVkResponse> Handle(
        RegisterUserWithVkCommand request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var findedUser = await _userService.FindUserByEmailAsync(request.Email);
        if (findedUser != null)
            return new RegisterUserWithVkResponse(IdentityResult.Success, _jwtService.GenerateJwt(findedUser.Id, Roles.User, findedUser.Email));
        
        var user = new Entities.User()
        {
            FirstName = request.Name,
            LastName = request.Surname,
            UserName = $"{request.Name}{request.Surname}".ToTransliatiateLatin(),
            Email = request.Email,
        };
        

        var result = await _userService.RegisterUserAsync(user);

        if (!result.Succeeded)
            throw new ApplicationExceptionBase(string.Join(", ", result.Errors.Select(x => x.Description)));
        
        await _userService.AddUserRoleAsync(user, Roles.User);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Role, Roles.User)
        };

        if (result.Succeeded)
            await _userService.AddClaimsAsync(user, claims);

        return new RegisterUserWithVkResponse(result, _jwtService.GenerateJwt(user.Id, Roles.User, user.Email));
    }
}