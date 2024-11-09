using System.Reflection;
using Itis.MyTrainings.Api.Web.Extensions;
using Itis.MyTrainings.Api.Web.Masstransit.Consumers;
using Itis.MyTrainings.Api.Web.SignalR;
using Itis.MyTrainings.Api.Web.SignalR.Filters;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCore();
builder.ConfigureAuthorization();
builder.ConfigureJwtBearer();
builder.ConfigurePostgresqlConnection();
builder.Services.AddSignalR(options =>
{
    options.AddFilter<HubFilter>();
});
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ChatHistoryRecordConsumer>();
    x.AddConsumer<UnicastChatHistoryRecordConsumer>();
    
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddCors();

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

app.Run();