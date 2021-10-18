using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
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
        public IActionResult command()
        {
            var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            string command = _context.ef_commands.Where(x => x.Id == cmdr.Command).FirstOrDefault().code;
            string ship = _context.ef_ships.Where(x => x.Id == cmdr.ship).FirstOrDefault().shipName;
            var dah = new personelCountVM()
            {

                AllStaffOfficers = _commanddashboard.AllStaffOfficers(ship, command),
                ApproveStaffOfficers = _commanddashboard.ApprovedStaffOfficers(ship, command),
                AwaitingApprovalStaffOfficers = _commanddashboard.AwaiteApprovalStaffOfficers(ship, command),
                YettoFillStaffOfficers = _commanddashboard.YetToFillStaffOfficers(ship, command),
                CommandYetToFillOfficers = _commanddashboard.AllCommandStaffOfficers(ship, command),

                AllStaffRatings = _commanddashboard.AllStaffRatings(ship, command),
                ApproveStaffRatings = _commanddashboard.ApprovedStaffRatings(ship, command),
                AwaitingApprovalStaffRatings = _commanddashboard.AwaiteApprovalStaffRatings(ship, command),
                YettoFillStaffRatings = _commanddashboard.YetToFillStaffRatings(ship, command),
                CommandYetToFillRatings = _commanddashboard.AllCommandStaffRatings(ship, command),

                AllStaffTrainings = _commanddashboard.AllStaffTrainings(ship, command),
                ApproveStaffTrainings = _commanddashboard.ApprovedStaffTrainings(ship, command),
                AwaitingApprovalStaffTrainings = _commanddashboard.AwaiteApprovalStaffTrainings(ship, command),
                YettoFillStaffTrainings = _commanddashboard.YetToFillStaffTrainings(ship, command),
                CommandYetToFillTrainings = _commanddashboard.AllCommandStaffTrainings(ship, command),
            };

            return View(dah);
        }
            public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult Privacy()
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
