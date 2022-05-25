using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NNPEFWEB.Controllers
{
    public class ShipController : Controller
    {
        private readonly IPersonService personService;
        private readonly ISystemsInfoService _systemsInfo;
        private readonly IPersonInfoService personinfoService;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IGeneratePdf _generatepdf;
        private readonly string connectionString;
        private readonly ILogger<HomeController> _logger;
        public ShipController(ILogger<HomeController> logger,IGeneratePdf generatepdf, IUnitOfWorks unitOfWorks, IPersonInfoService personinfoService, IPersonService personService,
                                 ApplicationDbContext _context, IConfiguration configuration,
                                 ISystemsInfoService systemsInfo)
        {
            _logger = logger;
            this._context = _context;
            this.personService = personService;
            this._systemsInfo = systemsInfo;
            this.personinfoService = personinfoService;
            this.unitOfWorks = unitOfWorks;
            _generatepdf = generatepdf;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public ActionResult UpdatedPersonelList2(string id, string svcno)
        {
            try
            {
                HttpContext.Session.SetString("classid", id);
                int? shipid = HttpContext.Session.GetInt32("ship");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;
                
                if (svcno != null)
                {
                    var pp2 = personinfoService.GetUpdatedPersonnelBySVCNO(id, ship, svcno);
                    return View(pp2);
                }
                else
                {
                    var pp = personinfoService.GetUpdatedPersonnel(id, ship);
                    return View(pp);
                }

            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                throw;
            }

        }

        public ActionResult UpdatedPersonelList(string id, string svcno, int? pageNumber)
        {
            try
            {
                HttpContext.Session.SetString("classid", id);
                int? shipid = HttpContext.Session.GetInt32("ship");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;

                if (svcno != null)
                {
                   // var pp2 = personinfoService.GetUpdatedPersonnelBySVCNO2(cmdr.Appointment, svcno).ToList();
                    var pp2 = personinfoService.GetUpdatedPersonnelBySVCNO(id, ship, svcno).ToList();
                    PaginatedList<ef_personalInfo> personalInfos = new PaginatedList<ef_personalInfo>(pp2, 1, 1, 1);

                    return View(personalInfos);
                }

                else
                {

                    var pp = personinfoService.GetUpdatedPersonnelBySHip(id, ship, pageNumber).Result;
                    return View(pp);
                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public IActionResult Export()
        {
            try
            {
                string payclass = HttpContext.Session.GetString("classid");
                int? shipid = HttpContext.Session.GetInt32("ship");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;

                var pp = personinfoService.GetUpdatedPersonnel3(payclass, ship);
                List<RPTPersonModel> rpt = new List<RPTPersonModel>();
                foreach (var person in pp)
                {
                    if (person.Status == "SHIP")
                        person.Status = "Form Filled";
                    if (person.Status == "CPO")
                        person.Status = "Authorized";
                    if (person.Status == null)
                        person.Status = "Not Yet filled";

                    var pps = new RPTPersonModel
                    {
                        ServiceNumber = person.serviceNumber,
                        Rank = person.Rank,
                        Name = person.Surname + " " + person.OtherName,
                        Seniority=person.seniorityDate.Value.ToShortDateString().ToString(),
                        Ship = person.ship,
                        Status=person.Status
                        
                    };
                    rpt.Add(pps);
                }

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(rpt, true);
                    package.Save();
                }

                stream.Position = 0;
                string excelName = $"ShipReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                throw;
            }
        }
    
        public async Task<IActionResult> PrintList()
        {
            try
            {
                string payclass = HttpContext.Session.GetString("classid");
                int? shipid = HttpContext.Session.GetInt32("ship");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;
                if (payclass == "1")
                {
                    ViewBag.Category = "Officers";
                }
                else if (payclass == "2")
                {
                    ViewBag.Category = "Ratings";
                }
               else
                {
                    ViewBag.Category = "Trainee";
                }
                var pp = personinfoService.GetUpdatedPersonnel3(payclass, ship).ToList();
                 return await _generatepdf.GetPdf("Reports/ShipReport", pp);
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                return RedirectToAction("Home", "Homepage");
            }

        }
        public async Task<IActionResult> ShipReports(string reporttype, string payclass, int? pageNumber)
        {
            try
            {
                int? shipid = HttpContext.Session.GetInt32("ship");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;

                ViewData["reporttype"] = String.IsNullOrEmpty(reporttype) ? "AllStaff" : reporttype;

                var ppersons = await personinfoService.GetPersonnelShipReport(reporttype, payclass, ship, pageNumber);
               
                ViewData["payclass"] = payclass;

                return View(ppersons);

            }
            catch (Exception ex)
            {
                TempData["UpdateMessage"] = "Updated Fail";

                throw;
            }
        }
        public Task<IActionResult> ListOfAllStaffReport(string reporttype, string payclass)
        {
            int? shipid = HttpContext.Session.GetInt32("ship");
            string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;

            var ppersons = personinfoService.GetPersonnelShipReportrepo(reporttype, payclass,ship);

            //List<ef_personalInfo> rpt = new List<ef_personalInfo>();

            //foreach (var person in ppersons)
            //{
            //    var pps = new ef_personalInfo
            //    {
            //        Rank = person.Rank,
            //        serviceNumber = person.serviceNumber,
            //        Surname = person.Surname,
            //        OtherName = person.OtherName,
            //        ship = person.ship,
            //    };
            //    rpt.Add(pps);
            //}

            return _generatepdf.GetPdf("Reports/ShipReport", ppersons);
        }
        public IActionResult Export2(string reporttype, string payclass)
        {
            int? shipid = HttpContext.Session.GetInt32("ship");
            string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;

            var pp = personinfoService.GetPersonnelShipReportrepo(reporttype, payclass,ship);
            List<RPTPersonModel> rpt = new List<RPTPersonModel>();
            foreach (var person in pp)
            {
                var pps = new RPTPersonModel
                {
                    ServiceNumber = person.serviceNumber,
                    Rank = person.Rank,
                    Seniority = person.seniorityDate == null ? "00-00-0000" : person.seniorityDate.ToString(),
                    Name = person.Surname + " " + person.OtherName,
                    Status = person.Status,
                    Ship = person.ship,
                };
                rpt.Add(pps);
            }


            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                workSheet.Cells.LoadFromCollection(rpt, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"ShipReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public DateTime getdate(DateTime anydate)
        {
            DateTime newdate;
            if (anydate == null)
            {
                newdate = Convert.ToDateTime(2000 - 01 - 01);
            }
            else
            {
                newdate = Convert.ToDateTime(anydate);
            }
            return newdate;
        }

        public IActionResult UpdateOfficer(int id)
        {
            try
            {
        
                string username=HttpContext.Session.GetString("ShipUser");
                var pers = personService.GetUserByShip(username).Result;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    
                        using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@personid", id));
                            cmd.Parameters.Add(new SqlParameter("@doname", pers.surName+" "+pers.otheName));
                            cmd.Parameters.Add(new SqlParameter("@dosvcno", pers.userName));
                            cmd.Parameters.Add(new SqlParameter("@dorank", pers.rank));
                            cmd.Parameters.Add(new SqlParameter("@dodate", DateTime.Now));

                           
                            sqlcon.Open();
                            cmd.ExecuteNonQuery();
                        }
                  
                   
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                //return RedirectToAction("Login", "PersonnelLogin");
            }
            return RedirectToAction("UpdatedPersonelList", new RouteValueDictionary(
                  new
                  {
                      controller = "Ship",
                      action = "UpdatedPersonelList",
                      id = 1,
                  }));
        }

        public IActionResult UpdateOfficer2(int id)
        {
            try
            {
                string username = HttpContext.Session.GetString("ShipUser");
                var pers = personService.GetUserByShip(username).Result;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@personid", id));
                        cmd.Parameters.Add(new SqlParameter("@doname", pers.surName + " " + pers.otheName));
                        cmd.Parameters.Add(new SqlParameter("@dosvcno", pers.userName));
                        cmd.Parameters.Add(new SqlParameter("@dorank", pers.rank));
                        cmd.Parameters.Add(new SqlParameter("@dodate", DateTime.Now));


                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }


                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                //return RedirectToAction("Login", "PersonnelLogin");
            }
            return RedirectToAction("UpdatedPersonelList", new RouteValueDictionary(
                  new
                  {
                      controller = "Ship",
                      action = "UpdatedPersonelList",
                      id = 2,
                  }));
        }

        public IActionResult UpdateOfficer3(int id)
        {
            try
            {
                string username = HttpContext.Session.GetString("ShipUser");
                var pers = personService.GetUserByShip(username).Result;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@personid", id));
                        cmd.Parameters.Add(new SqlParameter("@doname", pers.surName + " " + pers.otheName));
                        cmd.Parameters.Add(new SqlParameter("@dosvcno", pers.userName));
                        cmd.Parameters.Add(new SqlParameter("@dorank", pers.rank));
                        cmd.Parameters.Add(new SqlParameter("@dodate", DateTime.Now));


                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }


                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                //return RedirectToAction("Login", "PersonnelLogin");
            }
            return RedirectToAction("UpdatedPersonelList", new RouteValueDictionary(
                  new
                  {
                      controller = "Ship",
                      action = "UpdatedPersonelList",
                      id = 3,
                  }));
        }


       
    }
}
