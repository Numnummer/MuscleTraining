using System;
using System.ComponentModel.DataAnnotations;
using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.PostUserProfile;
using Moq;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.UserProfile;

public class PostUserProfileCommandHandlerTests
{
    private readonly Mock<IDbContext> _dbContextMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly PostUserProfileCommandHandler _handler;

    public PostUserProfileCommandHandlerTests()
    {
        _dbContextMock = new Mock<IDbContext>();
        _userServiceMock = new Mock<IUserService>();
        _handler = new PostUserProfileCommandHandler(_dbContextMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task Handle_UserExists_ProfileNotExists_CreatesUserProfile()
    {
        // Arrange
        var command = new PostUserProfileCommand(Guid.NewGuid());

        var user = new Core.Entities.User
        {
            Id = command.UserId,
            FirstName = "John",
            LastName = "Doe",
            UserProfileId = null
        };

        _userServiceMock.Setup(s => s.FindUserByIdAsync(command.UserId))
            .ReturnsAsync(user);

        _dbContextMock.Setup(db => db.UserProfiles.Add(It.IsAny<Core.Entities.UserProfile>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _dbContextMock.Verify(db => db.UserProfiles.Add(It.IsAny<Core.Entities.UserProfile>()), Times.Once);
        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _userServiceMock.Verify(us => us.UpdateUserAsync(It.Is<Core.Entities.User>(u => u.UserProfileId != null)), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<PostUserProfileResponse>(result);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new PostUserProfileCommand(Guid.NewGuid());

        _userServiceMock.Setup(s => s.FindUserByIdAsync(command.UserId))
            .ReturnsAsync((Core.Entities.User)null); // User not found

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Core.Entities.User>>(() =>
            _handler.Handle(command, CancellationToken.None));

        _dbContextMock.Verify(db => db.UserProfiles.Add(It.IsAny<Core.Entities.UserProfile>()), Times.Never);
        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UserProfileAlreadyExists_ThrowsValidationException()
    {
        // Arrange
        var command = new PostUserProfileCommand(Guid.NewGuid());

        var user = new Core.Entities.User
        {
            Id = command.UserId,
            FirstName = "John",
            LastName = "Doe",
            UserProfileId = Guid.NewGuid() // Profile already exists
        };

        _userServiceMock.Setup(s => s.FindUserByIdAsync(command.UserId))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _dbContextMock.Verify(db => db.UserProfiles.Add(It.IsAny<Core.Entities.UserProfile>()), Times.Never);
        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
