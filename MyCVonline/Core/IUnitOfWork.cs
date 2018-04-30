using MyCVonline.Core.Repositories;
using System;

namespace MyCVonline.Core
{
    public interface IUnitOfWork : IDisposable
    {
        INewsRepository News { get; }
        INotificationRepository Notifications { get; }
        IActivityRepository Activities { get; }
        IUserRepository Users { get; }
        IQualificationRepository Qualifications { get;}
        IJobRepository Jobs { get; }
        ISectionRepository Sections { get; }
        void complete();
    }
}
