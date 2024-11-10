using System;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.User.SignIn;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;

public class SignInQueryHandlerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IJwtService> _mockJwtService;
    private readonly SignInQueryHandler _handler;

    public SignInQueryHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockJwtService = new Mock<IJwtService>();
        _handler = new SignInQueryHandler(_mockUserService.Object, _mockJwtService.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowEntityNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var query = new SignInQuery
        {
            Email = "nonexistent@example.com",
            Password = "Password123!"
        };

        _mockUserService.Setup(x => x.FindUserByEmailAsync(query.Email))
            .ReturnsAsync((Core.Entities.User)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Core.Entities.User>>(() =>
            _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowAuthorizationException_WhenSignInFails()
    {
        // Arrange
        var query = new SignInQuery
        {
            Email = "testuser@example.com",
            Password = "WrongPassword123!"
        };

        var user = new Core.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = query.Email
        };

        var signInResult = SignInResult.Failed;

        _mockUserService.Setup(x => x.FindUserByEmailAsync(query.Email))
            .ReturnsAsync(user);

        _mockUserService.Setup(x => x.SignInWithPasswordAsync(user, query.Password))
            .ReturnsAsync(signInResult);

        // Act & Assert
        await Assert.ThrowsAsync<AuthorizationException>(() =>
            _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldReturnSignInResponseWithToken_WhenSignInSucceeds()
    {
        // Arrange
        var query = new SignInQuery
        {
            Email = "testuser@example.com",
            Password = "CorrectPassword123!"
        };

        var user = new Core.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = query.Email
        };

        var signInResult = SignInResult.Success;
        var role = "User";
        var generatedToken = "GeneratedJwtToken123";

        _mockUserService.Setup(x => x.FindUserByEmailAsync(query.Email))
            .ReturnsAsync(user);

        _mockUserService.Setup(x => x.SignInWithPasswordAsync(user, query.Password))
            .ReturnsAsync(signInResult);

        _mockUserService.Setup(x => x.GetRoleAsync(user))
            .ReturnsAsync(role);

        _mockJwtService.Setup(x => x.GenerateJwt(user.Id, role, user.Email))
            .Returns(generatedToken);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Result.Succeeded);
        Assert.Equal(generatedToken, result.JwtToken);
    }
}
