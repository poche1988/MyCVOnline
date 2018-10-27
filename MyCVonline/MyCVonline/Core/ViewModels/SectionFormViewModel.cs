using MyCVonline.Controllers;
using MyCVonline.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MyCVonline.Core.ViewModels
{
    public class SectionFormViewModel
    {
        public int? SectionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Display(Name ="Position in template")]
        public int PositionId { get; set; }

        public IEnumerable<TemplatePosition> Positions { get; set; }

        public string Heading
        {
            get
            {
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<SectionController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<SectionController, ActionResult>> Create = (c => c.Create(this));

                var action = (SectionId != null) ? Edit : Create; //if ID is not 0 is Edit, otherwise is create
                var ActionHeading = (action.Body as MethodCallExpression).Method.Name + " a Section";
                return ActionHeading;
            }
            set { }
        }

        public string Action
        {
            get
            {
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<SectionController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<SectionController, ActionResult>> Create = (c => c.Create(this));

                var action = (SectionId != null) ? Edit : Create; //if ID is not 0 is Edit, otherwise is create
                                                        //with the next line I will extract the method name at runtime
                return (action.Body as MethodCallExpression).Method.Name;
            }
            set { }
        }
    }
}