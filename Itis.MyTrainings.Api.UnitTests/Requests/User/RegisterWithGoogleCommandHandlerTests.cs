using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterWithGoogle;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;

public class RegisterWithGoogleCommandHandlerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IJwtService> _mockJwtService;
    private readonly RegisterWithGoogleCommandHandler _handler;

    public RegisterWithGoogleCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockJwtService = new Mock<IJwtService>();
        _handler = new RegisterWithGoogleCommandHandler(_mockUserService.Object, _mockJwtService.Object);
    }

    //[Fact]
    public async Task Handle_ShouldThrowValidationException_WhenTokenLacksEmailOrEmailVerified()
    {
        // Arrange
        var command = new RegisterWithGoogleCommand("InvalidToken");

        var mockTokenHandler = new Mock<JwtSecurityTokenHandler>();
        var jwtToken = new JwtSecurityToken();
        mockTokenHandler.Setup(x => x.ReadJwtToken(command.Token)).Returns(jwtToken);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    //[Fact]
    public async Task Handle_ShouldReturnSignInResponse_WhenUserAlreadyRegisteredWithGoogle()
    {
        // Arrange
        var command = new RegisterWithGoogleCommand("ValidGoogleToken");

        var email = "existinguser@gmail.com";
        var jwtToken = new JwtSecurityToken(claims: new[]
        {
            new Claim("email", email),
            new Claim("email_verified", "true")
        });

        var user = new Core.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = email,
            RegisteredWithGoogle = true
        };

        _mockUserService.Setup(x => x.FindUserByEmailAsync(email))
            .ReturnsAsync(user);

        _mockUserService.Setup(x => x.SignInWithoutPasswordAsync(user))
            .Returns(Task.CompletedTask);

        var role = "User";
        var token = "GeneratedJwtToken";
        _mockUserService.Setup(x => x.GetRoleAsync(user))
            .ReturnsAsync(role);
        _mockJwtService.Setup(x => x.GenerateJwt(user.Id, role, user.Email))
            .Returns(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(token, result.JwtToken);
    }

    //[Fact]
    public async Task Handle_ShouldThrowValidationException_WhenUserAlreadyExistsWithoutGoogleRegistration()
    {
        // Arrange
        var command = new RegisterWithGoogleCommand("ValidGoogleToken");

        var email = "existinguser@gmail.com";
        var jwtToken = new JwtSecurityToken(claims: new[]
        {
            new Claim("email", email),
            new Claim("email_verified", "true")
        });

        var user = new Core.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = email,
            RegisteredWithGoogle = false
        };

        _mockUserService.Setup(x => x.FindUserByEmailAsync(email))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    //[Fact]
    public async Task Handle_ShouldRegisterUserAndReturnSignInResponse_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new RegisterWithGoogleCommand("ValidGoogleToken");

        var email = "newuser@gmail.com";
        var jwtToken = new JwtSecurityToken(claims: new[]
        {
            new Claim("email", email),
            new Claim("email_verified", "true")
        });

        var user = new Core.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = email,
            RegisteredWithGoogle = true,
            UserName = "newuser"
        };

        var registrationResult = IdentityResult.Success;

        _mockUserService.Setup(x => x.FindUserByEmailAsync(email))
            .ReturnsAsync((Core.Entities.User)null); // User does not exist

        _mockUserService.Setup(x => x.RegisterUserWithoutPassword(user))
            .ReturnsAsync(registrationResult);

        _mockUserService.Setup(x => x.AddUserRoleAsync(user, "User"))
            .ReturnsAsync(IdentityResult.Success);

        _mockUserService.Setup(x => x.AddClaimsAsync(user, It.IsAny<List<Claim>>()))
            .ReturnsAsync(IdentityResult.Success);

        _mockUserService.Setup(x => x.SignInWithoutPasswordAsync(user))
            .Returns(Task.CompletedTask);

        var role = "User";
        var token = "GeneratedJwtToken";
        _mockUserService.Setup(x => x.GetRoleAsync(user))
            .ReturnsAsync(role);

        _mockJwtService.Setup(x => x.GenerateJwt(user.Id, role, user.Email))
            .Returns(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(token, result.JwtToken);
        _mockUserService.Verify(x => x.RegisterUserWithoutPassword(It.IsAny<Core.Entities.User>()), Times.Once);
        _mockUserService.Verify(x => x.AddUserRoleAsync(It.IsAny<Core.Entities.User>(), "User"), Times.Once);
        _mockUserService.Verify(x => x.AddClaimsAsync(It.IsAny<Core.Entities.User>(), It.IsAny<List<Claim>>()), Times.Once);
    }

    //[Fact]
    public async Task Handle_ShouldThrowApplicationException_WhenRegistrationFails()
    {
        // Arrange
        var command = new RegisterWithGoogleCommand("ValidGoogleToken");

        var email = "newuser@gmail.com";
        var jwtToken = new JwtSecurityToken(claims: new[]
        {
            new Claim("email", email),
            new Claim("email_verified", "true")
        });

        var user = new Core.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = email,
            RegisteredWithGoogle = true,
            UserName = "newuser"
        };

        var registrationResult = IdentityResult.Failed();

        _mockUserService.Setup(x => x.FindUserByEmailAsync(email))
            .ReturnsAsync((Core.Entities.User)null); // User does not exist

        _mockUserService.Setup(x => x.RegisterUserWithoutPassword(user))
            .ReturnsAsync(registrationResult);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationExceptionBase>(() =>
            _handler.Handle(command, CancellationToken.None));

        _mockUserService.Verify(x => x.RegisterUserWithoutPassword(It.IsAny<Core.Entities.User>()), Times.Once);
        _mockUserService.Verify(x => x.AddUserRoleAsync(It.IsAny<Core.Entities.User>(), "User"), Times.Never);
        _mockUserService.Verify(x => x.AddClaimsAsync(It.IsAny<Core.Entities.User>(), It.IsAny<List<Claim>>()), Times.Never);
    }
}
