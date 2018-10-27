using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;

namespace MyCVonlinenNUnit.Tests.Extensions
{
    public static class ControllerExtensions
    {
        public static void MockCurrentUser(this Controller controller, string userId, string Username)
        {
            //mocking current user
            var identity = new GenericIdentity(Username);
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
                Username));
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                userId));
            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;
        }
    }
}
