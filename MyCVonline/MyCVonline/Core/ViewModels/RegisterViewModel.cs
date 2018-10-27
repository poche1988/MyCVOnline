using MyCVonline.Core.ViewModels.GlobalResources;
using MyCVonline.Core.ViewModels.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace MyCVonline.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("doesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "Email address already registered!")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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

        public RegisterViewModel()
        {
            Photo = "DefaultUser.png";
        }
    }

    
}