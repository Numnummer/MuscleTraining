using System.Reflection;
using Itis.MyTrainings.Api.Web.AutoMapperProfiles;
using Itis.MyTrainings.Api.Web.Extensions;
using Itis.MyTrainings.Api.Web.SignalR;
using Itis.MyTrainings.Api.Web.SignalR.Filters;
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
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        config.ConfigureEndpoints(context);
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