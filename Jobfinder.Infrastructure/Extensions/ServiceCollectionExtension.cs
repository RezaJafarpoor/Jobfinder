using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Infrastructure.Extensions;

public static class ServiceCollectionExtension  
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("Database"))
                .EnableSensitiveDataLogging();   // WARNING: Remove this this line in production
        });
    }
}