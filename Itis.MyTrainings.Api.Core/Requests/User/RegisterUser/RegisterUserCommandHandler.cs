using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterUser;

/// <summary>
/// Обработчик запроса <see cref="RegisterUserCommand"/>
/// </summary>
public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="roleService">Сервис для работы с ролями</param>
    public RegisterUserCommandHandler(
        IUserService userService,
        IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    /// <inheritdoc />
    public async Task<RegisterUserResponse> Handle(
        RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var isUserExist = await _userService.FindUserByEmailAsync(request.Email);
        if (isUserExist != null)
            throw new ValidationException("Пользователь с данной почтой уже существует");
        
        var isRoleExist = await _roleService.IsRoleExistAsync(request.Role);
        if (!isRoleExist)
            throw new EntityNotFoundException<Role>($"Роли \"{request.Role}\" не существует");
        
        var user = new Entities.User
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        
        var result = await _userService.RegisterUserAsync(user, request.Password);

        if (result.Succeeded)
            await _userService.AddUserRoleAsync(user, request.Role);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Role, request.Role)
        };

        if (result.Succeeded)
            await _userService.AddClaimsAsync(user, claims);

        return new RegisterUserResponse(result);
    }
}