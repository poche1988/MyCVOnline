using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace MyCVonline.Persistence.Repositories
{

    public class ActivityRepository : IActivityRepository
    {
        private readonly IApplicationDbContext _context;
        public ActivityRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserActivity> GetUsersActivities()
        {
            return _context.UserActivities
                .Include(a => a.Activity)
                .Include(a => a.User)
                .OrderByDescending(u => u.Activity.Date)
                .Take(50)
                .ToList();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}