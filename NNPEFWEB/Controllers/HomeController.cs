 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;

namespace NNPEFWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboard _dashboard;
        private readonly ICommandDashboard _commanddashboard;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IDashboard dashboard, ICommandDashboard commanddashboard, ApplicationDbContext context)
        {
            _logger = logger;
            _dashboard = dashboard;
            _context = context;
            _commanddashboard = commanddashboard;
        }

        [Authorize]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.Name != "hicad@hicad.com")
            {
                return RedirectToAction("sectiondashboard");
            }
            else
            {
                // var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                var dah = new personelCountVM()
                {
                    AllStaff = _dashboard.AllStaff(),
                    ApproveStaff = _dashboard.ApprovedStaff(),
                    AwaitingApprovalStaff = _dashboard.AwaiteApprovalStaff(),
                    YettoFillStaff = _dashboard.YetToFillStaff(),
                    CommandYetToFill = _dashboard.AllCommandStaff(),

                    AllStaffOfficers = _dashboard.AllStaffOfficers(),
                    ApproveStaffOfficers = _dashboard.ApprovedStaffOfficers(),
                    AwaitingApprovalStaffOfficers = _dashboard.AwaiteApprovalStaffOfficers(),
                    YettoFillStaffOfficers = _dashboard.YetToFillStaffOfficers(),
                    CommandYetToFillOfficers = _dashboard.AllCommandStaffOfficers(),

                    AllStaffRatings = _dashboard.AllStaffRatings(),
                    ApproveStaffRatings = _dashboard.ApprovedStaffRatings(),
                    AwaitingApprovalStaffRatings = _dashboard.AwaiteApprovalStaffRatings(),
                    YettoFillStaffRatings = _dashboard.YetToFillStaffRatings(),
                    CommandYetToFillRatings = _dashboard.AllCommandStaffRatings(),

                    AllStaffTrainings = _dashboard.AllStaffTrainings(),
                    ApproveStaffTrainings = _dashboard.ApprovedStaffTrainings(),
                    AwaitingApprovalStaffTrainings = _dashboard.AwaiteApprovalStaffTrainings(),
                    YettoFillStaffTrainings = _dashboard.YetToFillStaffTrainings(),
                    CommandYetToFillTrainings = _dashboard.AllCommandStaffTrainings(),


                };

                return View(dah);
            }
        }
        public IActionResult sectiondashboard()
        {

            string ship = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Appointment;
            var dah = new personelCountVM()
            {

                AllStaffOfficers = _commanddashboard.AllStaffOfficers(ship),
                ApproveStaffOfficers = _commanddashboard.ApprovedStaffOfficers(ship),
                AwaitingApprovalStaffOfficers = _commanddashboard.AwaiteApprovalStaffOfficers(ship),
                YettoFillStaffOfficers = _commanddashboard.YetToFillStaffOfficers(ship),
                CommandYetToFillOfficers = _commanddashboard.AllCommandStaffOfficers(ship),

                AllStaffRatings = _commanddashboard.AllStaffRatings(ship),
                ApproveStaffRatings = _commanddashboard.ApprovedStaffRatings(ship),
                AwaitingApprovalStaffRatings = _commanddashboard.AwaiteApprovalStaffRatings(ship),
                YettoFillStaffRatings = _commanddashboard.YetToFillStaffRatings(ship),
                CommandYetToFillRatings = _commanddashboard.AllCommandStaffRatings(ship),

                AllStaffTrainings = _commanddashboard.AllStaffTrainings(ship),
                ApproveStaffTrainings = _commanddashboard.ApprovedStaffTrainings(ship),
                AwaitingApprovalStaffTrainings = _commanddashboard.AwaiteApprovalStaffTrainings(ship),
                YettoFillStaffTrainings = _commanddashboard.YetToFillStaffTrainings(ship),
                CommandYetToFillTrainings = _commanddashboard.AllCommandStaffTrainings(ship),
            };

            return View(dah);
        }
        public IActionResult commanddashbord()
        {
            //var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            int? shipid = HttpContext.Session.GetInt32("ship");
            string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;
            var dah = new personelCountVM()
            {

                AllStaffOfficers = _commanddashboard.AllStaffOfficers(ship),
                ApproveStaffOfficers = _commanddashboard.ApprovedStaffOfficers(ship),
                AwaitingApprovalStaffOfficers = _commanddashboard.AwaiteApprovalStaffOfficers(ship),
                YettoFillStaffOfficers = _commanddashboard.YetToFillStaffOfficers(ship),
                CommandYetToFillOfficers = _commanddashboard.AllCommandStaffOfficers(ship),

                AllStaffRatings = _commanddashboard.AllStaffRatings(ship),
                ApproveStaffRatings = _commanddashboard.ApprovedStaffRatings(ship),
                AwaitingApprovalStaffRatings = _commanddashboard.AwaiteApprovalStaffRatings(ship),
                YettoFillStaffRatings = _commanddashboard.YetToFillStaffRatings(ship),
                CommandYetToFillRatings = _commanddashboard.AllCommandStaffRatings(ship),

                AllStaffTrainings = _commanddashboard.AllStaffTrainings(ship),
                ApproveStaffTrainings = _commanddashboard.ApprovedStaffTrainings(ship),
                AwaitingApprovalStaffTrainings = _commanddashboard.AwaiteApprovalStaffTrainings(ship),
                YettoFillStaffTrainings = _commanddashboard.YetToFillStaffTrainings(ship),
                CommandYetToFillTrainings = _commanddashboard.AllCommandStaffTrainings(ship),
            };

            return View(dah);
        }
         public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Command()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
