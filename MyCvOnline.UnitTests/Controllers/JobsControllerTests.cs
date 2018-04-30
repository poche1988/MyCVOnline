using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCvOnline.UnitTests.Extensions;
using MyCVonline.Controllers;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using MyCVonline.Core.ViewModels;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace MyCvOnline.UnitTests.Controllers
{
    [TestClass]
    public class JobsControllerTests
    {
        private JobsController _jobController;
        private Mock<IJobRepository> _MockRepository;
        private Mock<IUserRepository> _MockUsersRepository;
        private ApplicationUser _usu;
        public JobsControllerTests()
        {

            //mocking repository and unit of work
            _MockRepository = new Mock<IJobRepository>();
            _MockUsersRepository = new Mock<IUserRepository>();

            var mockUoW = new Mock<IUnitOfWork>();

            mockUoW.Setup(u => u.Jobs).Returns(_MockRepository.Object);
            mockUoW.Setup(u => u.Users).Returns(_MockUsersRepository.Object);

            _usu = Extensions.ControllerExtensions.GetUser();
            _MockUsersRepository.Setup(r => r.GetUser()).Returns(_usu);

            _jobController = new JobsController(mockUoW.Object);
            _jobController.MockCurrentUserForMVC("1", "user1@domain.com");

            
        }
        [TestMethod]
        public void Index_ShoudReturn_DefaultView()
        {
            //using FluentMVCTesting
            _jobController.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void IndexModel_ShoudBe_JobIndexViewModel()
        {
            var result = _jobController.Index() as ViewResult;
            Assert.IsInstanceOfType(result.Model, typeof(JobIndexViewModel));
        }

        [TestMethod]
        public void Create_ShoudReturns_JobFormViewName()
        {
            var result = _jobController.Create() as ViewResult;
            Assert.AreEqual("JobForm", result.ViewName);
        }

        [TestMethod]
        public void CreateModel_ShoudBe_JobFormViewModel()
        {
            var result = _jobController.Create() as ViewResult;
            Assert.IsInstanceOfType(result.Model, typeof(JobFormViewModels));
        }

        [TestMethod]
        public void Create_ShouldReturnsJobFormView_ifModelStateIsNotValid()
        {
            _jobController.ModelState.AddModelError("", "mock error message");
            JobFormViewModels model = new JobFormViewModels();
            _jobController.WithCallTo(x => x.Create(model)).ShouldRenderView("JobForm");
        }

        [TestMethod]
        public void Create_ShouldRedirectsToIndex_IfModelStateIsValid()
        {
            JobFormViewModels model = new JobFormViewModels(); 
            //using FluentMVCTesting
            _jobController.WithCallTo(x => x.Create(model)).ShouldRedirectTo(x=>x.Index);
        }

        [TestMethod]
        public void Update_NoJobWithGivenIdExists_ShouldReturnNotFound()
        {
            
            var result = _jobController.Update(1);
            //using fluentassertions
            result.Should().BeOfType<System.Web.Mvc.HttpNotFoundResult>();
            
        }

        [TestMethod]
        public void Update_NoActiveJobWithGivenIdExists_ShouldReturnNotFound()
        {
            var JobOb = new Job() { User = _usu, Active = false };
            _MockRepository.Setup(r => r.GetJob(1)).Returns(JobOb);
            var result = _jobController.Update(1);
            //using fluentassertions
            result.Should().BeOfType<System.Web.Mvc.HttpNotFoundResult>();

        }

        [TestMethod]
        public void Update_JobsOfAnotherUser_ShouldReturnViewError()
        {
            var JobOb = new Job() { User = Extensions.ControllerExtensions.GetDifferentUser(), Active = true };
            _MockRepository.Setup(r => r.GetJob(1)).Returns(JobOb);
            
            _jobController.WithCallTo(x => x.Update(1)).ShouldRenderView("Error");
        }

        [TestMethod]
        public void Update_ValidRequest_ShouldReturnViewJobForm()
        {
            var JobOb = new Job() { User = _usu, Active = true };
            _MockRepository.Setup(r => r.GetJob(1)).Returns(JobOb);

            _jobController.WithCallTo(x => x.Update(1)).ShouldRenderView("JobForm");
        }

        [TestMethod]
        public void Update_ShouldReturnsJobFormView_IfModelStateIsNotValid()
        {
            _jobController.ModelState.AddModelError("", "mock error message");
            JobFormViewModels model = new JobFormViewModels();
            _jobController.WithCallTo(x => x.Update(model)).ShouldRenderView("JobForm");
        }

        [TestMethod]
        public void Update_ShouldRedirectsToIndex_IfModelStateIsValid()
        {
            var JobOb = new Job() { User = _usu, Active = true };
            _MockRepository.Setup(r => r.GetJob(1)).Returns(JobOb);

            JobFormViewModels model = new JobFormViewModels()
            {
                ID = 1,
                From = "10/10/2005",
                To = "11/10/2006",
            };
            _jobController.WithCallTo(x => x.Update(model)).ShouldRedirectTo(x => x.Index);
        }
    }
}
