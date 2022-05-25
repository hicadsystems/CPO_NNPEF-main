using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.Services;
using NNPEFWEB.ViewModel;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using NNPEFWEB.Models;

namespace NNPEFWEB.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly IConfiguration config;
        private readonly IUnitOfWorks unitOfWork;
        private int resetcode;
        Random rnd = new Random();
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager, ApplicationDbContext context,
            IUnitOfWorks unitOfWork ,IAuthenticationService authenticationService,
            IUserService userService, IConfiguration config, ILogger<HomeController> logger) :base(userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
            this.authenticationService = authenticationService;
            this.config = config;
            this.unitOfWork = unitOfWork;
            this.context = context;
            this._logger = logger;
        }

        // GET: Authentication
        [AllowAnonymous]
        public IActionResult Login()
        {
            
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = authenticationService.FindUser(login.UserName).Result;
            if (user == null)
            {
                TempData["ErrorMessage"] = "User Not Found";
                return RedirectToAction("login", "Authentication");
            }
            if (user.ResetPasswordCode != null)
            {
                if (user.Appointment != "Adm")
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
                    var userToReset = HttpContext.Session.GetString("UserId");
                    if (userToReset == null)
                        return RedirectToAction("Login");
                        // retrieve the user from session
                    if (!string.IsNullOrEmpty(userToReset))
                    {
                        var user = authenticationService.FindUser(userToReset).Result;

                        if (user != null)
                        {
                            var token = await userManager.GeneratePasswordResetTokenAsync(user);
                            var resetpass = await userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
                            if (resetpass.Succeeded)
                            {
                                authenticationService.updateUserlogins(user);
                                HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");
                                return RedirectToAction("Login");
                            }
                            //string PasswordToHash = GetMd5Hash(md5Hash, model.ConfirmPassword);
                            //user.PasswordHash = PasswordToHash;
                            //user.UpdatedOn = DateTime.Now.AddMonths(3);
                            //authenticationService.updateUserlogins(user);

                            //HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");

                            //HttpContext.Session.Clear();

                            //return RedirectToAction("Login");
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await authenticationService.FindUserByEmail(forgotPasswordViewModel.Email);
                    if (user != null )
                    {
                        resetcode = rnd.Next(100000, 200000);   // generate random number and send to user mail and phone
                        string message = "Your Password Reset Code is:" + resetcode;
                        HttpContext.Session.SetString("userToReset", user.Email);
                        HttpContext.Session.SetString("UserId", user.UserName);
                        HttpContext.Session.SetString("resetCode", resetcode.ToString());

                        // a call to send email notification
                            string emailFrom = "NN-CPO";
                        string emailSender = config.GetValue<string>("mailconfig:SenderEmail");

                        // add navy email adds
                        if (!string.IsNullOrEmpty(user.Email))
                            SendEmailNotification(emailFrom, emailSender, message, user.Email);

                        return View("ForgotPasswordConfirmation");
                    }

                    return View("ForgotPasswordConfirmation");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    return View("ForgotPasswordConfirmation");
                }
            }

            return View(forgotPasswordViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordAdmin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ResetPasswordAdmin(string resetCode)
        {
            if (!string.IsNullOrEmpty(resetCode))
            {
                try
                {
                    string sessionResetCode = HttpContext.Session.GetString("resetCode");
                    if (resetCode == sessionResetCode)
                    {
                        return RedirectToAction("ResetPassword");
                    }
                    else
                    {
                        TempData["confirmError"] = $"Reset code : {resetCode} is Invalid!!";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    return View(resetCode);
                }
            }
            else
            {
                TempData["confirmError"] = $"Enter Reset code!!";
                return View(resetCode);
            }
        }

        public void SendEmailNotification(string emailFrom, string sender, string messageToSend, string receipient)
        {
            var UserName = config.GetValue<string>("mailconfig:SenderEmail");
            var Password = config.GetValue<string>("mailconfig:Password");
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(receipient));
            message.From = new MailAddress(sender, emailFrom);
            message.Subject = "Password Reset Code";
            message.Body = string.Format(body, emailFrom, sender, messageToSend);
            message.IsBodyHtml = true;

            string host = config.GetValue<string>("mailconfig:Server");
            int port = config.GetValue<int>("mailconfig:Port");
            var enableSSL = Convert.ToBoolean("True");

            SmtpClient SmtpServer = new SmtpClient(host);
            SmtpServer.Port = port; // Also Add the port number to send it, its default for Gmail
            SmtpServer.Credentials = new NetworkCredential(UserName, Password);
            SmtpServer.EnableSsl = enableSSL;
            SmtpServer.Timeout = 200000; // Add Timeout property

            try
            {
                SmtpServer.Send(message);
            }
            catch (Exception ex)
            {
                TempData["confirmError"] = $"Exception caught in SendEmailNotification(): {ex.Message}";
            }

            //SmtpServer.Send(message);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }




        public async Task<IActionResult> Logout()
        {
            await authenticationService.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}