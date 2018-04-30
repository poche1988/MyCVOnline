using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;

namespace MyCVonline.Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IApplicationDbContext _context;
        public JobRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Job> GetJobs()
        {
            var id = ClaimsPrincipal.Current.Identity.GetUserId();

            return _context.Jobs
                 .Where(q => q.User.Id == id && q.Active == true)
                 .OrderByDescending(q => q.To).ToList();

        }

        public Job GetJob(int id)
        {
            var currentUserId = ClaimsPrincipal.Current.Identity.GetUserId();

            return _context
                .Jobs
                .Include(J=>J.User)
                .Where(j => j.JobId == id && j.Active == true && j.User.Id == currentUserId).FirstOrDefault();
        }

        public void Add(Job job)
        {
            _context.Jobs.Add(job);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}