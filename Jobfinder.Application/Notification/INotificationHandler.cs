using Jobfinder.Domain.Interfaces;

namespace Jobfinder.Application.Notification;

public interface INotificationHandler<in TNotification> where TNotification : INotification
{
    Task Handle(TNotification notification);
}