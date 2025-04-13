using Amazon.S3.Model;
using Jobfinder.Application.DomainEventHandlers;
using Jobfinder.Application.Notification;
using Jobfinder.Application.Services;
using Jobfinder.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<JobSeekerService>();
        services.AddScoped<EmployerService>();
        AddPubSubMediator(services);
      
    }

    private static void AddPubSubMediator(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();
        services.AddScoped<NotificationMediator>();
        
    }
}