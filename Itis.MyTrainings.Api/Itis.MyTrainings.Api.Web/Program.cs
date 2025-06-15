using System.Reflection;
using ChatMessageDtos;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using Itis.MyTrainings.Api.Core.Models;
using Itis.MyTrainings.Api.Web.AutoMapperProfiles;
using Itis.MyTrainings.Api.Web.Extensions;
using Itis.MyTrainings.Api.Web.GrpcServices;
using Itis.MyTrainings.Api.Web.Services;
using Itis.MyTrainings.Api.Web.SignalR;
using Itis.MyTrainings.Api.Web.SignalR.Filters;
using Itis.MyTrainings.PaymentService.Web.Protos;
using MassTransit;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCore();
builder.ConfigureAuthorization();
builder.ConfigureJwtBearer();
builder.ConfigurePostgresqlConnection();
builder.Services.Configure<KestrelServerOptions>(options =>
{
   options.Limits.MaxRequestBodySize = int.MaxValue; 
});
builder.Services.AddSignalR(opt =>
{
    opt.MaximumReceiveMessageSize = int.MaxValue;
});
builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<ChatMessageProfile>();
});
builder.WebHost.ConfigureKestrel(options => {
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        
        config.Message<MessageStats>(m => m.SetEntityName("message-stats"));
        config.Publish<MessageStats>(p => 
        {
            p.ExchangeType = "direct";
            p.Durable = true;
        });
        
        // Привязка очереди с routing key
        config.Send<MessageStats>(s => 
        {
            s.UseRoutingKeyFormatter(context => context.Message.UserId.ToString());
        });
        
        config.ReceiveEndpoint("user-stats-queue", e =>
        {
            e.Bind("message-stats", x =>
            {
                x.RoutingKey = "#"; // Подписываемся на все routing keys
                x.ExchangeType = "direct";
                x.Durable = true;
            });
        });

        config.ConfigureEndpoints(context);
    });
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddCors();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddGrpcClient<Transaction.TransactionClient>(o =>
{
    o.Address = new Uri("http://localhost:5101");
});
builder.Services.AddScoped<ITransactionClient, TransactionClient>();
builder.Services.AddScoped<IMessageStatsService, ClickHouseMessageService>();
builder.Services.AddSingleton<IDictionary<Guid, Guid>>(new Dictionary<Guid, Guid>());

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseExceptionHandling();
app.UseAuthentication();
app.UseAuthorization();
app.ConfigureSignalR();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGrpcReflectionService();
}

app.MapHub<NotificationHub>("/notification");


app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyMethod();
    option.AllowCredentials();
    option.SetIsOriginAllowed(origin => true);
});

await app.MigrateDbAsync();
app.MapHealthChecks("health");

app.MapControllers();
app.MapHub<SupportChatHub>("/supportChat");
app.MapGrpcService<MessagingService>();

app.Run();