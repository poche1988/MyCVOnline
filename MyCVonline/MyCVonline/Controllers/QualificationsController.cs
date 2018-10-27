using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MyCVonline.Controllers
{
    [Authorize]
    public class QualificationsController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public QualificationsController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var UserQualifications = _UnitOfWork.Qualifications.GetQualifications();

            var viewmodel = new QualificationIndexViewModel
            {
                Qualifications = UserQualifications,
                ShowActions = User.Identity.IsAuthenticated
            };
            return View(viewmodel);
        }

        public ActionResult Create()
        {
            var viewmodel = new QualificationFormViewModel()
            {
                Levels = _UnitOfWork.Qualifications.GetQualificationLevels()
            };

            return View("QualificationForm", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QualificationFormViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Levels = _UnitOfWork.Qualifications.GetQualificationLevels();
                return View("QualificationForm", viewmodel);
            }
            var qualification = new Qualification
            {
                UserId = User.Identity.GetUserId(),
                From = viewmodel.DateFrom,
                To = viewmodel.DateTo,
                QualificationTitle = viewmodel.Qualification,
                University = viewmodel.University,
                QualificationLevelId = viewmodel.Level,
                Active = viewmodel.active,
                Highlighted = viewmodel.Highlighted
            };
            _UnitOfWork.Qualifications.Add(qualification);
            _UnitOfWork.complete();
            return RedirectToAction("Index", "Qualifications");
        }

        [HttpGet]
        public ActionResult CreateOrEditWithAjax()
        {
            var viewmodel = new QualificationFormViewModel();
            viewmodel.Levels = _UnitOfWork.Qualifications.GetQualificationLevels();
            return View("QualificationFormAjax", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateOrEditWithAjax(QualificationFormViewModel viewmodel)
        {
            bool successful = false;
            if (ModelState.IsValid)
            {
                if (viewmodel.ID == null)
                {
                    var qualification = new Qualification
                    {
                        UserId = User.Identity.GetUserId(),
                        From = viewmodel.DateFrom,
                        To = viewmodel.DateTo,
                        QualificationTitle = viewmodel.Qualification,
                        University = viewmodel.University,
                        QualificationLevelId = viewmodel.Level,
                        Active = viewmodel.active,
                        Highlighted = viewmodel.Highlighted
                    };

                    _UnitOfWork.Qualifications.Add(qualification);
                }
                else
                {
                    var qualification = _UnitOfWork.Qualifications.GetQualification((int)viewmodel.ID);
                    qualification.From = viewmodel.DateFrom;
                    qualification.To = viewmodel.DateTo;
                    qualification.QualificationTitle = viewmodel.Qualification;
                    qualification.QualificationLevelId = viewmodel.Level;
                    qualification.University = viewmodel.University;
                }

                _UnitOfWork.complete();
                successful = true;
                var user = _UnitOfWork.Users.GetUser();
                var Qualifications = _UnitOfWork.Qualifications.GetQualifications();


                var result = new { Education = Qualifications, success = successful };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else {
                var errors = ViewData.ModelState.Values
                         .SelectMany(f => f.Errors
                                          .Select(x => new {
                                              Error = x.ErrorMessage,
                                              Exception = x.Exception
                                          })).ToList();
                return Json(new { Status = "error", Errors = errors });
            }
         


        }


        public ActionResult Update(int id)
        {
            var QuaObj = _UnitOfWork.Qualifications.GetQualification(id);
            var viewmodel = new QualificationFormViewModel()
            {
                ID = QuaObj.QualificationId,
                Qualification = QuaObj.QualificationTitle,
                From = QuaObj.From.ToString("MM-yyyy"),
                To = QuaObj.To.ToString("MM-yyyy"),
                Level = QuaObj.QualificationLevelId,
                University = QuaObj.University,
                active = QuaObj.Active,
                Highlighted = QuaObj.Highlighted,
                Levels = _UnitOfWork.Qualifications.GetQualificationLevels()
            };

            return View("QualificationForm", viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(QualificationFormViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Levels = _UnitOfWork.Qualifications.GetQualificationLevels();
                return View("QualificationForm", viewmodel);
            }

            var qualification = _UnitOfWork.Qualifications.GetQualification((int)viewmodel.ID);
            qualification.From = viewmodel.DateFrom;
            qualification.To = viewmodel.DateTo;
            qualification.QualificationTitle = viewmodel.Qualification;
            qualification.QualificationLevelId = viewmodel.Level;
            qualification.University = viewmodel.University;

            _UnitOfWork.complete();

            return RedirectToAction("Index", "Qualifications");

        }


    }
}