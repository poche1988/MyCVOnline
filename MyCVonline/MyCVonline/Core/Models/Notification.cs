using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCVonline.Core.Models
{
    public class Notification
    {
        public int NotificationId { get; private set; }

        public DateTime Date { get; private set; }

        public String Message { get; set; }

        public bool IsCanceled { get; set; }

        

        [Required]
        public News News { get; private set; }

        protected Notification()
        {
            //default constructor for entity
        }

        public Notification(News news, string message)
        {
            if (news == null)
                throw new ArgumentNullException("news");
            if (message == null)
                throw new ArgumentNullException("message");
            Date = DateTime.Now;
            News = news;
            Message = message;
        }

        public void sendNotifications(List<ApplicationUser> users)
        {
            //Create UserNotification for each user
            foreach (ApplicationUser user in users)
            {
                user.Notify(this);
            }
        }

        public void sendNotification(ApplicationUser user)
        {
            //Create UserNotification for one user
            user.Notify(this);
            
        }



    }
}