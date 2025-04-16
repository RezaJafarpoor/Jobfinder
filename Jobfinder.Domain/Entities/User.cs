using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobfinder.Domain.Entities;

public sealed class User : IdentityUser<Guid>, IHasDomainEvents
{

    public Roles UserRole { get; set; }
    [NotMapped] public List<INotification> DomainEvents { get; set; } = [];
    public User()
    {
        
    }

    public User(string email, Roles userRole)
    {
        UserRole = userRole;
        Email = email;
        UserName = email;
    }

    public void AddDomainEvent(INotification domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents () => DomainEvents.Clear();
}