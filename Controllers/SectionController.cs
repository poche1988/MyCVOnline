using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MyCVonline.Controllers
{
    [Authorize]
    public class SectionController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public SectionController(IUnitOfWork UOW)
        {
            _UnitOfWork = UOW;
        }

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var UserSections = _UnitOfWork.Sections.GetSections();

            var viewmodel = new SectionIndexViewModel
            {
                Sections = UserSections,
                ShowActions = User.Identity.IsAuthenticated
            };
            return View(viewmodel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewmodel = new SectionFormViewModel()
            {
                Positions = _UnitOfWork.Sections.GetPositions()
            };

            return View("SectionForm", viewmodel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectionFormViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Positions = _UnitOfWork.Sections.GetPositions();
                return View("SectionForm", viewmodel);
            }


            var section = new Section
            {
                UserId = User.Identity.GetUserId(),
                Title = viewmodel.Title,
                Description = viewmodel.Description,
                Position = _UnitOfWork.Sections.GetPosition(viewmodel.PositionId),


            };
            _UnitOfWork.Sections.Add(section);
            _UnitOfWork.complete();
            return RedirectToAction("Index", "Section");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var SecObj = _UnitOfWork.Sections.GetSection(id);
            var viewmodel = new SectionFormViewModel()
            {
                SectionId = SecObj.SectionId,
                Description = SecObj.Description,
                Title = SecObj.Title,
                PositionId = SecObj.Position.ID,
                Positions = _UnitOfWork.Sections.GetPositions()
            };

            return View("SectionForm", viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SectionFormViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Positions = _UnitOfWork.Sections.GetPositions();
                return View("SectionForm", viewmodel);
            }

            var section = _UnitOfWork.Sections.GetSection((int)viewmodel.SectionId);
            section.Description = viewmodel.Description;
            section.Title = viewmodel.Title;
            section.Position = _UnitOfWork.Sections.GetPosition(viewmodel.PositionId);

            _UnitOfWork.complete();

            return RedirectToAction("Index", "Section");

        }

        [HttpGet]
        public ActionResult CreateOrEditWithAjax()
        {
            var viewmodel = new SectionFormViewModel();
            viewmodel.Positions = _UnitOfWork.Sections.GetPositions();
            return View("SectionFormAjax", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateOrEditWithAjax(SectionFormViewModel viewmodel)
        {
            bool successful = false;
            if (ModelState.IsValid)
            {
                if (viewmodel.SectionId == null)
                {
                    var section = new Section
                    {
                        UserId = User.Identity.GetUserId(),
                        Title = viewmodel.Title,
                        Description = viewmodel.Description,
                        Position = _UnitOfWork.Sections.GetPosition(viewmodel.PositionId),
                    };

                    _UnitOfWork.Sections.Add(section);
                }
                else
                {
                    var section = _UnitOfWork.Sections.GetSection((int)viewmodel.SectionId);
                    section.Description = viewmodel.Description;
                    section.Title = viewmodel.Title;
                    section.Position = _UnitOfWork.Sections.GetPosition(viewmodel.PositionId);
                }

                _UnitOfWork.complete();
                successful = true;
                var Sections = _UnitOfWork.Sections.GetSections().ToList();
                var SectionsUnderEducation = Sections.Where(s => s.Position.ID == 1).ToList();
                var SectionsAboveEducation = Sections.Where(s => s.Position.ID == 2).ToList();
                var SectionsUnderJob = Sections.Where(s => s.Position.ID == 3).ToList();
                var SectionsAboveJob = Sections.Where(s => s.Position.ID == 4).ToList();


                var result = new {
                    SectionsUnderEducation = SectionsUnderEducation,
                    SectionsAboveEducation = SectionsAboveEducation,
                    SectionsUnderJobs = SectionsUnderJob,
                    SectionsAboveJobs = SectionsAboveJob,
                    success = successful
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var errors = ViewData.ModelState.Values
                         .SelectMany(f => f.Errors
                                          .Select(x => new {
                                              Error = x.ErrorMessage,
                                              Exception = x.Exception
                                          })).ToList();
                return Json(new { Status = "error", Errors = errors });
            }



        }
    }
}