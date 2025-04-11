using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Infrastructure.Events;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher, IScopedService
{


    public async Task DispatchAsync(IDomainEvent domainEvent)
    {
        var handler = serviceProvider.GetService<IDomainEventHandler<IDomainEvent>>();
        if (handler is not null)
            await handler.HandleAsync(domainEvent);
    }

}