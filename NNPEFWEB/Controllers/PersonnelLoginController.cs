using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using NETCore.MailKit.Core;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;

namespace NNPEFWEB.Controllers
{
    public class PersonnelLoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPersonService personService;
        private readonly IPersonInfoService personInfoService;
        private readonly IEmailService _EmailService;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IConfiguration config;
        private int resetcode;
        Random rnd = new Random();
        private readonly ILogger<HomeController> _logger;
        public PersonnelLoginController
         (
            ILogger<HomeController> logger,
            ApplicationDbContext context,IPersonInfoService personInfoService,
            IPersonService personService, IUnitOfWorks unitOfWorks, IConfiguration config
         )
         {
            _logger = logger;
            this.personService = personService;
            this.unitOfWorks = unitOfWorks;
            this.config = config;
            this.personInfoService = personInfoService;
            _context = context;
         }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(personLoginVM value)
        {
            try
            {
                MD5 md = MD5.Create();
                if (ModelState.IsValid)
                {
                    var site = _context.ef_systeminfos.FirstOrDefault(x => x.closedate.Date < DateTime.Now.Date);
                    if (site != null)
                    {
                        return RedirectToAction("ClosingPage", "Account");
                    }

                   // var pers = personService.GetPersonBySvc_NO(value.userName);
                    var pers = _context.ef_PersonnelLogins.Where(x => x.userName == value.userName).SingleOrDefault();
                    var checkdoublemail = _context.ef_PersonnelLogins.Where(x => x.email == pers.email).Count();
                    if (pers == null)
                    {
                        TempData["errorMessage"] = "Invalid credentials";
                        return RedirectToAction("Login", "PersonnelLogin");
                    }
                    else if (pers.expireDate == null && pers.userName == value.userName && pers.password == value.password)
                    {
                        //if (pers.email == null)
                        //{
                        //    TempData["errorMessage"] = "Email Not Avialable Contact CPO";
                        //}
                        //else if (IsValidEmail(pers.email) != true)
                        //{
                        //    TempData["errorMessage"] = "Invalid Email Contact CPO";
                        //}
                        //else if (checkdoublemail > 1)
                        //{
                        //    TempData["errorMessage"] = "Duplicate Email Contact CPO";
                        //}
                        //else
                        //{
                        //HttpContext.Session.SetString("SVC_No", value.userName);
                        //return RedirectToAction("ResetPasswordServiceNumber", new RouteValueDictionary(
                        //   new
                        //   {
                        //       controller = "PersonnelLogin",
                        //       action = "ResetPasswordServiceNumber",
                        //       email = pers.userName
                        //   }));
                        //}
                        HttpContext.Session.SetString("SVC_No", value.userName);
                        return RedirectToAction("ResetPasswordServiceNumber");
                        //MD5 md5Hash = MD5.Create();
                        //string PasswordToHash = GetMd5Hash(md5Hash, value.password);
                        //_db2.updatepassword(PasswordToHash, userToReset);
                    }
                    else if (VerifyMd5Hash(md, value.password, pers.password))
                    {
                        //if (pp.Status != null)
                        //{

                        //    TempData["errorMessage"]= "Form Is Already Submited";
                        //    return View();
                        //}
                        if (pers.payClass == "1")
                        {
                            HttpContext.Session.SetString("personnel", pers.userName);
                            return RedirectToAction("OfficerRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "OfficerRecord",
                                id = pers.password
                            }));
                        }
                        else if (pers.payClass == "2")
                        {
                            HttpContext.Session.SetString("personnel", pers.userName);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord",
                                id = pers.password
                            }));
                        }
                        else if (pers.payClass == "3")
                        {
                            HttpContext.Session.SetString("personnel", pers.userName);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord",
                                id = pers.password
                            }));
                        }
                        else if (pers.payClass == "4")
                        {
                            HttpContext.Session.SetString("personnel", pers.userName);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord",
                                id = pers.password
                            }));
                        }
                        else if (pers.payClass == "5")
                        {
                            HttpContext.Session.SetString("personnel", pers.userName);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord",
                                id = pers.password
                            }));
                        }
                        else if (pers.payClass == "6")
                        {
                            HttpContext.Session.SetString("personnel", pers.userName);
                            return RedirectToAction("TrainingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "TrainingRecord",
                                id = pers.password
                            }));
                        }

                    }
                    else
                    {
                       
                        TempData["errorMessage"] = "Invalid UserName/Password";
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }
            return View();
        }
        public bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
        [HttpPost]
        public IActionResult SendApplicantMessage(string Email)
        {
            try
            {
                Guid k = Guid.NewGuid();
              
                string confirmationUrl = Url.Action("ConfirmEmail", "Authentication", new { email = Email, token = k.ToString() },
                    protocol: HttpContext.Request.Scheme);
                string confirm = "Please confirm your account by clicking this link: <a href=\""
                                         + confirmationUrl + "\">link</a>";
                _EmailService.Send(Email, "Applicant confirmation message", confirm, true);

                return Ok(new { responseCode = 200, responseDescription = "successful" });
            }
            catch
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }


        [NonAction]
        public void SendMessage(string emailTo)
        {
            Guid k = Guid.NewGuid();
            
            string confirmationUrl = Url.Action("ConfirmEmail", "Authentication", new { email = emailTo, token = k.ToString() },
                protocol: HttpContext.Request.Scheme);
            string confirm = "Please confirm your account by clicking this link: <a href=\""
                                     + confirmationUrl + "\">link</a>";
            _EmailService.Send(emailTo, "Applicant confirmation message", confirm, true);
        }

        public ActionResult VerifyCode()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode([Required(ErrorMessage = "This Field is Required")] string Code)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string SessionCode = HttpContext.Session.GetString("verificationCode").ToString();
                    if (SessionCode != null)
                    {
                        if (SessionCode != Code)
                        {
                            ModelState.AddModelError("", "Invalid Verification Code");
                        }
                        else
                        {
                            return RedirectToAction("ResetPassword");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string log = ex.Message;
                }

            }
            return View();
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
                    var userToReset = HttpContext.Session.GetString("userToReset");   // retrieve the user from session
                    if (!string.IsNullOrEmpty(userToReset))
                    {
                        var user = personService.GetPersonBySvc_NO(userToReset);
                                                                                                      // validate if user exist
                        if (user != null)
                        {
                            string PasswordToHash = GetMd5Hash(md5Hash, model.ConfirmPassword);
                            user.password = PasswordToHash;
                            user.expireDate = DateTime.Now.AddMonths(3);
                            personService.updatepersonlogin2(user);

                            // update the user password
                            HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");

                            // retrieve service number from local storge, and verify the user before updating the user password

                            // after successfully updating password, remove session data.
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
        [AllowAnonymous]
        public ActionResult ResetPasswordServiceNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult ResetPasswordServiceNumber(ComfirmEmail conmail)
        {

            try
            {
                if (conmail.Email != conmail.ConfirmEmail)
                {
                    TempData["verifymessage"] = "Email Not Match";
                }
                if (conmail.ConfirmEmail != null)
                {
                    string username=HttpContext.Session.GetString("SVC_No");
                    var user = personService.GetPersonBySvc_NO(username);
                    if (user!=null)
                    {
                        resetcode = rnd.Next(100000, 200000);   // generate random number and send to user mail and phone
                        string message = "Your Verification Code is:" + resetcode;
                        HttpContext.Session.SetString("userToReset", user.userName);
                        HttpContext.Session.SetString("verificationCode", resetcode.ToString());
       
                       // a call to send email notification
                        string emailFrom = "NN-CPO";
                        string emailSender = config.GetValue<string>("mailconfig:SenderEmail");
                        // add navy email adds
                        if (!string.IsNullOrEmpty(conmail.ConfirmEmail))
                            SendEmailNotification(emailFrom, emailSender, message, conmail.ConfirmEmail);

                        TempData["verifymessage"]= "An Email Notification Was sent to your Email address.";
                        return RedirectToAction("VerifyCode");
                      
                    }
                    else
                    {
                        TempData["verifymessage"]= "Invalid Service Number. Please Contact your Admin";
                    }

                }
            }
            catch (Exception ex)
           {
                _logger.LogInformation(ex.Message);
                TempData["errorMessage"] = "Unable To Send Mail";
                return RedirectToAction("Login");
            }
            return View();
        }
        public void SendEmailNotification(string emailFrom, string sender, string messageToSend, string receipient)
        {
           
            var UserName = config.GetValue<string>("mailconfig:SenderEmail");
            var Password = config.GetValue<string>("mailconfig:Password");
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(receipient));
            message.From = new MailAddress(sender, emailFrom);
            message.Subject = "Password Verification Code";
            message.Body = string.Format(body, emailFrom, sender, messageToSend);
            message.IsBodyHtml = true;

   

            string host = config.GetValue<string>("mailconfig:Server");
            int port = config.GetValue<int>("mailconfig:Port");
            var enableSSL = config.GetValue<bool>("mailconfig:enableSSL");

            SmtpClient SmtpServer = new SmtpClient(host);

            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(UserName, Password)
            };
            SmtpServer.Port = port; // Also Add the port number to send it, its default for Gmail
            SmtpServer.Credentials = new System.Net.NetworkCredential(UserName, Password);
            SmtpServer.EnableSsl = enableSSL;
            SmtpServer.Timeout = 200000; // Add Timeout property
            SmtpServer.Send(message);

         
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
    }
}
