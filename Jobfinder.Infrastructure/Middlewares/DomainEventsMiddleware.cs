using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Events;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.AspNetCore.Http;

namespace Jobfinder.Infrastructure.Middlewares;

internal class DomainEventsMiddleware
(ApplicationDbContext dbContext,
    DomainEventDispatcher dispatcher)    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        
        await next(context);
        var events = dbContext.ChangeTracker.Entries<User>()
            .Where(u => u.Entity.DomainEvents.Count != 0)
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();
        foreach (var domainEvent in events)
        {
            await dispatcher.DispatchAsync(domainEvent);
        }

    }
}