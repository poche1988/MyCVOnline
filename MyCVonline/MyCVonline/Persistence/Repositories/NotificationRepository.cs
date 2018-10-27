using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;

namespace MyCVonline.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;
        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<UserNotification> GetUserNotifications()
        {
            var currentUserId = ClaimsPrincipal.Current.Identity.GetUserId();
            return _context.UserNotifications
                .Where(u => u.UserID == currentUserId)
                .Include(N => N.Notification)
                .Include(N => N.Notification.News)
                .OrderByDescending(u => u.Notification.Date)
                .Take(10)
                .ToList();
        }

        public IEnumerable<UserNotification> GetUnreadUserNotifications() {
            var currentUserId = ClaimsPrincipal.Current.Identity.GetUserId();
            return 
                _context.UserNotifications
                .Where(u => u.IsRead == false && u.UserID == currentUserId)
                .ToList();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}