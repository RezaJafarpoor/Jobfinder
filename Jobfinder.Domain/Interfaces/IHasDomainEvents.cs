using System.ComponentModel.DataAnnotations.Schema;

namespace Jobfinder.Domain.Interfaces;

public interface IHasDomainEvents
{
    [NotMapped] List<INotification> DomainEvents { get; set; }
    void AddDomainEvent(INotification domainEvent);

     void ClearDomainEvents();
}