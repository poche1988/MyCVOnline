using System;

namespace MyCVonline.Core.Models
{
    public class Activity
    {
        public int ActivityId { get; private set; }

        public DateTime Date { get; private set; }

        public ActivityType Type { get; private set; }
        
        protected Activity()
        {
            //default constructor for entity
        }

        public Activity(ActivityType t)
        {
            Type = t;
            Date = DateTime.Now;
        }


        public void AddUserActivity(ApplicationUser user)
        {
            //Add activity for one user
            user.AddActivity(this);

        }

        public enum ActivityType {
            Register,
            Login
        }
    }
}