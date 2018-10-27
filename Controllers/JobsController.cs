using Microsoft.AspNet.Identity;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyCVonline.Controllers
{
    public class JobsController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private ApplicationUser _currentUser;

        public JobsController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
            _currentUser = _UnitOfWork.Users.GetUser();
        }
        
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            IEnumerable<Job> UserJobs = _UnitOfWork.Jobs.GetJobs();
            
            var viewmodel = new JobIndexViewModel
            {
                Jobs = UserJobs,
                ShowActions = User.Identity.IsAuthenticated
            };
            return View(viewmodel);
        }

        public ActionResult Create()
        {
            var viewmodel = new JobFormViewModels();

            return View("JobForm", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobFormViewModels viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("JobForm", viewmodel);
            }

            var job = new Job
            {
                User = _currentUser,
                From = viewmodel.DateFrom,
                To = viewmodel.DateTo,
                OnGoing = viewmodel.OnGoing,
                JobTitle = viewmodel.JobTitle,
                JobDescription = viewmodel.JobDescription,
                Active = viewmodel.active,
                Reference = viewmodel.Reference,
                CompanyName = viewmodel.CompanyName
            };
            _UnitOfWork.Jobs.Add(job);

            _UnitOfWork.complete();

            return RedirectToAction("Index", "Jobs");
        }

        public ActionResult Update(int id)
        {
            var JobObj = _UnitOfWork.Jobs.GetJob(id);
            if (JobObj == null || JobObj.Active==false) return HttpNotFound();

            if (JobObj.User != _currentUser) return View("Error");


            var viewmodel = new JobFormViewModels()
            {
                ID = JobObj.JobId,
                JobTitle = JobObj.JobTitle,
                JobDescription = JobObj.JobDescription,
                CompanyName = JobObj.CompanyName,
                Reference = JobObj.Reference,
                From = JobObj.From.ToString("MM-yyyy"),
                To = JobObj.To.ToString("MM-yyyy"),
                OnGoing = JobObj.OnGoing,
                active = JobObj.Active,

            };

            return View("JobForm", viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(JobFormViewModels viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("JobForm", viewmodel);
            }

            var job = _UnitOfWork.Jobs.GetJob((int)viewmodel.ID);
            job.From = viewmodel.DateFrom;
            job.To = viewmodel.DateTo;
            job.OnGoing = viewmodel.OnGoing;
            job.JobTitle = viewmodel.JobTitle;
            job.JobDescription = viewmodel.JobDescription;
            job.Reference = viewmodel.Reference;
            job.CompanyName = viewmodel.CompanyName;

            _UnitOfWork.complete();

            return RedirectToAction("Index", "Jobs");
            
        }

        [HttpGet]
        public ActionResult CreateOrEditWithAjax()
        {
            var viewmodel = new JobFormAjaxViewModels();
            return View("JobFormAjax", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateOrEditWithAjax(JobFormAjaxViewModels viewmodel)
        {
            bool successful = false;
            if (ModelState.IsValid)
            {
                if (viewmodel.JobID == null)
                {
                    var job = new Job
                    {
                        User = _currentUser,
                        From = viewmodel.DateFrom,
                        To = viewmodel.DateTo,
                        OnGoing = viewmodel.OnGoing,
                        JobTitle = viewmodel.JobTitle,
                        JobDescription = viewmodel.JobDescription,
                        Active = true,
                        Reference = viewmodel.Reference,
                        CompanyName = viewmodel.CompanyName
                    };

                    _UnitOfWork.Jobs.Add(job);
                    _UnitOfWork.complete();
                }
                else
                {
                    var job = _UnitOfWork.Jobs.GetJob((int)viewmodel.JobID);
                    job.From = viewmodel.DateFrom;
                    job.To = viewmodel.DateTo;
                    job.OnGoing = viewmodel.OnGoing;
                    job.JobTitle = viewmodel.JobTitle;
                    job.JobDescription = viewmodel.JobDescription;
                    job.Reference = viewmodel.Reference;
                    job.CompanyName = viewmodel.CompanyName;
                    _UnitOfWork.complete();
                }

                
                successful = true;
                var Jobs = _UnitOfWork.Jobs.GetJobs();


                var result = new { Jobs = Jobs, success = successful };
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

        [NonAction]
        public void SaveNewJob(JobFormViewModels viewmodel) {
            var job = new Job
            {
                User = _currentUser,
                From = viewmodel.DateFrom,
                To = viewmodel.DateTo,
                OnGoing = viewmodel.OnGoing,
                JobTitle = viewmodel.JobTitle,
                JobDescription = viewmodel.JobDescription,
                Active = viewmodel.active,
                Reference = viewmodel.Reference,
                CompanyName = viewmodel.CompanyName
            };
            _UnitOfWork.Jobs.Add(job);
            
        }
    }
}