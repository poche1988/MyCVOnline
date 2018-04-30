using Moq;
using MyCVonline.Controllers;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using MyCVonlinenNUnit.Tests.Extensions;
using NUnit.Framework;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace MyCVonlinenNUnit.Tests.Controllers
{
    [TestFixture]
    public class JobsControllerTests
    {
        private JobsController sut;
        private Mock<IJobRepository> _mockRepository;
        
        [SetUp]
        public void Setup()
        {
            //mocking repository
            _mockRepository = new Mock<IJobRepository>();

            //mocking unitofwork
            var mockUoW = new Mock<IUnitOfWork>();

            mockUoW.SetupGet(u => u.Jobs).Returns(_mockRepository.Object);

            sut = new JobsController(mockUoW.Object);

            sut.MockCurrentUser("1", "gonzaloamado88@gmail.com");
        }

        [Test]
        public void Index_ShouldRenderIndexView()
        {
            sut.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }


        [Test]
        public void Update_NoJobWithGivenIdExist_ShouldReturnNotFound()
        {
            var result = sut.Update(-1);
            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Update_ShouldRenderJobFormView()
        {
            var job = new Job();
            _mockRepository.Setup(r => r.GetJob(1)).Returns(job);

            var result = sut.Update(1) as ViewResult;
            
            Assert.That(result.ViewName, Is.EqualTo("JobForm"));
        }

        
    }
}
