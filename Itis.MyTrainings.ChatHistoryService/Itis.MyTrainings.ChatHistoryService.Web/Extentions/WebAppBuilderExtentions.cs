using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;
using Itis.MyTrainings.ChatHistoryService.Core.Services;
using Itis.MyTrainings.ChatHistoryService.PostgreSql;
using Itis.MyTrainings.ChatHistoryService.PostgreSql.Repository;
using Itis.MyTrainings.ChatHistoryService.Web.Masstransit.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.ChatHistoryService.Web.Extentions;

public static class WebAppBuilderExtentions
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IChatHistoryRecordService, ChatHistoryRecordService>();
    }
    
    public static void AddAppRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatHistoryRepository, ChatHistoryRepository>();
    }
    
    public static void AddAppDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ServiceDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDatabase"));
        });
    }

    public static void AddMessageBroker(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(bus =>
        {
            bus.AddConsumer<ChatHistoryRecordConsumer>();
            bus.AddConsumer<UnicastChatHistoryRecordConsumer>();
            
            bus.UsingRabbitMq((context, config) =>
            {
                config.Host("rabbitmq", "/", h =>
                {
                    h.Username(builder.Configuration["RabbitMQ:UserName"]);
                    h.Password(builder.Configuration["RabbitMQ:Password"]);
                });

                config.ConfigureEndpoints(context);
            });
        });
        builder.Services.AddMassTransitHostedService();
    }
}