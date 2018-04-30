using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCVonline.Core.Models;
using MyCVonline.Persistence;
using MyCVonline.Repositories;
using MyCVonline.tests.Extensions;
using System.Data.Entity;

namespace MyCVonline.tests.Persistence.Repositories
{
    [TestClass]
    public class QualificationRepositoryTests
    {
        

        private QualificationRepository _repository;
        private Mock<DbSet<Qualification>> _mockQualifications;
        

        [TestInitialize]
        public void TestInitialize()
        {
            _mockQualifications = new Mock<DbSet<Qualification>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c=>c.Qualifications).Returns(_mockQualifications.Object);
            _repository = new QualificationRepository(mockContext.Object);
            
        }
        [TestMethod]
        public void GetQualification_QualificationNotActive_ShouldNotBeReturned()
        {
            var qualification = new Qualification { QualificationId = 1, Active = false};

            _mockQualifications.SetSource(new [] { qualification });

            var ReturnedQualification = _repository.GetQualification(1);

            ReturnedQualification.Should().BeNull();
        }

        [TestMethod]
        public void GetQualification_WrongId_ShouldNotBeReturned()
        {
            var qualification = new Qualification { QualificationId = 2, Active = true };

            _mockQualifications.SetSource(new[] { qualification });

            var ReturnedQualification = _repository.GetQualification(1);

            ReturnedQualification.Should().BeNull();
        }
    }
}
