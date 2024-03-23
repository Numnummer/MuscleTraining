using Itis.MyTrainings.Api.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureJwtBearer();
builder.ConfigureAuthorization();
builder.ConfigurePostgresqlConnection();
builder.ConfigureCore();

const string MyAllowSpecificOrigins = "MyAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, 
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});


var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
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