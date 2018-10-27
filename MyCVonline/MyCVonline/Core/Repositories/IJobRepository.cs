using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.Repositories
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetJobs();

        Job GetJob(int id);

        void Add(Job job);
    }
}
