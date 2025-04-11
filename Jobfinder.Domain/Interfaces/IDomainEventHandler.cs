namespace Jobfinder.Domain.Interfaces;

public interface IDomainEventHandler<in TEvent> where TEvent:IDomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}