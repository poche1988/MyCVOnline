using System.Collections.Generic;

namespace MyCVonline.Core.DTOs
{
    public class NotificationsDTO
    {
        public IEnumerable<NotificationDTO> Notifications { get; set; }

        public int UnreadCount { get; set; }

    }
}