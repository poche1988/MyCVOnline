using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MyCVonline.Controllers
{

    public class NewsController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public NewsController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }

        [Authorize(Roles = ("Admin"))]
        public ActionResult Index( string query = null)
        {
            var news = _UnitOfWork.News.GetAllNews();

            if (!string.IsNullOrWhiteSpace(query))
            {
                news = news.Where(n => n.Title.Contains(query) || n.Content.Contains(query)).OrderBy(n => n.Date);
            }

            var viewmodel = new NewsIndexViewModel{ News = news, SearchTerm = query};
            return View(viewmodel);
        }


        [Authorize(Roles = ("Admin"))]
        [HttpPost]
        public ActionResult Search(NewsIndexViewModel viewmodel)
        {
            return RedirectToAction("Index", "News", new { query= viewmodel.SearchTerm });
        }

        [Authorize(Roles = ("Admin"))]
        public ActionResult Create()
        {
            var viewmodel = new NewsFormViewModel();

            return View("NewsForm", viewmodel);
        }

        [Authorize(Roles = ("Admin"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsFormViewModel viewmodel)
        {
            string uploadedPhoto;
            string NotificationMessage = viewmodel.NotificationMessage;
            if (!ModelState.IsValid)
            {
                return View("NewsForm", viewmodel);
            }
            if (viewmodel.File != null && viewmodel.File.FileName != "Photo.png") //avoid to override default photo
            {
                viewmodel.File.SaveAs(HttpContext.Server.MapPath("~/Images/News/") + viewmodel.File.FileName);
                uploadedPhoto = viewmodel.File.FileName;
            }
            else
                uploadedPhoto = viewmodel.Photo;

            var news = new News
            {
                NewsId = viewmodel.ID,
                Title = viewmodel.Title,
                Content = viewmodel.Content,
                Date = viewmodel.DateNews,
                Photo = uploadedPhoto
            };

            _UnitOfWork.News.AddNews(news);

            Notification not = new Notification(news, NotificationMessage);

            not.sendNotifications(_UnitOfWork.Users.GetUsers().ToList());

            _UnitOfWork.complete();

            return RedirectToAction("Index", "News");
        }

        [Authorize(Roles = ("Admin"))]

        public ActionResult Update(int id)
        {
            var viewmodel = _UnitOfWork.News.getNewsWithNotificationMessage(id);
            return View("NewsForm", viewmodel);
        }

        [Authorize(Roles = ("Admin"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(NewsFormViewModel viewmodel)
        {
            string uploadedPhoto;
            if (!ModelState.IsValid)
            {
                return View("NewsForm", viewmodel);
            }

            if (viewmodel.File != null)
            {
                viewmodel.File.SaveAs(HttpContext.Server.MapPath("~/Images/News/") + viewmodel.File.FileName);
                uploadedPhoto = viewmodel.File.FileName;
            }
            else
                uploadedPhoto = viewmodel.Photo;

            var News = _UnitOfWork.News.GetNews(viewmodel.ID);

            News.Title = viewmodel.Title;
            News.Date = viewmodel.DateNews;
            News.Content = viewmodel.Content;
            News.Photo = uploadedPhoto;

            _UnitOfWork.complete();

            return RedirectToAction("Index", "News");
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var news = _UnitOfWork.News.GetNews(id);
            if (news.Content == "Help") return RedirectToAction("Help", "Home");
            else return View();
        }  
    }
}