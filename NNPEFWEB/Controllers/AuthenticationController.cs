using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.Services;
using NNPEFWEB.ViewModel;
using System.Security.Cryptography;
using System;
using System.Text;

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
            var user = authenticationService.FindUser(login.UserName).Result;
            if (user.ResetPasswordCode != null)
            {
                if (user.Appointment != "1")
                {
                    var auth2 = await authenticationService.SignInUserAsync(login.UserName, login.Password, "false");

                    if (!auth2.Success)
                    {
                        TempData["ErrorMessage"] = auth2.Message;
                        return new RedirectResult(RefererUrl());
                    }

                    return RedirectToAction("sectiondashboard", "Home");
                }
                else
                {
                    var auth = await authenticationService.SignInUserAsync(login.UserName, login.Password, "false");

                    if (!auth.Success)
                    {
                        TempData["ErrorMessage"] = auth.Message;
                        return new RedirectResult(RefererUrl());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                HttpContext.Session.SetString("UserId", user.UserName);
                return RedirectToAction("ResetPassword", "Authentication");
            }
      }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MD5 md5Hash = MD5.Create();
                    var userToReset = HttpContext.Session.GetString("UserId");   // retrieve the user from session
                    if (!string.IsNullOrEmpty(userToReset))
                    {
                        var user = authenticationService.FindUser(userToReset).Result;
                        if (user != null)
                        {
                            string PasswordToHash = GetMd5Hash(md5Hash, model.ConfirmPassword);
                            user.PasswordHash = PasswordToHash;
                            user.UpdatedOn = DateTime.Now.AddMonths(3);
                            authenticationService.updateUserlogins(user);

                            HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");

                            HttpContext.Session.Clear();

                            return RedirectToAction("Login");
                        }
                    }

                }
                catch (Exception ex)
                {
                    string log = ex.Message;
                }
            }
            ModelState.AddModelError("", "Unable to Reset Password. Please Contact your Admin");
            return View(model);
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
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

        

        public async Task<IActionResult> Logout()
        {
            await authenticationService.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}