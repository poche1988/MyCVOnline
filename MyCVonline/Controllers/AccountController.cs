using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MyCVonline.Core;
using MyCVonline.Core.Models;
using MyCVonline.Core.ViewModels;
using MyCVonline.Core.ViewModels.ValidationAttributes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static MyCVonline.Core.Models.Activity;

namespace MyCVonline.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUnitOfWork _UnitOfWork;


        public AccountController(IUnitOfWork unitofwork)
        {
            _UnitOfWork = unitofwork;
            
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUnitOfWork unitofwork)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _signInManager = signInManager;
            _UnitOfWork = unitofwork;
           
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        //public UserManager<ApplicationUser> UserManager { get; private set; }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        var userActivity = new Activity(ActivityType.Login);
                        userActivity.AddUserActivity(_UnitOfWork.Users.GetUserByEmail(model.Email));
                        _UnitOfWork.complete();
                        return RedirectToLocal(returnUrl);
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async System.Threading.Tasks.Task<ActionResult> LinkedINAuth(string code, string state)
        {
            try
            {
                //Get Accedd Token
                var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken");
                var request = new RestRequest(Method.POST);
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", code);
                //request.AddParameter("redirect_uri", "http://localhost:50610/Account/LinkedINAuth");
                request.AddParameter("redirect_uri", "http://mycvonline.co.nz/Account/LinkedINAuth");
                request.AddParameter("client_id", "812hza79ipgmfb");
                request.AddParameter("client_secret", "q9ce796teap9MXt4");
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                //Fetch AccessToken
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                LinkedINVM linkedINVM = jsonSerializer.Deserialize<LinkedINVM>(content);

                //Set REST Client
                client = new RestClient("https://api.linkedin.com/v1/people/~?oauth2_access_token=" + linkedINVM.access_token + "&format=json");

                //getting user data

                var apiRequestUri = new Uri("https://api.linkedin.com/v1/people/~:(id,email-address,first-name,last-name,positions,headline,summary,location,picture-urls::(original))?format=json");

                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + linkedINVM.access_token);
                    var json = webClient.DownloadString(apiRequestUri);
                    dynamic linkedInUserInfo = JsonConvert.DeserializeObject(json);

                    //check if user exist
                    var user = _UnitOfWork.Users.GetUserByEmail(linkedInUserInfo.emailAddress.ToString());

                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        
                    }
                    else
                    {
                        //register with data retrieved
                        RegisterViewModel vm = new RegisterViewModel();


                        vm.Email = linkedInUserInfo.emailAddress;
                        vm.Name = linkedInUserInfo.firstName + " " + linkedInUserInfo.lastName;
                        vm.Password = linkedInUserInfo.id;
                        vm.ConfirmPassword = linkedInUserInfo.id;
                        vm.Profession = (linkedInUserInfo.positions._total > 0) ? linkedInUserInfo.positions.values[0].title : linkedInUserInfo.headline;
                        vm.Summary = (linkedInUserInfo.summary != null) ? linkedInUserInfo.summary : "your summary...";
                        vm.Photo = (linkedInUserInfo.pictureUrls != null) ? linkedInUserInfo.pictureUrls.values[0] : "DefaultUser.png";
                        vm.Address = linkedInUserInfo.location.name;

                        await Register(vm);


                        //adding current position if it exist
                        //if (linkedInUserInfo.positions._total > 0)
                        //{
                        //    JobFormViewModels jvm = new JobFormViewModels();
                        //    jvm.JobTitle = linkedInUserInfo.positions.values[0].title;
                        //    jvm.JobDescription = (linkedInUserInfo.positions.values[0].summary != null) ? linkedInUserInfo.positions.values[0].summary : "";
                        //    jvm.CompanyName = linkedInUserInfo.positions.values[0].company.name;
                        //    jvm.From = "01/" + linkedInUserInfo.positions.values[0].startDate.month + "/" + linkedInUserInfo.positions.values[0].startDate.year;

                        //    if ((bool)linkedInUserInfo.positions.values[0].isCurrent)
                        //    {
                        //        jvm.OnGoing = linkedInUserInfo.positions.values[0].isCurrent;
                        //        jvm.To = DateTime.Today.ToString("dd-MM-yyyy");
                        //    }
                        //    else jvm.To = "01/" + linkedInUserInfo.positions.values[0].endDate.month + "/" + linkedInUserInfo.positions.values[0].startDate.year;

                        //    _jobcontroller.SaveNewJob(jvm);

                        //    _UnitOfWork.complete();

                        //}
                    }
                }

                return RedirectToAction("Index", "Account");
            }
            catch
            {
                throw;
            }
        }


        public ActionResult Index()
        {
            IEnumerable<Section> sections = _UnitOfWork.Sections.GetSections();
            var user = _UnitOfWork.Users.GetUser();
            MyCvViewModel model = new MyCvViewModel
            {
                CurrentUser = user,
                Qualifications = _UnitOfWork.Qualifications.GetQualifications(),
                Jobs = _UnitOfWork.Jobs.GetJobs(),
                SectionsUnderEducation = sections.Where(s => s.Position.ID == 1).ToList(),
                SectionsAboveEducation = sections.Where(s => s.Position.ID == 2).ToList(),
                SectionsUnderJob = sections.Where(s => s.Position.ID == 3).ToList(),
                SectionsAboveJob = sections.Where(s => s.Position.ID == 4).ToList(),

            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel viewmodel = new RegisterViewModel();
            return View(viewmodel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userPhoto = "DefaultUser.png";
                if (model.File != null)
                {
                    model.File.SaveAs(HttpContext.Server.MapPath("~/Images/Users/") + model.File.FileName);
                    userPhoto = model.File.FileName;
                }

                if (model.Photo.Substring(0, 5) == "https") // if it's true it means that the register action is called from linkedinauth
                userPhoto = model.Photo;
                
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Address = model.Address,
                    Profession = model.Profession,
                    PhoneNumber = model.PhoneNumber,
                    Summary = model.Summary,
                    Photo = userPhoto,
                    Birthdate = DateTime.Today
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    UserManager.AddToRole(user.Id, "User");

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account. You have to confirm your E-maill to be able to recover your password.", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    generateWelcomeNotificationAndSaveActivity(user);

                    _UnitOfWork.complete();

                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);


            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        [NonAction]
        public void generateWelcomeNotificationAndSaveActivity(ApplicationUser user)
        {
            var news = new News
            {
                Title = "Welcome, " + user.Name + "!!",
                Content = "Help",
                Date = DateTime.Today,
            };

            _UnitOfWork.News.AddNews(news);
            var notif = new Notification(news, "Help yourself with this tutorial");
            //sending welcome notification with tutorial to the new user
            ApplicationUser currentUser = _UnitOfWork.Users.GetUserById(user.Id);
            notif.sendNotification(currentUser);

            //adding user activity
            var userActivity = new Activity(ActivityType.Register);
            userActivity.AddUserActivity(currentUser);
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            ApplicationUser usuario = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            UpdateProfileViewModel viewmodel = new UpdateProfileViewModel
            {
                UserId = usuario.Id,
                Name = usuario.Name,
                PhoneNumber = usuario.PhoneNumber,
                Photo = usuario.Photo,
                Profession = usuario.Profession,
                Summary = usuario.Summary,
                Birthdate = usuario.Birthdate.ToString("dd-MM-yyyy"),
                Address = usuario.Address,
                Email = usuario.Email
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(UpdateProfileViewModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = _UnitOfWork.Users.GetUser();
                user.Name = model.Name;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Birthdate = DateTime.Today; //fixing a bug
                user.Profession = model.Profession;
                user.Summary = model.Summary;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                if (model.File != null)
                {
                    model.File.SaveAs(HttpContext.Server.MapPath("~/Images/Users/") + model.File.FileName);
                    user.Photo = model.File.FileName;
                }
                else user.Photo = model.Photo;

                try
                {
                    _UnitOfWork.complete();

                    return RedirectToAction("Index", "Account");
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }
            else return View(model);
        }


        [HttpGet]
        public ActionResult UpdateProfileAjax()
        {
            ApplicationUser usuario = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            UpdateProfileViewModel viewmodel = new UpdateProfileViewModel
            {
                UserId = usuario.Id,
                Name = usuario.Name,
                PhoneNumber = usuario.PhoneNumber,
                Photo = usuario.Photo,
                Profession = usuario.Profession,
                Summary = usuario.Summary,
                Birthdate = usuario.Birthdate.ToString("dd-MM-yyyy"),
                Address = usuario.Address,
                Email = usuario.Email
            };
            return View("UpdateProfileFormAjax", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateProfileAjax(UpdateProfileViewModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = _UnitOfWork.Users.GetUser();
                user.Name = model.Name;
                user.Profession = model.Profession;
                user.Summary = model.Summary;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;


                _UnitOfWork.complete();
                model.success = true;

            }
            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult FileUpload(IEnumerable<HttpPostedFileBase> files)
        {
            var validator = new ValidatePhotoAttribute();
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (validator.IsValid(file))
                    {
                        // Verify that the user selected a file
                        if (file != null && file.ContentLength > 0)
                        {
                            ApplicationUser user = _UnitOfWork.Users.GetUser();

                            // extract only the fielname
                            var fileName = Path.GetFileName(file.FileName);
                            // TODO: need to define destination
                            var path = Path.Combine(Server.MapPath("~/Images/Users"), fileName);
                            file.SaveAs(path);
                            user.Photo = fileName;
                            _UnitOfWork.complete();
                        }
                    }
                    else
                    {
                        return Json(new { Error = "select a PNG or JPG image smaller than 4MB" });
                    }
                }
            }
            return Json(files);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ResetPassword", model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [Authorize]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePassword(UpdatePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);


            if (user == null || !UserManager.CheckPassword(user, model.CurrentPassword))
            {
                // Don't reveal that the user does not exist or the current password is wrong
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Home", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Account");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        [AllowAnonymous]
        [HttpPost]
        public JsonResult doesEmailExist(string email)
        {

            ApplicationUser user = UserManager.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null && user.Id == User.Identity.GetUserId())
            {
                return Json(true);
            }

            return Json(user == null);
        }


    }
}