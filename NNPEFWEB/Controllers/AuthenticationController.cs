using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.Services;
using NNPEFWEB.ViewModel;

namespace NNPEFWEB.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        
        private readonly IUnitOfWorks unitOfWork;

        public AuthenticationController(ApplicationDbContext context,IUnitOfWorks unitOfWork ,IAuthenticationService authenticationService, IUserService userService) :base(userService)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
   
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        // GET: Authentication
        
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var auth = await authenticationService.SignInUserAsync(login.UserName, login.Password, "false");
            
            if (!auth.Success)
            {
                TempData["ErrorMessage"] = auth.Message;
                return new RedirectResult(RefererUrl());
            }
           
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> ClientLogin(LoginViewModel login)
        //{
        //    var auth = await authenticationService.SignInUserAsync(login.EmailAddress, login.Password, "true");

        //    if (auth.Success)
        //    {
        //        var user = await userService.GetUserRolesDevices(auth.Data.Id);              

        //        var response = new ClientResponse
        //        {
        //            User = user
        //        };
        //        return Ok(response.ToResponse());
        //    }

        //    return NotFound(auth);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await authenticationService.(model.Email);
        //        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
        //        // Send an email with this link
        //        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        //        await _emailSender.SendEmailAsync(model.Email, "Reset Password",
        //           $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
        //        return View("ForgotPasswordConfirmation");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        

        public async Task<IActionResult> Logout()
        {
            await authenticationService.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}