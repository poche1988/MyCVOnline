﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCVonline.Core.Models
{
    public class UserNotification
    {
        [Key, Column(Order = 1)]
        public string UserID { get; private set; }

        [Key, Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }

        protected UserNotification()
        {
            //default constructor for entity framework
        }

        //custom constructor
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (notification == null)
                throw new ArgumentNullException("notification");
            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }

    }
}