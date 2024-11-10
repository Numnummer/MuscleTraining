using Itis.MyTrainings.Api.PostgreSql;
using Itis.MyTrainings.Api.UnitTests.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Itis.MyTrainings.Api.UnitTests;

/// <summary>
/// Базовый класс для unit-тестов
/// </summary>
public class UnitTestBase : IDisposable
{
    /// <summary>
    /// Создать контекст с БД в памяти
    /// </summary>
    /// <returns>Контекст EF</returns>
    protected InMemoryContext CreateInMemoryContext(Action<InMemoryContext>? dbSeeder = null)
    {
        var contextOptions = new DbContextOptionsBuilder<EfContext>()
            .UseInMemoryDatabase("TestDbContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var context = new InMemoryContext(contextOptions);

        if (dbSeeder != null)
            dbSeeder.Invoke(context);
        context.SaveChangesAsync().GetAwaiter().GetResult();

        context.ChangeTracker.Clear();
        return context;
    }
    
    /// <inheritdoc/>
    public void Dispose() => 
        GC.SuppressFinalize(this);
}