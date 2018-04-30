using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCVonline.Controllers;
using MyCVonline.Core;
using MyCVonline.Persistence;

namespace UnitTestMyCvOnline
{
    [TestClass]
    public class JobsControllerTests
    {
        [TestMethod]
        public void TestDetailsView()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            IUnitOfWork UoW = new UnitOfWork(dbContext);
            var controller = new JobsController(UoW);
            var result = controller.Index as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

        }
    }
}
