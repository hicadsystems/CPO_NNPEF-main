using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using OfficeOpenXml;

namespace NNPEFWEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISystemsInfoService _systemsInfoService;
        private readonly string connectionstring;
        private IPersonInfoService _personinfoService;
        private readonly IPersonService _personService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IConfiguration config;
        private readonly IShipService _shipService;
        private int resetcode;
        Random rnd = new Random();

        public AccountController(
            IShipService shipService,
            IConfiguration config,
               IPersonInfoService personinfoService,
            IPersonService personService,
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISystemsInfoService systemsInfoService,
            ApplicationDbContext context, IUnitOfWorks unitOfWorks, IConfiguration configuration)
        {
            _shipService = shipService;
            this.config = config;
            _personinfoService = personinfoService;
            _personService = personService;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _systemsInfoService = systemsInfoService;
            _context = context;
            _unitOfWorks = unitOfWorks;
            connectionstring = configuration.GetConnectionString("DefaultConnection");


        }

        [HttpGet]
        public ActionResult GetSystemsInfo()
        {
            var systeminfo = _context.ef_systeminfos.ToList();

            return View(systeminfo);
        }

        [HttpGet]
        public ActionResult GetSystemsInfoById(int id)
        {
            var systeminfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systeminfo);
        }

        [HttpGet]
        public ActionResult CreateSystemsInfo()
        {
            systeminfoVM systeminfoVM = new systeminfoVM();

            return View(systeminfoVM);
        }

        [HttpPost]
        public ActionResult CreateSystemsInfo(systeminfoVM ef_Systeminfo)
        {
            try
            {
                if (ef_Systeminfo.company_image != null)
                {
                    IFormFile file = Request.Form.Files.Where(x => x.Name == "company_image").FirstOrDefault();

                    using (var dataStream = new MemoryStream())
                    {
                        file.CopyToAsync(dataStream);

                        ef_Systeminfo.company_image = dataStream.ToArray();

                    }
                }

                _systemsInfoService.CreateSystemsInfo(ef_Systeminfo);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            return RedirectToAction("GetSystemsInfo");

        }

        [HttpGet]
        public ActionResult UpdateSystemsInfo(int id)
        {
            var systemsInfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systemsInfo);
        }

        [HttpPost]
        public ActionResult UpdateSystemsInfo(ef_systeminfo ef_Systeminfo)
        {
            if (ef_Systeminfo.company_image != null)
            {
                IFormFile file = Request.Form.Files.Where(x => x.Name == "company_image").FirstOrDefault();

                using (var dataStream = new MemoryStream())
                {
                    file.CopyToAsync(dataStream);

                    ef_Systeminfo.company_image = dataStream.ToArray();

                }
            }

            _systemsInfoService.UpdateSystemsInfo(ef_Systeminfo);

            return RedirectToAction("GetSystemsInfo");
        }

        [HttpGet]
        public ActionResult DeleteSystemsInfo(int id)
        {
            var systemsInfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systemsInfo);
        }

        [HttpPost]
        public ActionResult DeleteSystemsInfo(int id, ef_systeminfo ef_Systeminfo)
        {
            var systemsInfoInDB = _systemsInfoService.GetSystemsInfo(id);

            if (systemsInfoInDB == null)
            {
                return View("Error");
            }

            _systemsInfoService.DeleteSystemsInfo(systemsInfoInDB.Id);

            return RedirectToAction("GetSystemsInfo");
        }

        public ActionResult SiteActivities()
        {
           var dd= _context.ef_systeminfos.FirstOrDefault();
            HttpContext.Session.SetInt32("Site", dd.SiteStatus);
            return View();
        }
        [HttpPost]
        public ActionResult SiteActivities(ef_systeminfo  status )
        {
            string user = User.Identity.Name;
            using(SqlConnection sqlcon=new SqlConnection())
            {
                using(SqlCommand cmd=new SqlCommand("OpenCloseForm", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@opendate", status.opendate));
                    cmd.Parameters.Add(new SqlParameter("@closedate", status.closedate));
                    cmd.Parameters.Add(new SqlParameter("@status", status.SiteStatus));
                    cmd.Parameters.Add(new SqlParameter("@user", user));
              
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                }
               
            }
            return View();
        }
        //public ActionResult CommandLogin()
        //{
        //    var command = new SelectList(_context.ef_commands.ToList(), "Id", "commandName");
        //    var ship = new SelectList(_context.ef_ships.ToList(), "Id", "shipName");


        //    var com = new CommandLoginVM
        //    {
        //        commandList = command,
        //        shiplist = ship,
        //    };
        //    return View(com);
        //}
        //[HttpPost]
        //public ActionResult CommandLogin(CommandLoginVM com)
        //{
        //    try
        //    {
        //        var user = _userManager.FindByNameAsync(com.UserName).Result;
        //        if (user != null)
        //        {
        //            if (user.ship.Value != com.ship)
        //            {
        //                TempData["commandLoginMessage"] = "User No Found";
        //            }
        //            else
        //            {

        //                if (user.EmailConfirmed == false)
        //                {
        //                    HttpContext.Session.SetString("SVC_No", user.UserName);

        //                    return RedirectToPage("/Account/ForgotPassword", new { area = "Identity" });
        //                }
        //                com.UserName = user.UserName;

        //                var result = _signInManager.PasswordSignInAsync(com.UserName, com.Password, true, lockoutOnFailure: false).Result;
        //                if (result.Succeeded)
        //                {
        //                    if (user.Appointment == "Admin" || user.Appointment == "Operator")
        //                    {
        //                        HttpContext.Session.SetInt32("LoginCommand", com.Command);
        //                        HttpContext.Session.SetString("LoginClass", com.Class);
        //                        HttpContext.Session.SetInt32("ship", com.ship);

        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                    HttpContext.Session.SetString("CommandUser", com.UserName);
        //                    HttpContext.Session.SetInt32("LoginCommand", com.Command);
        //                    HttpContext.Session.SetString("Appointment", user.Appointment);
        //                    HttpContext.Session.SetInt32("ship", com.ship);

        //                    return RedirectToAction("commanddashbord", "Home");
        //                    //return RedirectToAction("PersonelList", new RouteValueDictionary(
        //                    //      new
        //                    //      {
        //                    //          controller = "PersonalInfo",
        //                    //          action = "PersonelList",
        //                    //          id = com.Class,
        //                    //      }));
        //                }
        //                else
        //                {
        //                    TempData["commandLoginMessage"] = "Invalid UserName Or Password";
        //                }
        //            }

        //        }
        //        else
        //        {
        //            TempData["commandLoginMessage"] = "User No Found";
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogInformation(ex.Message);
        //    }

        //    return RedirectToAction("CommandLogin", "Account");
        //}
        public ActionResult CommandLogin()
        {
            var command = new SelectList(_context.ef_commands.ToList(), "Id", "commandName");
            var ship = new SelectList(_context.ef_ships.ToList(), "Id", "shipName");


            var com = new CommandLoginVM
            {
                commandList = command,
                shiplist = ship,
            };
            return View(com);
        }
        [HttpPost]
        public ActionResult CommandLogin(CommandLoginVM com)
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

                    var pers = _personService.GetPersonBySvc_NO(com.UserName);
                    var per = _personinfoService.GetPersonalInfo(com.UserName).Result;
                    if (pers.svcNo.Substring(0,2) != "NN")
                    {
                        TempData["commandLoginMessage"] = "Not Eligible Contact Admin";
                        return RedirectToAction("CommandLogin", "Account");
                    }
                    var checkdoublemail = _context.ef_PersonnelLogins.Where(x => x.email == pers.email).Count();
                    if (pers == null)
                    {
                        TempData["commandLoginMessage"] = "Invalid credentials";
                        return RedirectToAction("CommandLogin", "Account");
                    }
                    else if (pers.expireDate == null && pers.userName == com.UserName && pers.password == com.Password)
                    {
                        if (pers.email == null)
                        {
                            TempData["commandLoginMessage"] = "Email Not Avialable Contact CPO";
                        }
                        else if (IsValidEmail(pers.email) != true)
                        {
                            TempData["commandLoginMessage"] = "Invalid Email Contact CPO";
                        }
                        else if (checkdoublemail > 1)
                        {
                            TempData["commandLoginMessage"] = "Duplicate Email Contact CPO";
                        }
                        else
                        {
                            HttpContext.Session.SetString("SVC_No", com.UserName);
                            return RedirectToAction("ResetPasswordServiceNumber", new RouteValueDictionary(
                               new
                               {
                                   controller = "Account",
                                   action = "ResetPasswordServiceNumber",
                                   email = pers.userName
                               }));
                        }

                    }
                    else if (VerifyMd5Hash(md, com.Password, pers.password))
                    {
                        //if (pers.Appointment == null || pers.Appointment == "")
                        //{
                        //    TempData["commandLoginMessage"] = "You Are Not Eligable To Login, Contact Admin";
                        //    return RedirectToAction("Login", "Account");
                        //}
                        if (pers.payClass == "1")
                        {
                            HttpContext.Session.SetString("CommandUser", com.UserName);
                            HttpContext.Session.SetString("Appointment", per.appointment);
                            HttpContext.Session.SetString("Authorization", "CommandLevel");
                            HttpContext.Session.SetInt32("ship", com.ship);

                            return RedirectToAction("commanddashbord", "Home");
                        }
                        else
                        {
                            TempData["commandLoginMessage"] = "You Are Not Eligable To Login";
                            return RedirectToAction("CommandLogin", "Account");
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }
            return View();
       
        }
        public ActionResult ShipLogin()
        {
            var command = new SelectList(_context.ef_commands.ToList(), "Id", "commandName");
            var ship = new SelectList(_context.ef_ships.ToList(), "Id", "shipName");


            var com = new CommandLoginVM
            {
                commandList = command,
                shiplist = ship,
            };
            return View(com);
        }
        [HttpPost]
        public ActionResult ShipLogin(CommandLoginVM com)
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

                    var pers = _shipService.GetPersonBySvcno(com.UserName);
                    var per = _personinfoService.GetPersonalInfo(com.UserName).Result;
                    var shipid = _context.ef_ships.FirstOrDefault(x => x.shipName == pers.ship);
                    if (pers.userName.Substring(0, 2) != "NN")
                    {
                        TempData["commandLoginMessage"] = "Not Eligible Contact Admin";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    if (shipid.Id != com.ship)
                    {
                        TempData["commandLoginMessage"] = "Make sure you select the right ship";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    //var checkdoublemail = _context.ef_shiplogins.Where(x => x.email == pers.email).Count();
                    if (pers == null)
                    {
                        TempData["commandLoginMessage"] = "Invalid credentials";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    else if (pers.expireDate == null && pers.userName == com.UserName && pers.password == com.Password)
                    {
                        
                       //if (IsValidEmail(pers.email) != true)
                       // {
                       //     TempData["commandLoginMessage"] = "Invalid Email Contact CPO";
                       // }
                       // else if (checkdoublemail > 1)
                       // {
                       //     TempData["commandLoginMessage"] = "Duplicate Email Contact CPO";
                       // }
                       // else
                       // {
                            HttpContext.Session.SetString("SVC_No", com.UserName);
                            HttpContext.Session.SetString("shipuserToReset", com.UserName);
                            return RedirectToAction("ResetShipPassword", "Account");
                        //}

                    }
                    else if (VerifyMd5Hash(md, com.Password, pers.password))
                    {
                        
                            HttpContext.Session.SetString("ShipUser", com.UserName);
                            HttpContext.Session.SetString("Authorization", "CommandLevel");
                            HttpContext.Session.SetInt32("ship", com.ship);

                            return RedirectToAction("commanddashbord", "Home");
                      }
                        else
                        {
                            TempData["commandLoginMessage"] = "You Are Not Eligible To Login";
                            return RedirectToAction("ShipLogin", "Account");
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
        public IActionResult ResetPasswordServiceNumber(string email)
        {

            try
            {
                if (email != null)
                {
                    var user = _personService.GetPersonBySvc_NO(email);
                    if (user != null)
                    {
                        resetcode = rnd.Next(100000, 200000);   // generate random number and send to user mail and phone
                        string message = "Your Verification Code is:" + resetcode;
                        HttpContext.Session.SetString("userToReset", user.userName);
                        HttpContext.Session.SetString("verificationCode", resetcode.ToString());

                        // a call to send email notification
                        string emailFrom = "NN-CPO";
                        string emailSender = config.GetValue<string>("mailconfig:SenderEmail");
                        // add navy email adds
                        if (!string.IsNullOrEmpty(user.email))
                            SendEmailNotification(emailFrom, emailSender, message, user.email);

                        TempData["verifymessage"] = "An Email Notification Was sent to your Email address.";
                        return RedirectToAction("VerifyCode");

                    }
                    else
                    {
                        TempData["verifymessage"] = "Invalid Service Number. Please Contact your Admin";
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
                        var user = _personService.GetPersonBySvc_NO(userToReset);
                        if (user != null)
                        {
                            string PasswordToHash = GetMd5Hash(md5Hash, model.ConfirmPassword);
                            user.password = PasswordToHash;
                            user.expireDate = DateTime.Now.AddMonths(3);
                            _personService.updatepersonlogin2(user);

                            HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");

                            HttpContext.Session.Clear();

                            return RedirectToAction("CommandLogin");
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
        public ActionResult ResetShipPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetShipPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MD5 md5Hash = MD5.Create();
                    var userToReset = HttpContext.Session.GetString("shipuserToReset");   // retrieve the user from session
                    if (!string.IsNullOrEmpty(userToReset))
                    {
                        var user = _shipService.GetPersonBySvcno(userToReset);
                        if (user != null)
                        {
                            string PasswordToHash = GetMd5Hash(md5Hash, model.ConfirmPassword);
                            user.password = PasswordToHash;
                            user.expireDate = DateTime.Now.AddMonths(3);
                            _shipService.updateshiploginss(user);

                            HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");

                            HttpContext.Session.Clear();

                            return RedirectToAction("shipLogin");
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


        public List<SelectListItem> GetCommand()
        {
            var commandList = (from rk in _context.ef_commands
                               select new SelectListItem()
                               {
                                   Text = rk.commandName,
                                   Value = rk.Id.ToString(),
                               }).ToList();

            commandList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return commandList;
        }
        public List<ef_ship> GetShip2(int commandId)
        {
            var lgaList = (from rk in _context.ef_ships
                           where (rk.commandid == commandId)
                           select new ef_ship()
                           {
                               shipName = rk.shipName,
                               Id = rk.Id,
                           }).ToList();


            return lgaList;
        }
        public IActionResult getships(int id)
        {
            var shipss = new List<ef_ship>();
            shipss = getshipsFromDatabaseByCommandId(id); //call repository
            return Json(shipss);
        }
        public List<ef_ship> getshipsFromDatabaseByCommandId(int id)
        {
            return _context.ef_ships.Where(c => c.commandid == id).ToList();
        }

        public List<SelectListItem> GetShip()
        {
            var lgaList = (from rk in _context.ef_ships
                           select new SelectListItem()
                           {
                               Text = rk.shipName,
                               Value = rk.Id.ToString(),
                           }).ToList();


            return lgaList;
        }
        public ActionResult Register()
        {
            var reg = new RegisterVM
            {
                CommandList = GetCommand(),
                RankList = GetRank()
            };
            return View(reg);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM Input)
        {
            GetCommand();
            GetRank();
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Command = Input.Command,
                    Rank = Input.Rank,
                    Appointment = Input.Appointment

                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                   
                    if (Input.Appointment == "CDR")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.CDR.ToString());
                    if (Input.Appointment == "HOD")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.HOD.ToString());
                    if (Input.Appointment == "DO")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.DO.ToString());
                    if (Input.Appointment == "Admin")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.Admin.ToString());
                    if (Input.Appointment == "SEC")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.Operator.ToString());
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "UserRole");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["regError"] = error.Description;
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
        public List<SelectListItem> GetRank()
        {
           var RankList = (from rk in _context.ef_ranks
                           select new SelectListItem()
                           {
                               Text = rk.rankName,
                               Value = rk.rankName.ToString(),
                           }).ToList();

            RankList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            return RankList;
        }
        public List<ef_command> GetCommand2()
        {
           var CommandList = (from rk in _context.ef_commands
                           select new ef_command()
                           {
                               commandName = rk.commandName,
                               code = rk.code,
                           }).ToList();
            return CommandList;

        }

       



        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        public IActionResult UpdatePayrollWithEF()
        {
            ViewBag.commandList = GetCommand();
            return View();
        }
        [HttpPost]
        public IActionResult UpdatePayrollWithEF(string command,string payclass)
        {
            ViewBag.commandList = GetCommand();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePayrollEF", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@command", command));
                        cmd.Parameters.Add(new SqlParameter("@payclass", payclass));


                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                        TempData["UpdateMessage"] = "Sucessfully Updated";
                    }
                }
            }
            catch (Exception ex)
            {
               
                TempData["UpdateMessage"] = "Updated Fail";
             //   ex.Message;
            }

         
            return View();
        }
        public IActionResult ClosingPage()
        {
            return View();
        }
        public IActionResult UploadShipUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadShipUser(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";
                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";
                return View();
            }
            var listapplication = new List<personLoginVM>();
            var listapplicationofrecordnotavailable = new List<personLoginVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                   
                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[1, 1].Value == null)
                            worksheet.Cells[1, 1].Value = "";

                        if (worksheet.Cells[1, 2].Value == null)
                            worksheet.Cells[1, 2].Value = "";

                        if (worksheet.Cells[1, 3].Value == null)
                            worksheet.Cells[1, 3].Value = "";

                        if (worksheet.Cells[1, 4].Value == null)
                            worksheet.Cells[1, 4].Value = "";
                        if (worksheet.Cells[1, 5].Value == null)
                            worksheet.Cells[1, 5].Value = "";
                        if (worksheet.Cells[1, 6].Value == null)
                            worksheet.Cells[1, 6].Value = "";



                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";

                        if (worksheet.Cells[row, 4].Value == null)
                            worksheet.Cells[row, 4].Value = "";

                        if (worksheet.Cells[row, 5].Value == null)
                            worksheet.Cells[row, 5].Value = "";
                        if (worksheet.Cells[row, 6].Value == null)
                            worksheet.Cells[row, 6].Value = "";

                        //string command = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        //string ship = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();

                        string svcno = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string rank = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string name = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        string command = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                        string ship = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                        string password = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();


                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()))

                        {
                            listapplicationofrecordnotavailable.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = name,
                                department =command,
                                ship=ship,
                                password=password
                            });

                        }
                        else
                        {
                            //check if already in the list -- a possibility
                            listapplication.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = name,
                                department = command,
                                ship = ship,
                                password=password
                            });
                        }

                    }
                    //foreach (var s in listapplication)
                    //{

                    //    using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                    //    {
                    //        using (SqlCommand cmd = new SqlCommand("UploadShipUsers2", sqlcon))
                    //        {
                    //            cmd.CommandTimeout = 1200;
                    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //            cmd.Parameters.Add(new SqlParameter("@rank", s.rank));
                    //            cmd.Parameters.Add(new SqlParameter("@userName", s.svcNo));
                    //            cmd.Parameters.Add(new SqlParameter("@password", s.password));
                    //            cmd.Parameters.Add(new SqlParameter("@surName", s.surName));
                    //            cmd.Parameters.Add(new SqlParameter("@phoneNumber", s.phoneNumber));
                    //            cmd.Parameters.Add(new SqlParameter("@department", s.department));
                    //            cmd.Parameters.Add(new SqlParameter("@ship", s.ship));


                    //            sqlcon.Open();
                    //            cmd.ExecuteNonQuery();
                    //        }

                    //    }
                    //}
                    string userp = User.Identity.Name;

                    ProcesUpload procesUpload2 = new ProcesUpload(null, connectionstring, listapplication, _unitOfWorks, userp, null, null);
                    await procesUpload2.processUploadShipUser();
                    TempData["message"] = "Uploaded Successfully";

                }

            }
            if (listapplicationofrecordnotavailable.Count > 0)
            {

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return View();
        }
        public IActionResult UploadUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadUser(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";
                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";
                return View();
            }
            var listapplication = new List<personLoginVM>();
            var listapplicationofrecordnotavailable = new List<personLoginVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string SVC_NO = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string RANK = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string SURNAME = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string OTHERNAMES = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string APPOINTMENT = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                    string PHONE = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                    string EMAIL = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                    string DEPARTMENT = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();
                    string SHIP = String.IsNullOrEmpty(worksheet.Cells[1, 9].ToString()) ? "" : worksheet.Cells[1, 9].Value.ToString().Trim();

                    if (SVC_NO != "SVC_NO" || RANK != "RANK" || SURNAME != "SURNAME" || APPOINTMENT != "APPOINTMENT" || EMAIL != "EMAIL"
                        || PHONE != "PHONE")
                    {
                        return BadRequest("File not in the Right format");
                    }
                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[1, 1].Value == null)
                            worksheet.Cells[1, 1].Value = "";

                        if (worksheet.Cells[1, 2].Value == null)
                            worksheet.Cells[1, 2].Value = "";

                        if (worksheet.Cells[1, 3].Value == null)
                            worksheet.Cells[1, 3].Value = "";

                        if (worksheet.Cells[1, 4].Value == null)
                            worksheet.Cells[1, 4].Value = "";
                        if (worksheet.Cells[1, 5].Value == null)
                            worksheet.Cells[1, 5].Value = "";
                        if (worksheet.Cells[1, 6].Value == null)
                            worksheet.Cells[1, 6].Value = "";
                        if (worksheet.Cells[1, 7].Value == null)
                            worksheet.Cells[1, 7].Value = "";
                        if (worksheet.Cells[1, 8].Value == null)
                            worksheet.Cells[1, 8].Value = "";
                        if (worksheet.Cells[1, 9].Value == null)
                            worksheet.Cells[1, 9].Value = "";


                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";

                        if (worksheet.Cells[row, 4].Value == null)
                            worksheet.Cells[row, 4].Value = "";

                        if (worksheet.Cells[row, 5].Value == null)
                            worksheet.Cells[row, 5].Value = "";

                        if (worksheet.Cells[row, 6].Value == null)
                            worksheet.Cells[row, 6].Value = "";
                        if (worksheet.Cells[row, 7].Value == null)
                            worksheet.Cells[row, 7].Value = "";
                        if (worksheet.Cells[row, 8].Value == null)
                            worksheet.Cells[row, 8].Value = "";
                        if (worksheet.Cells[row, 9].Value == null)
                            worksheet.Cells[row, 9].Value = "";



                        string svcno = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string rank = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string surname = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        string othername = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                        string appointment = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                        string phone = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();
                        string email = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? "" : worksheet.Cells[row, 7].Value.ToString().Trim();
                        string department = String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim();
                        string ship = String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ? "" : worksheet.Cells[row, 9].Value.ToString().Trim();


                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()))

                        {
                            listapplicationofrecordnotavailable.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = surname,
                                otheName = othername,
                                appointment = appointment,
                                phoneNumber = phone,
                                email = email,
                                department = department,
                                ship = ship
                            });

                        }
                        else
                        {
                            //check if already in the list -- a possibility
                            listapplication.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = surname,
                                otheName = othername,
                                appointment = appointment,
                                phoneNumber = phone,
                                email = email,
                                department = department,
                                ship = ship
                            });
                        }

                    }
                    string userp = User.Identity.Name;

                    ProcesUpload procesUpload2 = new ProcesUpload(null,connectionstring, listapplication, _unitOfWorks, userp, null, null);
                    await procesUpload2.processUploadInThread2();
                    TempData["message"] = "Uploaded Successfully";

                }

            }
            if (listapplicationofrecordnotavailable.Count > 0)
            {

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return View();
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
