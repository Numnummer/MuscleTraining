using Itis.MyTrainings.Api.Contracts.Enums;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.User.GetCurrentUserInfo;
using Itis.MyTrainings.Api.PostgreSql.Migrations;
using Moq;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;

public class GetCurrentUserInfoQueryHandlerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly GetCurrentUserInfoQueryHandler _handler;

    public GetCurrentUserInfoQueryHandlerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _handler = new GetCurrentUserInfoQueryHandler(_userServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentNullException_WhenUserIdIsNull()
    {
        // Arrange
        var query = new GetCurrentUserInfoQuery(null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowEntityNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var query = new GetCurrentUserInfoQuery(Guid.NewGuid());

        _userServiceMock.Setup(x => x.FindUserByIdAsync(query.UserId.Value))
            .ReturnsAsync((Core.Entities.User)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Core.Entities.User>>(() =>
            _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldReturnUserInfo_WhenUserAndProfileExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetCurrentUserInfoQuery(userId);

        var user = new Core.Entities.User
        {
            Id = userId,
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        var userProfile = new Core.Entities.UserProfile
        {
            Id = Guid.NewGuid(),
            Gender = Genders.Man,
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890",
            Height = 180,
            Weight = 75
        };

        _userServiceMock.Setup(x => x.FindUserByIdAsync(userId))
            .ReturnsAsync(user);

        _userServiceMock.Setup(x => x.FindUserProfileByUserId(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userProfile);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(userProfile.Id, result.UserProfileId);
        Assert.Equal("test@example.com", result.Email);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal(userProfile.DateOfBirth, result.DateOfBirth);
        Assert.Equal(userProfile.PhoneNumber, result.PhoneNumber);
        Assert.Equal(userProfile.Height, result.Height);
        Assert.Equal(userProfile.Weight, result.Weight);
    }

    [Fact]
    public async Task Handle_ShouldReturnNullUserProfileFields_WhenUserProfileIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetCurrentUserInfoQuery(userId);

        var user = new Core.Entities.User
        {
            Id = userId,
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        _userServiceMock.Setup(x => x.FindUserByIdAsync(userId))
            .ReturnsAsync(user);

        _userServiceMock.Setup(x => x.FindUserProfileByUserId(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.UserProfile)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Null(result.UserProfileId);
        Assert.Equal("test@example.com", result.Email);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Null(result.Gender);
        Assert.Null(result.DateOfBirth);
        Assert.Null(result.PhoneNumber);
        Assert.Null(result.Height);
        Assert.Null(result.Weight);
    }
}
