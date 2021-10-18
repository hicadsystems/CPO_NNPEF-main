using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;

namespace NNPEFWEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISystemsInfoService _systemsInfoService;
        private readonly string connectionstring;
        //private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        //private readonly IUnitOfWorks _unitOfWorks;
        private readonly IUnitOfWorks _unitOfWorks;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISystemsInfoService systemsInfoService,
            ApplicationDbContext context, IUnitOfWorks unitOfWorks, IConfiguration configuration)
        {
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
        // GET: AccountController
        public ActionResult CommandLogin()
        {
            var command = new SelectList(_context.ef_commands.ToList(), "Id", "commandName");
            var ship = new SelectList(_context.ef_ships.ToList(), "Id", "shipName");


            var com = new CommandLoginVM
            {
                commandList = command,
                shiplist=ship,
            };
            return View(com);
        }
        [HttpPost]
        public ActionResult CommandLogin(CommandLoginVM com)
        {
            //GetCommand();
            var user =  _userManager.FindByNameAsync(com.UserName).Result;
            if (user != null)
            {
                if (user.Command != com.Command && user.ship.Value != com.ship)
                {
                    TempData["commandLoginMessage"] = "User No Found";
                }
                 else { 
                
                    if (user.EmailConfirmed == false)
                    {
                        HttpContext.Session.SetString("SVC_No", user.UserName);
                        
                        return RedirectToPage("/Account/ForgotPassword", new { area = "Identity" });
                    }
                    com.UserName = user.UserName;

                    var result = _signInManager.PasswordSignInAsync(com.UserName, com.Password, true, lockoutOnFailure: false).Result;
                    if (result.Succeeded)
                    {
                        if (user.Appointment == "Admin" || user.Appointment == "Operator")
                        {
                            HttpContext.Session.SetInt32("LoginCommand", com.Command);
                            HttpContext.Session.SetString("LoginClass", com.Class);
                            HttpContext.Session.SetInt32("ship", com.ship);

                            return RedirectToAction("Index", "Home");
                        }
                        HttpContext.Session.SetString("CommandUser", com.UserName);
                        HttpContext.Session.SetInt32("LoginCommand", com.Command);
                        HttpContext.Session.SetString("Appointment", user.Appointment);
                        HttpContext.Session.SetInt32("ship", com.ship);

                         return RedirectToAction("command", "Home");
                        //return RedirectToAction("PersonelList", new RouteValueDictionary(
                        //      new
                        //      {
                        //          controller = "PersonalInfo",
                        //          action = "PersonelList",
                        //          id = com.Class,
                        //      }));
                    }
                    else
                    {
                        TempData["commandLoginMessage"] = "Invalid UserName Or Password";
                    }
                }
                
            }
            else
            {
                TempData["commandLoginMessage"] = "User No Found";
            }

            return RedirectToAction("CommandLogin", "Account");
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

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: AccountController/Create
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(IFormCollection collection)
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

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
