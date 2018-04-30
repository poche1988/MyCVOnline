using MyCVonline.Controllers;
using MyCVonline.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MyCVonline.Core.ViewModels
{
    public class QualificationFormViewModel : IValidatableObject
    {
        public int? ID { get; set; }

        public string Heading
        {
            get
            {
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<QualificationsController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<QualificationsController, ActionResult>> Create = (c => c.Create(this));

                var action = (ID != null) ? Edit : Create; //if ID is not 0 is Edit, otherwise is create
                var ActionHeading = (action.Body as MethodCallExpression).Method.Name + " a Qualification";
                return ActionHeading;
            }
            set { }
        }

        public string Action {
            get{
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<QualificationsController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<QualificationsController, ActionResult>> Create = (c => c.Create(this));
                
                var action = (ID != null) ? Edit:Create; //if ID is not 0 is Edit, otherwise is create
                //with the next line I will extract the method name at runtime
               return (action.Body as MethodCallExpression).Method.Name;
            }
            set { }
        }

        [Required]
        [DataType(DataType.Date)]
        public string From { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string To { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        [Display(Name = "University - Academy")]
        public string University { get; set; }

        public int Level { get; set; }
        public IEnumerable<QualificationLevel> Levels { get; set; }

        public bool active { get; set; }

        public bool Highlighted { get; set; }

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
            get{
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

        
        public QualificationFormViewModel()
        {
            active = true;
            From = DateTime.Today.ToString("MM-yyyy");
            To = DateTime.Today.ToString("MM-yyyy");
        }
    }
}