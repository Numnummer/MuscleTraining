using System;
using Itis.MyTrainings.Api.Contracts.Enums;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.PutUserProfile;
using Moq;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.UserProfile;

public class PutUserProfileCommandHandlerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IDbContext> _dbContextMock;
    private readonly PutUserProfileCommandHandler _handler;

    public PutUserProfileCommandHandlerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _dbContextMock = new Mock<IDbContext>();
        _handler = new PutUserProfileCommandHandler(_userServiceMock.Object, _dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_UserAndProfileExist_UpdatesUserProfile()
    {
        // Arrange
        var command = new PutUserProfileCommand(Guid.NewGuid());

        var user = new Core.Entities.User
        {
            Id = command.UserId,
            FirstName = "ExistingFirstName",
            LastName = "ExistingLastName"
        };

        var userProfile = new Core.Entities.UserProfile
        {
            UserId = command.UserId,
            PhoneNumber = "0987654321",
            Gender = Genders.Man,
            DateOfBirth = new DateTime(1985, 5, 5),
            Height = 170,
            Weight = 70
        };

        _userServiceMock.Setup(s => s.FindUserByIdAsync(command.UserId))
            .ReturnsAsync(user);

        _userServiceMock.Setup(s => s.FindUserProfileByUserId(command.UserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userProfile);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userServiceMock.Verify(s => s.UpdateUserAsync(It.Is<Core.Entities.User>(u => u.FirstName == command.FirstName && u.LastName == command.LastName)), Times.Once);
        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotNull(result);
        Assert.Equal(userProfile.Id, result.UserProfileId);
        Assert.Equal(command.FirstName, user.FirstName);
        Assert.Equal(command.LastName, user.LastName);
        Assert.Equal(command.PhoneNumber, userProfile.PhoneNumber);
        Assert.Equal(command.Gender, userProfile.Gender);
        Assert.Equal(command.DateOfBirth, userProfile.DateOfBirth);
        Assert.Equal(command.Height, userProfile.Height);
        Assert.Equal(command.Weight, userProfile.Weight);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new PutUserProfileCommand(Guid.NewGuid());

        _userServiceMock.Setup(s => s.FindUserByIdAsync(command.UserId))
            .ReturnsAsync((Core.Entities.User)null); // User not found

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Core.Entities.User>>(() =>
            _handler.Handle(command, CancellationToken.None));

        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UserProfileDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new PutUserProfileCommand(Guid.NewGuid());

        var user = new Core.Entities.User
        {
            Id = command.UserId,
            FirstName = "ExistingFirstName",
            LastName = "ExistingLastName"
        };

        _userServiceMock.Setup(s => s.FindUserByIdAsync(command.UserId))
            .ReturnsAsync(user);

        _userServiceMock.Setup(s => s.FindUserProfileByUserId(command.UserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.UserProfile)null); // User profile not found

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Core.Entities.UserProfile>>(() =>
            _handler.Handle(command, CancellationToken.None));

        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
