using MyCVonline.Core;
using System.Net;
using System.Web.Http;

namespace MyCVonline.Controllers
{

    [Authorize]
    public class HighlightedQualificationController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        public HighlightedQualificationController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }

        [Route("api/HighlightedQualification/Highlight/{id}")]
        [HttpPost]
        public IHttpActionResult Highlight(int id)
        {
            var qualification = _UnitOfWork.Qualifications.GetQualification(id);

            if (qualification == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (qualification.Highlighted == true) qualification.Highlighted = false;
            else qualification.Highlighted = true;

            _UnitOfWork.complete();
            return Ok();
        }
    }
}
