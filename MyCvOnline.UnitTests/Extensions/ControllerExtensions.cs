using Moq;
using MyCVonline.Core.Models;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Mvc;

namespace MyCvOnline.UnitTests.Extensions
{
    public static class ControllerExtensions
    {

        public static void MockCurrentUserWebApi(this ApiController controller, string UserId, string Username)
        {
            //Mocking IPrincipal - It works only for WEB API
            controller.User = GetIPrincipal(Username, UserId);

        }


        public static void MockCurrentUserForMVC(this Controller controller, string UserId, string Username)
        {
            //Mocking IPrincipal - It works for MVC

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User).Returns(GetIPrincipal(Username, UserId));
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);

            controller.ControllerContext = mock.Object;
        }

        private static IPrincipal GetIPrincipal(string Username, string UserId)
        {
            var claim = new Claim(Username, UserId);
            var mockIdentity = Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);
            return Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity);
        }

        public static ApplicationUser GetUser()
        {
            var user = new ApplicationUser
            {
                Name = "name",
                Profession = "profession",
                Birthdate = DateTime.Today,
                Address = "Address"

            };

            return user;
        }

        public static ApplicationUser GetDifferentUser()
        {
            var user = new ApplicationUser
            {
                Name = "name2",
                Profession = "profession2",
                Birthdate = DateTime.Today,
                Address = "Address2"

            };

            return user;
        }

    }
}
