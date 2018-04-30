using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.ViewModels
{
    public class SectionIndexViewModel
    {
        public IEnumerable<Section> Sections { get; set; }
        public bool ShowActions { get; set; }
    }
}