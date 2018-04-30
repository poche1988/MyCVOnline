using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCVonline.Controllers.API;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using MyCVonline.tests.Extensions;
using System.Web.Http.Results;

namespace MyCVonline.tests.Controllers.Api
{
    [TestClass]
    public class QualificationsControllerTest
    {
        //Mocking the user
        private QualificationsController _controller;
        private Mock<IQualificationRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = "gonzalo";
            _mockRepository = new Mock<IQualificationRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Qualifications).Returns(_mockRepository.Object);
            _controller = new QualificationsController(mockUoW.Object);
            _controller.MockCurrentUser(_userId, "gonzaloamado88@gmail.com");
        }
        [TestMethod]

        //testing the delete method in Api/QualificationsController
        public void Delete_NoQualificationWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Delete(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_QualificationIsDeleted_ShouldReturnNotFound()
        {
            var qualification = new Qualification();
            qualification.Active = false;

            _mockRepository.Setup(r => r.GetQualification(1)).Returns(qualification);

            var result = _controller.Delete(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_UserDeletingOtherUserQualification_ShouldReturnUnauthorized()
        {
            var qualification = new Qualification { Active = true, UserId = _userId + "-" };
            
            _mockRepository.Setup(r => r.GetQualification(1)).Returns(qualification);

            var result = _controller.Delete(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnOk()
        {
            var qualification = new Qualification { Active = true, UserId = _userId };

            _mockRepository.Setup(r => r.GetQualification(1)).Returns(qualification);

            var result = _controller.Delete(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
