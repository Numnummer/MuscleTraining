using Itis.MyTrainings.PaymentService.Core.Abstractions;
using Itis.MyTrainings.PaymentService.DataAccess;
using Itis.MyTrainings.PaymentService.DataAccess.Repository;
using Itis.MyTrainings.PaymentService.Web.Protos;
using Itis.MyTrainings.PaymentService.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EfContext>(o =>
{
    o.UseNpgsql(builder.Configuration["Application:DbConnectionString"]);
});

builder.Services.AddGrpcClient<RevertTransaction.RevertTransactionClient>(o =>
{
    o.Address = new Uri("http://localhost:5049");
});
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<TransactionService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();