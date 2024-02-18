using Itis.MyTrainings.Api.Core.Entities;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;

/// <summary>
/// TODO: База тестов написана, осталось написать тесты
/// </summary>
public class Test : UnitTestBase
{
    private UserProfile _userProfile;
    
    public Test()
    {
        _userProfile = new UserProfile()
        {
            Gender = "Машина Антон"
        };
    }
    
    [Fact]
    public async Task Handle_QueryWithFilters_ShouldReturnFilteredEntitiesAsync()
    {
        var dbcontext = CreateInMemoryContext(
            x => x.Add(_userProfile)
            );
        Assert.True(true);
    }
}