using MyCVonline.Core.ViewModels.GlobalResources;
using MyCVonline.Core.ViewModels.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace MyCVonline.Core.ViewModels
{
    public class UpdateProfileViewModel
    {
        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("doesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "This Email already exists. Please enter a different one.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Profession { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "About you")]
        public string Summary { get; set; }

        public string Photo { get; set; }

        public bool success { get; set; }

        [DataType(DataType.Date)]
        public string Birthdate { get; set; }

        public DateTime Date
        {
            get
            {
                DateTime dateValue;
                if (DateTime.TryParse(Birthdate, out dateValue))
                    return dateValue;
                else
                    return DateTime.MinValue;
            }
        }

        [ValidatePhoto(ErrorMessageResourceName = "Wrongphoto", ErrorMessageResourceType = typeof(Global))]
        public HttpPostedFileBase File { get; set; }

   }
}