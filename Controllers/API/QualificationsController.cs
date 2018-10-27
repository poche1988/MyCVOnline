using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using System.Web.Http;

namespace MyCVonline.Controllers.API
{
    [Authorize]
    public class QualificationsController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        public QualificationsController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }

        [Route("api/Qualifications/Delete/{id}")]
        [HttpPost()]
        public IHttpActionResult Delete(int id)
        {
            var userAuthenticatedId = User.Identity.GetUserId();

            var qualification = _UnitOfWork.Qualifications.GetQualification(id);

            if (qualification == null || qualification.Active == false)
                return NotFound();

            if (qualification.UserId != userAuthenticatedId)
                return Unauthorized();

            qualification.Active = false;
            _UnitOfWork.complete();
            return Ok();
        }

        
    }
}
