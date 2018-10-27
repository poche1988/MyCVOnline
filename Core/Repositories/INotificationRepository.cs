using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotifications();

        IEnumerable<UserNotification> GetUnreadUserNotifications();
    }
}
