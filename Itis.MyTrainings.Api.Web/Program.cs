using Itis.MyTrainings.Api.Web.Constants;
using Itis.MyTrainings.Api.Web.Extensions;
using Itis.MyTrainings.Api.Web.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCore();
builder.ConfigureAuthorization();
builder.ConfigureJwtBearer();
builder.ConfigurePostgresqlConnection();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseExceptionHandling();
app.UseCors(SpecificOrigins.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.ConfigureSignalR();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


await app.MigrateDbAsync();

app.MapControllers();
app.MapHub<NotificationHub>("/notification");

app.Run();