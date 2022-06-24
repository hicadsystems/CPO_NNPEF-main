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
        private readonly ISectionDashboard _sectiondashboard;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IDashboard dashboard, ICommandDashboard commanddashboard, ISectionDashboard sectiondashboard,ApplicationDbContext context)
        {
            _logger = logger;
            _dashboard = dashboard;
            _context = context;
            _commanddashboard = commanddashboard;
            _sectiondashboard = sectiondashboard;
        }

       // [Authorize]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.Name ==null)
            {
                return RedirectToAction("homepage");
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
            if (User.Identity.Name == null)
            {
                return RedirectToAction("homepage");
            }
            else
            {
             string ship = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Appointment;
            var dah = new personelCountVM()
            {

                AllStaffOfficers = _sectiondashboard.AllStaffOfficers(ship),
                ApproveStaffOfficers = _sectiondashboard.ApprovedStaffOfficers(ship),
                AwaitingApprovalStaffOfficers = _sectiondashboard.AwaiteApprovalStaffOfficers(ship),
                YettoFillStaffOfficers = _sectiondashboard.YetToFillStaffOfficers(ship),
                CommandYetToFillOfficers = _sectiondashboard.AllCommandStaffOfficers(ship),

                AllStaffRatings = _sectiondashboard.AllStaffRatings(ship),
                ApproveStaffRatings = _sectiondashboard.ApprovedStaffRatings(ship),
                AwaitingApprovalStaffRatings = _sectiondashboard.AwaiteApprovalStaffRatings(ship),
                YettoFillStaffRatings = _sectiondashboard.YetToFillStaffRatings(ship),
                CommandYetToFillRatings = _sectiondashboard.AllCommandStaffRatings(ship),

                AllStaffTrainings = _sectiondashboard.AllStaffTrainings(ship),
                ApproveStaffTrainings = _sectiondashboard.ApprovedStaffTrainings(ship),
                AwaitingApprovalStaffTrainings = _sectiondashboard.AwaiteApprovalStaffTrainings(ship),
                YettoFillStaffTrainings = _sectiondashboard.YetToFillStaffTrainings(ship),
                CommandYetToFillTrainings = _sectiondashboard.AllCommandStaffTrainings(ship),
            };

            return View(dah);

            }
            
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
        public async Task<IActionResult> AllStaffList(int? pageNumber)
        {
            var allStaffOfficersList =await _dashboard.AllStaffList("1","ALL",pageNumber);

            return View(allStaffOfficersList);
        }

        public async Task<IActionResult> ApprovedStaffList(int? pageNumber)
        {
            var approvedStaffList =await _dashboard.ApprovedStaffList("1", "CPO", pageNumber);

            return View(approvedStaffList);
        }

        public async Task<IActionResult> AwaiteApprovalStaffList(int? pageNumber)
        {
            var awaiteApprovalStaffList =await _dashboard.AwaiteApprovalStaffList("1", "SHIP", pageNumber);

            return View(awaiteApprovalStaffList);
        }

        public async Task<IActionResult> YetToFillStaffList(int? pageNumber)
        {
            var yetToFillStaffList =await _dashboard.YetToFillStaffList("1", null, pageNumber);

            return View(yetToFillStaffList);
        }

        public async Task<IActionResult> AllStaffRatingsList(int? pageNumber)
        {
            var allRatingslist =await _dashboard.AllStaffRatingsList("2", "ALL", pageNumber);

            return View(allRatingslist);
        }
       
        public async Task<IActionResult> ApprovedStaffRatingsList(int? pageNumber)
        {
            var approvedStaffList = await _dashboard.ApprovedStaffRatingsList("1", "CPO", pageNumber);

            return View(approvedStaffList);
        }

        public async Task<IActionResult> AwaiteApprovalStaffRatingsList(int? pageNumber)
        {
            var awaiteApprovalStaffList = await _dashboard.AwaiteApprovalStaffRatingsList("1", "SHIP", pageNumber);

            return View(awaiteApprovalStaffList);
        }

        public async Task<IActionResult> YetToFillStaffRatingsList(int? pageNumber)
        {
            var yetToFillStaffList = await _dashboard.YetToFillStaffRatingsList("1", null, pageNumber);

            return View(yetToFillStaffList);
        }
        public async Task<IActionResult> AllStaffTrainingsList(int? pageNumber)
        {
            var allRatingslist = await _dashboard.AllStaffTrainingsList("1", "ALL", pageNumber);

            return View(allRatingslist);
        }

        public async Task<IActionResult> ApprovedStaffTrainingsList(int? pageNumber)
        {
            var approvedStaffList = await _dashboard.ApprovedStaffTrainingsList("1", "CPO", pageNumber);

            return View(approvedStaffList);
        }

        public async Task<IActionResult> AwaiteApprovalStaffTrainingsList(int? pageNumber)
        {
            var awaiteApprovalStaffList =await _dashboard.AwaiteApprovalTrainingsList("1", "SHIP", pageNumber);

            return View(awaiteApprovalStaffList);
        }

        public async Task<IActionResult> YetToFillStaffTrainingsList(int? pageNumber)
        {
            var yetToFillStaffList =await _dashboard.YetToFillStaffTrainingsList("1", null, pageNumber);

            return View(yetToFillStaffList);
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
            return RedirectToAction("HomePage","Home");
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
