using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCvOnline.UnitTests.Extensions;
using MyCVonline.Controllers.API;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using System.Web.Http.Results;

namespace MyCvOnline.UnitTests.Controllers.Api
{
    [TestClass]
    public class JobsControllerTests
    {
        private JobsController _jobController;
        private Mock<IJobRepository> _MockRepository;
        private string _userId;
        private ApplicationUser _usu;
        
        public JobsControllerTests()
        {
           
            //mocking repository and unit of work
            _MockRepository = new Mock<IJobRepository>();

            var mockUoW = new Mock<IUnitOfWork>();

            mockUoW.Setup(u => u.Jobs).Returns(_MockRepository.Object);

            _jobController = new JobsController(mockUoW.Object);
            _usu = ControllerExtensions.GetUser();
            _userId = _usu.Id;
            _jobController.MockCurrentUserWebApi(_userId + 1, "user1@domain.com");

        }
        [TestMethod]
        public void Delete_NoJobWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _jobController.Delete(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_JobsWithActiveFalse_ShouldReturnNotFound()
        {
            var JobOb = new Job();
            JobOb.Active = false;
            _MockRepository.Setup(r => r.GetJob(1)).Returns(JobOb);

            var result = _jobController.Delete(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_JobsOfAnotherUser_ShouldReturnNotAuthorized()
        {
            var JobOb = new Job() { User = _usu};

            JobOb.Active = true;
            _MockRepository.Setup(r => r.GetJob(1)).Returns(JobOb);

            var result = _jobController.Delete(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnOk()
        {
            _jobController.MockCurrentUserWebApi(_userId, "user1@fomain.com");

            var JobOb = new Job() { User = _usu };
            JobOb.Active = true;
            _MockRepository.Setup(r => r.GetJob(1)).Returns(JobOb);

            var result = _jobController.Delete(1);
            result.Should().BeOfType<OkResult>();
        }

        

        

    }
}
