using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterWithGoogle;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterWithGoogle;

public class RegisterWithGoogleCommandHandler : IRequestHandler<RegisterWithGoogleCommand, RegisterWithGoogleResponse>
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public RegisterWithGoogleCommandHandler(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<RegisterWithGoogleResponse> Handle(RegisterWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(request.Token);

        var email = jsonToken.Claims.FirstOrDefault(x => x.Type == "email");
        var emailVerified = jsonToken.Claims.FirstOrDefault(x => x.Type == "email_verified");

        if (email == null || emailVerified == null)
            throw new ValidationException("Токен не содержит необходимых данных");

        var existedUser = await _userService.FindUserByEmailAsync(email.Value);
        if (existedUser != null)
        {
            if (existedUser.RegisteredWithGoogle)
                return await SignIn(existedUser);
            
            throw new ValidationException("Пользователь с такой почтой уже существует");
        }

        var user = new Entities.User
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            UserName = email.Value.Split('@').First(),
            Email = email.Value,
            RegisteredWithGoogle = true
        };
        
        var result = await _userService.RegisterUserWithoutPassword(user);

        if (result.Succeeded)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, "User")
            };

            await _userService.AddUserRoleAsync(user, "User");
            await _userService.AddClaimsAsync(user, claims);
        }
        else
            throw new ApplicationExceptionBase("Ошибка при регистрации");

        return await SignIn(user);
    }

    private async Task<RegisterWithGoogleResponse> SignIn(Entities.User user)
    {
        await _userService.SignInWithoutPasswordAsync(user);
        
        var role = await _userService.GetRoleAsync(user);
        var token = _jwtService.GenerateJwt(user.Id, role!, user.Email);

        return new RegisterWithGoogleResponse(token);
    }
}