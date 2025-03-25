using Jobfinder.Infrastructure.Identity;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Unicode;

namespace Jobfinder.Infrastructure.Extensions;

public static class ServiceCollectionExtension  
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("AppDbConnection"))
                .EnableSensitiveDataLogging();   // WARNING: Remove this this line in production
        });
    }

    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOption>()
            .Bind(config: configuration.GetSection("JwtSetting"));
        
        services.AddAuthorization();
        services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                var jwtOption = configuration.GetSection("JwtSetting").Get<JwtOption>();
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