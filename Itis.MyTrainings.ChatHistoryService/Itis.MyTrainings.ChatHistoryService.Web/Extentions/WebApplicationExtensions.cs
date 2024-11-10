using Itis.MyTrainings.ChatHistoryService.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.ChatHistoryService.Web.Extentions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Применить миграцию к бд
    /// </summary>
    /// <param name="host">хост</param>
    public static async Task MigrateDbAsync(this WebApplication host)
    {
        try
        {
            await using var scope = host.Services.CreateAsyncScope();
            var sp = scope.ServiceProvider;

            await using var db = sp.GetRequiredService<ServiceDbContext>();

            await db.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            host.Logger.LogError(e, "Error while migrating the database");
            Environment.Exit(-1);
        }
    }
    
}