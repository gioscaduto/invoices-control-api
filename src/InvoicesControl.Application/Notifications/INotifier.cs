using System.Collections.Generic;

namespace InvoicesControl.Application.Notifications
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
