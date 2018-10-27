using Microsoft.AspNet.Identity.EntityFramework;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using System.Data.Entity;

namespace MyCVonline.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<QualificationLevel> Qualificationlevels { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<TemplatePosition> Positions { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Section> Sections { get; set; }




        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
    
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
        
    }
}