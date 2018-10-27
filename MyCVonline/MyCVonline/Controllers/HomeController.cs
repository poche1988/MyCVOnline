using System.Web.Mvc;

namespace MyCVonline.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public FileResult Help()
        {
            Response.AppendHeader("content-disposition", "inline; filename=mcvoTutorial.pdf");
            return File("/content/mcvoTutorial.pdf", "application/pdf");
        }

        

       

        
    }
}