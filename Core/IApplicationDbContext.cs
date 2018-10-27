using MyCVonline.Core.Models;
using System;
using System.Data.Entity;

namespace MyCVonline.Core
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Job> Jobs { get; set; }
        DbSet<QualificationLevel> Qualificationlevels { get; set; }
        DbSet<Qualification> Qualifications { get; set; }
        DbSet<News> News { get; set; }
       
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }

        DbSet<Activity> Activities { get; set; }
        DbSet<UserActivity> UserActivities { get; set; }


        DbSet<TemplatePosition> Positions { get; set; }

        DbSet<Section> Sections { get; set; }


    }
}
