using System.Linq.Expressions;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Itis.MyTrainings.StorageService.Core.Entities;
using Itis.MyTrainings.StorageService.Core.Helpers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(AssemblyHelper.CoreAssembly);
});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; 
});

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = builder.Configuration.GetSection("Minio");
    return new AmazonS3Client(
        new BasicAWSCredentials(config["AccessKey"], config["SecretKey"]),
        new AmazonS3Config
        {
            ServiceURL = config["ServiceUrl"],
            ForcePathStyle = true // Required for some S3-compatible services like Minio
        });
});

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = builder.Configuration.GetSection("MinioTemp");
    return new AmazonS3Client(
        new BasicAWSCredentials(config["AccessKey"], config["SecretKey"]),
        new AmazonS3Config
        {
            ServiceURL = config["ServiceUrl"],
            ForcePathStyle = true // Required for some S3-compatible services like Minio
        });
});

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("redis:6379"));
builder.Services.AddHangfire(config =>
{
    config.UseRedisStorage("redis:6379");
});

builder.Services.Configure<S3Options>(builder.Configuration.GetSection("S3Options"));
var app = builder.Build();

app.MapControllers();
app.UseHangfireDashboard();

RecurringJob.AddOrUpdate(
    "ClearRedisCache",
    (Expression<Action>)(() => RedisCacheHelper.ClearCache()),
    Cron.Daily
);
app.Run();
