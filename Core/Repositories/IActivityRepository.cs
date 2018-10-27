using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.Repositories
{
    public interface IActivityRepository
    {
        
        IEnumerable<UserActivity> GetUsersActivities();
    }
}
