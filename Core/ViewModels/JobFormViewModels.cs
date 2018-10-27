using MyCVonline.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MyCVonline.Core.ViewModels
{
    [Authorize]
    public class JobFormViewModels : IValidatableObject
    {
        public int? ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string From { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string To { get; set; }

        [Display(Name = "On Going")]
        public bool OnGoing { get; set; }

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

       
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Parse(To) < DateTime.Parse(From))
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
                    return DateTime.Parse(From);
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
                    return DateTime.Parse(To);
                }
                catch
                {
                    return DateTime.Today;
                }
            }
        }

        public string Heading
        {
            get
            {
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<JobsController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<JobsController, ActionResult>> Create = (c => c.Create(this));

                var action = (ID != null) ? Edit : Create; //if ID is not 0 is Edit, otherwise is create
                var ActionHeading = (action.Body as MethodCallExpression).Method.Name + " a Job";
                return ActionHeading;
            }
            set { }
        }

        public string Action
        {
            get
            {
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<JobsController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<JobsController, ActionResult>> Create = (c => c.Create(this));

                var action = (ID != null) ? Edit : Create; //if ID is not 0 is Edit, otherwise is create
                //with the next line I will extract the method name at runtime
                return (action.Body as MethodCallExpression).Method.Name;
            }
            set { }
        }

        
        public JobFormViewModels()
        {
            active = true;
            From = DateTime.Today.ToString("MM-yyyy");
            To = DateTime.Today.ToString("MM-yyyy");
        }
    }
}