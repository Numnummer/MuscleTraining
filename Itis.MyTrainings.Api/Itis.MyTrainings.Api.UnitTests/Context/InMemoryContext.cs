using Itis.MyTrainings.Api.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.UnitTests.Context;

/// <summary>
/// Контекст базы данных в памяти (для тестов)
/// </summary>
public class InMemoryContext: EfContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public InMemoryContext(DbContextOptions<EfContext> options) : base(options)
    {
    }
}