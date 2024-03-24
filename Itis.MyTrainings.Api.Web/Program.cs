using Itis.MyTrainings.Api.Web.Constants;
using Itis.MyTrainings.Api.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureJwtBearer();
builder.ConfigureAuthorization();
builder.ConfigurePostgresqlConnection();
builder.ConfigureCore();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(SpecificOrigins.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.MigrateDbAsync();

app.MapControllers();

app.Run();