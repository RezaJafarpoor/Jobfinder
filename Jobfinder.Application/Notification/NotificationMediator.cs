using Jobfinder.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Jobfinder.Application.Notification;

public class NotificationMediator(IServiceProvider serviceProvider)
{



    public async Task RaiseEvent<TNotification>(TNotification notification)
        where TNotification : INotification
    {
        var handlers = serviceProvider.GetServices<INotificationHandler<TNotification>>();
        foreach (var handler in handlers)
            await handler.Handle(notification);
    }
}