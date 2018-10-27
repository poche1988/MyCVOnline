using MyCVonline.Controllers;
using MyCVonline.Core.ViewModels.GlobalResources;
using MyCVonline.Core.ViewModels.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MyCVonline.Core.ViewModels
{
    public class NewsFormViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Notification Message")]
        public string NotificationMessage { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        public string Photo { get; set; }

        [ValidatePhoto(ErrorMessageResourceName = "Wrongphoto", ErrorMessageResourceType = typeof(Global))]
        public HttpPostedFileBase File { get; set; }

        public string Heading
        {
            get
            {
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<NewsController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<NewsController, ActionResult>> Create = (c => c.Create(this));

                var action = (ID != 0) ? Edit : Create; //if ID is not 0 is Edit, otherwise is create
                var ActionHeading = (action.Body as MethodCallExpression).Method.Name + " a News";
                return ActionHeading;
            }
            set { }
        }

        public string Action
        {
            get
            {
                //Next line return an expression with the current name of the methods Edit and Create
                Expression<Func<NewsController, ActionResult>> Edit = (c => c.Update(this));
                Expression<Func<NewsController, ActionResult>> Create = (c => c.Create(this));

                var action = (ID != 0) ? Edit : Create; //if ID is not 0 is Edit, otherwise is create
                                                        //with the next line I will extract the method name at runtime
                return (action.Body as MethodCallExpression).Method.Name;
            }
            set { }
        }

        public DateTime DateNews
        {
            get
            {
                try
                {
                    return DateTime.Parse(Date);
                }
                catch
                {
                    return DateTime.Today;
                }
            }
        }

        public NewsFormViewModel()
        {
            Photo = "Photo.png";
            Date = DateTime.Today.ToString("dd-MM-yyyy");
        }
    }
}