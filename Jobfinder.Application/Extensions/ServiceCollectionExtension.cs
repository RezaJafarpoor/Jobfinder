using Jobfinder.Application.DomainEventHandlers;
using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Application.Services;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Events;
using Jobfinder.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<JobSeekerService>();
        services.AddScoped<EmployerService>();
        services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();
    }
}