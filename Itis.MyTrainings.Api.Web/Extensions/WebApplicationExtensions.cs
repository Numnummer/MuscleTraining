using Itis.MyTrainings.Api.PostgreSql;
using Itis.MyTrainings.Api.Web.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Web.Extensions;

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

            await using var db = sp.GetRequiredService<EfContext>();

            await db.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            host.Logger.LogError(e, "Error while migrating the database");
            Environment.Exit(-1);
        }
    }
    
    /// <summary>
    /// Добавить службы SignalR
    /// </summary>
    /// <param name="app">хост</param>
    public static void ConfigureSignalR(this WebApplication app)
    {
        app.UseEndpoints(e =>
        {
            e.MapHub<NotificationHub>("/notification");
        });
    }
}