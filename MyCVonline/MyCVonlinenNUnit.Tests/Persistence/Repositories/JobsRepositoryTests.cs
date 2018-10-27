using Moq;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Persistence.Repositories;
using NUnit.Framework;
using System.Data.Entity;

namespace MyCVonlinenNUnit.Tests.Persistence.Repositories
{
    [TestFixture]
    public class JobsRepositoryTests
    {
        private JobRepository sut;
        private Mock<DbSet<Job>> _mockJobs;

        [OneTimeSetUp]
        public void Setup()
        {
            var user = new Mock<ApplicationUser>();
            _mockJobs = new Mock<DbSet<Job>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Jobs).Returns(_mockJobs.Object);

            sut = new JobRepository(mockContext.Object);
        }
        
        [Test]
        public void ShoulGetJobs()
        {
           //var jobs = new Job { }

        }
    }
}
