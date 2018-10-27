using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels.GlobalResources;
using MyCVonline.Core.ViewModels.ValidationAttributes;
using System.Collections.Generic;
using System.Web;

namespace MyCVonline.Core.ViewModels
{
    public class MyCvViewModel
    {
        public ApplicationUser CurrentUser { get; set; }

        public IEnumerable<Qualification> Qualifications { get; set; }

        public IEnumerable<Job> Jobs { get; set; }
        public List<Section> SectionsUnderJob { get; set; }
        public List<Section> SectionsAboveJob { get; set; }
        public List<Section> SectionsUnderEducation { get; set; }
        public List<Section> SectionsAboveEducation { get; set; }

        

        public string Photo { get; set; }

        
        [ValidatePhoto(ErrorMessageResourceName = "Wrongphoto", ErrorMessageResourceType = typeof(Global))]
        public HttpPostedFileBase File { get; set; }



    }
}