using Jobfinder.Infrastructure.Identity;
using Jobfinder.Infrastructure.Persistence;
using Jobfinder.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jobfinder.Infrastructure.Extensions;

public static class ServiceCollectionExtension  
{

    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddIdentity(configuration);
       
    }
    
    
    
    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
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
        
        services.AddAuthorization();
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
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromMinutes(jwtOption.ExpirationTimeInMinute),
                    ValidIssuer = jwtOption.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret))
                };
            });
    }
}