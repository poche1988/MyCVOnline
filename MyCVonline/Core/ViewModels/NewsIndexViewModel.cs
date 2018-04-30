using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.ViewModels
{
    public class NewsIndexViewModel
    {
        public IEnumerable<News> News { get; set; }

        public string SearchTerm { get; set; }
    }
}