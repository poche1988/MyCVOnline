using MyCVonline.Core.Models;
using System;
using System.Collections.Generic;

namespace MyCVonline.Core.ViewModels
{
    public class AdminPanelViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public int SignUpsInLastTenDays { get; set; }
        public DateTime LastSignUpDate { get; set; }
        public int UserActivitiesToday { get; set; }
        public string MostActiveUser { get; set; }
        public List<News> News { get; set; }

        public List<ApplicationUser> LastUsers { get; set; }
    }
}