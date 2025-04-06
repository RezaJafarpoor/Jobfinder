using Jobfinder.Application.Services;
using Jobfinder.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<RegisterService>();
        services.AddScoped<LoginService>();
        services.AddScoped<RefreshService>();
        services.AddScoped<JobSeekerService>();
        services.AddScoped<EmployerService>();
    }
}