using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUser;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IRoleService> _mockRoleService;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockRoleService = new Mock<IRoleService>();
        _handler = new RegisterUserCommandHandler(_mockUserService.Object, _mockRoleService.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Email = "existingemail@example.com",
            UserName = "newuser",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe",
            Role = "User"
        };

        _mockUserService.Setup(x => x.FindUserByEmailAsync(command.Email))
            .ReturnsAsync(new Core.Entities.User());

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenUserNameAlreadyExists()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Email = "newemail@example.com",
            UserName = "existinguser",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe",
            Role = "User"
        };

        _mockUserService.Setup(x => x.FindUserByEmailAsync(command.Email))
            .ReturnsAsync((Core.Entities.User)null);

        _mockUserService.Setup(x => x.FindUserByUserNameAsync(command.UserName))
            .ReturnsAsync(new Core.Entities.User());

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowEntityNotFoundException_WhenRoleDoesNotExist()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Email = "newemail@example.com",
            UserName = "newuser",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe",
            Role = "NonExistentRole"
        };

        _mockUserService.Setup(x => x.FindUserByEmailAsync(command.Email))
            .ReturnsAsync((Core.Entities.User)null);

        _mockUserService.Setup(x => x.FindUserByUserNameAsync(command.UserName))
            .ReturnsAsync((Core.Entities.User)null);

        _mockRoleService.Setup(x => x.IsRoleExistAsync(command.Role))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Role>>(() => 
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldRegisterUserSuccessfully_WhenAllChecksPass()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Email = "newemail@example.com",
            UserName = "newuser",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe",
            Role = "User"
        };

        var user = new Core.Entities.User
        {
            UserName = command.UserName,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email
        };

        var registrationResult = IdentityResult.Success;

        _mockUserService.Setup(x => x.FindUserByEmailAsync(command.Email))
            .ReturnsAsync((Core.Entities.User)null);

        _mockUserService.Setup(x => x.FindUserByUserNameAsync(command.UserName))
            .ReturnsAsync((Core.Entities.User)null);

        _mockRoleService.Setup(x => x.IsRoleExistAsync(command.Role))
            .ReturnsAsync(true);

        _mockUserService.Setup(x => x.RegisterUserAsync(It.IsAny<Core.Entities.User>(), command.Password))
            .ReturnsAsync(registrationResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(registrationResult, result.Result);
        _mockUserService.Verify(x => x.RegisterUserAsync(It.IsAny<Core.Entities.User>(), command.Password), Times.Once);
        _mockUserService.Verify(x => x.AddUserRoleAsync(It.IsAny<Core.Entities.User>(), command.Role), Times.Once);
        _mockUserService.Verify(x => x.AddClaimsAsync(It.IsAny<Core.Entities.User>(), It.IsAny<List<Claim>>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotAddClaims_WhenRegistrationFails()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            Email = "newemail@example.com",
            UserName = "newuser",
            Password = "Password123!",
            FirstName = "John",
            LastName = "Doe",
            Role = "User"
        };

        var user = new Core.Entities.User
        {
            UserName = command.UserName,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email
        };

        var failedRegistrationResult = IdentityResult.Failed();

        _mockUserService.Setup(x => x.FindUserByEmailAsync(command.Email))
            .ReturnsAsync((Core.Entities.User)null);

        _mockUserService.Setup(x => x.FindUserByUserNameAsync(command.UserName))
            .ReturnsAsync((Core.Entities.User)null);

        _mockRoleService.Setup(x => x.IsRoleExistAsync(command.Role))
            .ReturnsAsync(true);

        _mockUserService.Setup(x => x.RegisterUserAsync(It.IsAny<Core.Entities.User>(), command.Password))
            .ReturnsAsync(failedRegistrationResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(failedRegistrationResult, result.Result);
        _mockUserService.Verify(x => x.RegisterUserAsync(It.IsAny<Core.Entities.User>(), command.Password), Times.Once);
        _mockUserService.Verify(x => x.AddUserRoleAsync(It.IsAny<Core.Entities.User>(), command.Role), Times.Never);
        _mockUserService.Verify(x => x.AddClaimsAsync(It.IsAny<Core.Entities.User>(), It.IsAny<List<Claim>>()), Times.Never);
    }
}
