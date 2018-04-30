using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.ViewModels
{
    public class JobIndexViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
        public bool ShowActions { get; set; }
    }
}