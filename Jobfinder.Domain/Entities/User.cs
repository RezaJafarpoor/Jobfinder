using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{

    public Roles UserRole { get; set; }

    public List<IDomainEvent> DomainEvents { get; set; } = [];
    public User()
    {
        
    }

    public User(string email, Roles userRole)
    {
        UserRole = userRole;
        Email = email;
        UserName = email;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents () => DomainEvents.Clear();
}