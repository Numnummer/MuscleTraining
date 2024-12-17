using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Itis.MyTrainings.ChatHistoryService.Web.AutoMapperProfiles;
using Itis.MyTrainings.ChatHistoryService.Web.CustomMiddlewares;
using Itis.MyTrainings.ChatHistoryService.Web.Extentions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(typeof(ChatMessage));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(mapper =>
{
    mapper.AddProfile<ChatMessageProfile>();
});
builder.AddMessageBroker();
builder.AddAppDbContext();
builder.ConfigureAppOptions();
builder.Services.AddAppRepositories();
builder.Services.AddAppServices();
var app = builder.Build();
await app.MigrateDbAsync();
app.UseMiddleware<ApiKeyCheckMiddleware>();
app.MapControllers();

app.Run();