using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCVonline.Core.Models
{
    public class UserActivity
    {
        [Key, Column(Order = 1)]
        public string UserID { get; private set; }

        [Key, Column(Order = 2)]
        public int ActivityId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Activity Activity { get; private set; }

        

        protected UserActivity()
        {
            //default constructor for entity framework
        }

        //custom constructor
        public UserActivity(ApplicationUser user, Activity activity)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (activity == null)
                throw new ArgumentNullException("activity");
            User = user;
            Activity = activity;
        }

        
    }
}