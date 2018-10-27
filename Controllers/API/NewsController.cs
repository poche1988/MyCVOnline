using MyCVonline.Core;
using MyCVonline.Core.Models;
using System.Net;
using System.Web.Http;

namespace MyCVonline.Controllers.API
{
    [Authorize]
    public class NewsController : ApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        public NewsController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }

        [Route("api/News/Delete/{id}")]
        [HttpPost()]
        public void Delete(int id)
        {
            News News = _UnitOfWork.News.GetNews(id);

            if (News == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            News.IsCanceled = true;
            _UnitOfWork.complete();
        }
    }
}
