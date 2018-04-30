using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.ViewModels
{
    public class QualificationIndexViewModel
    {
        public IEnumerable<Qualification> Qualifications{ get; set; }
        public bool ShowActions { get; set; }
    }
}