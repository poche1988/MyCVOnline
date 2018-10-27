using MyCVonline.Core;
using MyCVonline.Core.Repositories;
using MyCVonline.Persistence.Repositories;
using MyCVonline.Repositories;

namespace MyCVonline.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public INewsRepository News { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IActivityRepository Activities { get; private set; }
        public IUserRepository Users { get; private set; }
        public IQualificationRepository Qualifications { get; private set; }
        public IJobRepository Jobs { get; private set; }
        public ISectionRepository Sections { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            News = new NewsRepository(_context);
            Users = new UserRepository(_context);
            Qualifications = new QualificationRepository(_context);
            Notifications = new NotificationRepository(_context);
            Jobs = new JobRepository(_context);
            Sections = new SectionRepository(_context);
            Activities = new ActivityRepository(_context);
        }

        public void complete() {
            bool a = _context.ChangeTracker.HasChanges();
            _context.SaveChanges();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}