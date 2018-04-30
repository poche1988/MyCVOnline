using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using System.Web.Http;

namespace MyCVonline.Controllers.API
{
    [Authorize]
    public class JobsController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        public JobsController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }

        [Route("api/Jobs/Delete/{id}")]
        [HttpPost()]
        public IHttpActionResult Delete(int id)
        {
            var userAuthenticatedId = User.Identity.GetUserId();

            var job = _UnitOfWork.Jobs.GetJob(id);

            if (job == null || job.Active == false)
                return NotFound();

            if (job.User == null || job.User.Id != userAuthenticatedId)
            {
                return Unauthorized();
            }

            job.Active = false;
            _UnitOfWork.complete();
            return Ok();
        }


    }
}

