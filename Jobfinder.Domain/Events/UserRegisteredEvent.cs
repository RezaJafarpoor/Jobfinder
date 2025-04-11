using  Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;

namespace Jobfinder.Domain.Events;

public record UserRegisteredEvent (User User): IDomainEvent
{
    
}