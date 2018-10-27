using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using System.Web.Http;

namespace MyCVonline.Controllers.API
{
    [Authorize]
    public class SectionController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        public SectionController(IUnitOfWork uow)
        {
            _UnitOfWork = uow;
        }

        [Route("api/Section/Delete/{id}")]
        [HttpPost()]
        public IHttpActionResult Delete(int id)
        {
            var userAuthenticatedId = User.Identity.GetUserId();

            var section = _UnitOfWork.Sections.GetSection(id);

            if (section == null)
                return NotFound();

            if (section.UserId != userAuthenticatedId)
                return Unauthorized();

            _UnitOfWork.Sections.Delete(section);
            _UnitOfWork.complete();
            return Ok();
        }
    }
}
