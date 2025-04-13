using Jobfinder.Application.Notification;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.AspNetCore.Http;

namespace Jobfinder.Infrastructure.Middlewares;

internal class DomainEventsMiddleware
(ApplicationDbContext dbContext,
    NotificationMediator mediator): IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        
        await next(context);
        var events = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();
        foreach (var domainEvent in events)
        {
            await mediator.RaiseEvent(domainEvent);
        }
    }
}