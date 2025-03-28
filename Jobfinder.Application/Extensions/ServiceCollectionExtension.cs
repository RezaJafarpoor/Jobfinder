using Jobfinder.Application.Interfaces;
using Jobfinder.Application.Services;
using Jobfinder.Domain.Dtos.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();
    }
}