using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyCVonline.Core.ViewModels
{
    [Authorize]
    public class JobFormAjaxViewModels : IValidatableObject
    {
        public int? JobID { get; set; }

        [Required]
        [Display(Name = "From")]
        [DataType(DataType.Date)]
        public string FromDate { get; set; }

        [Required]
        [Display(Name = "To")]
        [DataType(DataType.Date)]
        public string ToDate { get; set; }

        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string JobDescription { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Reference")]
        public string Reference { get; set; }


        [Required]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        public bool active { get; set; }

        [Display(Name = "On Going")]
        public bool OnGoing { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Parse(ToDate) < DateTime.Parse(FromDate))
            {
                yield return
                  new ValidationResult(errorMessage: "\"To\" must be greater than \"From\"",
                                       memberNames: new[] { "To" });
            }
        }


        public DateTime DateFrom
        {
            get
            {
                try
                {
                    return DateTime.Parse(FromDate);
                }
                catch
                {
                    return DateTime.Today;
                }
            }
        }

        public DateTime DateTo
        {
            get
            {
                try
                {
                    return DateTime.Parse(ToDate);
                }
                catch
                {
                    return DateTime.Today;
                }
            }
        }

       

        
    }
}