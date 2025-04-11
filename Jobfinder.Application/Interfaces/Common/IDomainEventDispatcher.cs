using Jobfinder.Domain.Interfaces;

namespace Jobfinder.Application.Interfaces.Common;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent);
}