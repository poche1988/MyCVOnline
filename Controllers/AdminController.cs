using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyCVonline.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        
        public AdminController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
        }


        public ActionResult AdminPanel()
        {
            var usersactivities = _UnitOfWork.Activities.GetUsersActivities();
            var users = _UnitOfWork.Users.GetUsers().ToList();
            var model = new AdminPanelViewModel() {
                Users = users,
                News = _UnitOfWork.News.GetAllNews().ToList(),
                SignUpsInLastTenDays = usersactivities
                                    .Where(a => a.Activity.Type == Activity.ActivityType.Register
                                    && a.Activity.Date >= DateTime.Now.AddDays(-10))
                                    .Count(),
                LastSignUpDate = usersactivities
                                .Where(a => a.Activity.Type == Activity.ActivityType.Register)
                                .OrderByDescending(a => a.Activity.Date)
                                .Select(a => a.Activity.Date).FirstOrDefault(),
                UserActivitiesToday = usersactivities
                                    .Where(a => a.Activity.Date >= DateTime.Today)
                                    .Count(),
                MostActiveUser = usersactivities.GroupBy(a => a.User)
                                        .OrderByDescending(g => g.Count())
                                        .Take(1)
                                        .Select(g => g.Key.Name).FirstOrDefault(),

                LastUsers = users.Where(u=>u.EmailConfirmed == true).OrderByDescending(u => u.Id).Take(20).ToList()
            };

            return View(model);
        }
        

    }


}