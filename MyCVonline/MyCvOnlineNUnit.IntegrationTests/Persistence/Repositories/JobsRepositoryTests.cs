using Moq;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Persistence.Repositories;
using NUnit.Framework;
using System;
using System.Data.Entity;

namespace MyCvOnlineNUnit.IntegrationTests.Persistence.Repositories
{
    [TestFixture]
    public class JobsRepositoryTests
    {
        private JobRepository sut;
        private Mock<DbSet<Job>> _mockJobs;
       

        [SetUp]
        public void Setup()
        {
            _mockJobs = new Mock<DbSet<Job>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Jobs).Returns(_mockJobs.Object);

            sut = new JobRepository(mockContext.Object);
        }

        [Test]
        public void ShoulAddJob()
        {
            var jobToAdd = new Job
            {
                JobTitle = "TestTitle",
                JobDescription = "TestDescription",
                OnGoing = false,
                From = DateTime.Today.AddDays(-635),
                To = DateTime.Today.AddDays(-100),
                CompanyName = "TestCompany",
                Active = true,
                User = new ApplicationUser { Name = "TestNameUser" }

            };
            //sut.Add(jobToAdd);

            //Assert.That(jobToAdd.JobId, Is.Not.EqualTo(0));

            //tengo q checekar como testea los respositorios el del curso full stack

        }
    }
}
