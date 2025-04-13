using  Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;

namespace Jobfinder.Domain.Events;

public class UserRegisteredEvent (User user) : INotification
{
    public User User { get; init; } = user;
    
}