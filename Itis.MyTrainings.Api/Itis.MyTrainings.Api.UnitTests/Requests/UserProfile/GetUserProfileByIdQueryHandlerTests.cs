using System;
using Itis.MyTrainings.Api.Contracts.Enums;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;
using Moq;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.UserProfile;

public class GetUserProfileByIdQueryHandlerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly GetUserProfileByIdQueryHandler _handler;

    public GetUserProfileByIdQueryHandlerTests()
    {
        // Arrange
        _userServiceMock = new Mock<IUserService>();
        
        // Mocked dbContext is not used directly in the handler but would typically be passed in
        var dbContextMock = new Mock<IDbContext>();

        // Injecting the mocked services into the handler
        _handler = new GetUserProfileByIdQueryHandler(dbContextMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task Handle_UserProfileExists_ReturnsUserProfile()
    {
        // Arrange
        var query = new GetUserProfileByIdQuery(Guid.NewGuid());
        var expectedProfile = new Core.Entities.UserProfile
        {
            Gender = Genders.Man,
            Height = 180,
            Weight = 75,
            PhoneNumber = "1234567890",
            DateOfBirth = new DateTime(1990, 1, 1),
            CreateDate = DateTime.UtcNow
        };

        _userServiceMock
            .Setup(s => s.FindUserProfileByUserId(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProfile);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProfile.Gender, result.Gender);
        Assert.Equal(expectedProfile.Height, result.Height);
        Assert.Equal(expectedProfile.Weight, result.Weight);
        Assert.Equal(expectedProfile.PhoneNumber, result.PhoneNumber);
        Assert.Equal(expectedProfile.DateOfBirth, result.DateOfBirth);
        Assert.Equal(expectedProfile.CreateDate, result.CreateDate);
    }

    [Fact]
    public async Task Handle_UserProfileDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var query = new GetUserProfileByIdQuery(Guid.NewGuid());

        _userServiceMock
            .Setup(s => s.FindUserProfileByUserId(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.UserProfile)null); // No profile found

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException<Core.Entities.UserProfile>>(() =>
            _handler.Handle(query, CancellationToken.None));
    }
}
