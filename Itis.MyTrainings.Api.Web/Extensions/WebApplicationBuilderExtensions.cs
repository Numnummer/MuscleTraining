using System.Reflection;
using System.Text;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Services;
using Itis.MyTrainings.Api.PostgreSql;
using Itis.MyTrainings.Api.Web.Configurators;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Itis.MyTrainings.Api.Web.Extensions;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Создание и настройка подключения к бд
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigurePostgresqlConnection(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<EfContext>(
            options =>
            {
                options.UseNpgsql(
                    builder.Configuration["Application:DbConnectionString"],
                    opt =>
                    {
                        opt.MigrationsAssembly(typeof(EfContext).GetTypeInfo().Assembly.GetName().Name);
                        opt.EnableRetryOnFailure(
                            15,
                            TimeSpan.FromSeconds(30),
                            null);
                    });
            });
    }
    
    /// <summary>
    /// Добавить службы и зависимости проекта
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(typeof(User));
        builder.Services.AddScoped<IDbContext, EfContext>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IJwtService, JwtService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
        builder.Services.AddScoped<IVkService, VkService>();
        builder.Services.AddScoped<IYandexService, YandexService>();
        builder.Services.AddSingleton<IHttpHelperService, HttpHelperService>();
        builder.Services
            .AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<EfContext>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromMinutes(5));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomSwagger();
    }
    
    /// <summary>
    /// Добавить и настроить авторизацию
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthorization(opt => opt.AddRoles());
    }
    
    /// <summary>
    /// Подключение и настройка JwtBearer
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureJwtBearer(this WebApplicationBuilder builder)
    {
        var issuer = builder.Configuration["JwtSettings:Issuer"];
        var audience = builder.Configuration["JwtSettings:Audience"];
        var secretKey = builder.Configuration["JwtSettings:SecretKey"]!;

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                };
            });
    }
    
    private static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        => services
            .AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
}