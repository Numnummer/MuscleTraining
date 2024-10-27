using Itis.MyTrainings.Api.Web.Extensions;
using Itis.MyTrainings.Api.Web.SignalR;
using Itis.MyTrainings.Api.Web.SignalR.Filters;
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

app.MapControllers();
app.MapHub<SupportChatHub>("/supportChat");

app.Run();