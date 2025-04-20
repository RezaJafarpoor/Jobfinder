using Amazon.Runtime.Internal.Util;
using Amazon.S3;
using Jobfinder.Application.Commons;
using Jobfinder.Application.Commons.Identity;
using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Email;
using Jobfinder.Infrastructure.Identity;
using Jobfinder.Infrastructure.Middlewares;
using Jobfinder.Infrastructure.Persistence.Minio;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Jobfinder.Infrastructure.Repositories;
using Jobfinder.Infrastructure.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;

namespace Jobfinder.Infrastructure.Extensions;

public static class ServiceCollectionExtension  
{

    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false });
        foreach (var type in types)
        {
            var serviceInterfaces = type.GetInterfaces();
            foreach (var iFace in serviceInterfaces)
            {
                if (typeof(IScopedService).IsAssignableFrom(iFace))
                    services.AddScoped(iFace, type);
            }
        }

        services.AddScoped<DomainEventsMiddleware>();
        services.AddPersistence(configuration);
        services.AddIdentity(configuration);
        services.AddEmail(configuration);
        services.AddScoped<ISeeder,DbSeed>();

    }
    
    
    
    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<MinioConfig>()
            .Bind(config: configuration.GetSection("MinioConfig"));
        var miniConfig = configuration.GetSection("MinioConfig").Get<MinioConfig>();
        if (miniConfig is null)
            throw new ArgumentException("minio config is missing");

        services.AddSingleton<IAmazonS3>(_ =>
        {
            var config = new AmazonS3Config()
            {
                ServiceURL = miniConfig.ServiceUrl,
                ForcePathStyle = true,
                Timeout = TimeSpan.FromSeconds(miniConfig.TimeOutInSecond),
                UseHttp = true
            };
            return new AmazonS3Client(miniConfig.Username,miniConfig.Password,config);
        });
        services.AddScoped<IMinioRepository, MinioRepository>();
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("AppDbConnection"))
                .EnableSensitiveDataLogging();   // WARNING: Remove this this line in production
        });
    }

    private static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TokenProvider>();
        services.AddOptions<JwtSetting>()
            .Bind(config: configuration.GetSection("JwtSetting"));
        
        services.AddAuthorization(option =>
        {
            option.AddPolicy(AuthPolicies.EmployerOnly.ToString(), policy =>
            {
                policy.RequireRole(Roles.Employer.ToString());
            });
            option.AddPolicy(AuthPolicies.AdminOnly.ToString(), policy =>
            {
                policy.RequireRole(Roles.Admin.ToString());
            });option.AddPolicy(AuthPolicies.JobSeekerOnly.ToString(), policy =>
            {
                policy.RequireRole(Roles.JobSeeker.ToString());
            });
        });
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                var jwtOption = configuration.GetSection("JwtSetting").Get<JwtSetting>();
                if (jwtOption is null)
                {
                    throw new Exception("Something is wrong with Jwt Settings");
                }
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromMinutes(jwtOption.ExpirationTimeInMinute),
                    ValidIssuer = jwtOption.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret))
                };
            });
    }


    private static void AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<Channel<EmailContent>>(_ => Channel.CreateUnbounded<EmailContent>(
            new UnboundedChannelOptions
            {
                AllowSynchronousContinuations = false,
                SingleReader = true,
                SingleWriter = false
            }));
        services.AddHostedService<EmailBackgroundService>();
    }

    public static void AddInfrastructureMiddleware(this WebApplication app)
    {
        app.UseMiddleware<DomainEventsMiddleware>();
    }
}