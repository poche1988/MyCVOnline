using AutoMapper;
using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using MyCVonline.Core.DTOs;
using MyCVonline.Core.Models;
using System.Linq;
using System.Web.Http;

namespace MyCVonline.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        public NotificationsController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }

        [Route("api/Notifications/GetNewNotifications")]
        public NotificationsDTO GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var UserNotifications = _UnitOfWork.Notifications.GetUserNotifications();
            var listnotifications = UserNotifications.Select(n => n.Notification);

            var notificationsDto = new NotificationsDTO
            {
                UnreadCount = UserNotifications.Where(n => n.IsRead == false).Count(),
                //install AutoMapper, add classMapping profile and add line in global asax to do this:
                Notifications = listnotifications.Select(Mapper.Map<Notification, NotificationDTO>)
            };

            return notificationsDto;
         

        }

        [Route("api/Notifications/MarkNotificationsAsRead")]
        [HttpPost()]
        public IHttpActionResult MarkNotificationsAsRead()
        {
            var userId = User.Identity.GetUserId();
            var UserNotifications = _UnitOfWork.Notifications.GetUnreadUserNotifications().ToList();

            UserNotifications.ForEach(n => n.Read());

            _UnitOfWork.complete();

            return Ok();
        }
    }
}
