using Xunit;
using Moq;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Requests.User.CheckUserProfile;
using Itis.MyTrainings.Api.Core.Exceptions;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;


public class CheckUserProfileQueryHandlerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly CheckUserProfileQueryHandler _handler;

    public CheckUserProfileQueryHandlerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _handler = new CheckUserProfileQueryHandler(_userServiceMock.Object);
    }

    [Fact]
    public async Task Handle_UserExistsWithProfileId_ReturnsTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Core.Entities.User { UserProfileId = userId };
        
        _userServiceMock
            .Setup(_ => _.FindUserByIdAsync(userId))
            .ReturnsAsync(user);        
        
        var query = new CheckUserProfileQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Handle_UserExistsWithoutProfileId_ReturnsFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Core.Entities.User { UserProfileId = null };
        
        _userServiceMock
            .Setup(service => service.FindUserByIdAsync(userId))
            .ReturnsAsync(user);

        var query = new CheckUserProfileQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        
        _userServiceMock
            .Setup(service => service.FindUserByIdAsync(userId))
            .ReturnsAsync((Core.Entities.User) null);

        var query = new CheckUserProfileQuery(userId);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Core.Entities.User>>(() => _handler.Handle(query, CancellationToken.None));
    }
}