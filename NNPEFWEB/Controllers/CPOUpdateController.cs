using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using OfficeOpenXml;
using Wkhtmltopdf.NetCore;

namespace NNPEFWEB.Controllers
{
    public class CPOUpdateController : Controller
    {
        private readonly IPersonService personService;
        private readonly ISystemsInfoService _systemsInfoService;
        private readonly IPersonInfoService personinfoService;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IMapper imapper;
        private readonly IGeneratePdf _generatepdf;
        private readonly string connectionString;

        public CPOUpdateController(IConfiguration configuration, IGeneratePdf generatepdf, IMapper _imapper,
                                      IUnitOfWorks unitOfWorks, IPersonInfoService personinfoService, IPersonService personService, ApplicationDbContext _context,
                                      ISystemsInfoService systemsInfoService)
        {
            this._context = _context;
            this.personService = personService;
            this._systemsInfoService = systemsInfoService;
            this.personinfoService = personinfoService;
            this.unitOfWorks = unitOfWorks;
            imapper = _imapper;
            _generatepdf = generatepdf;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult sectiona()
        {
            return View();
        }
        public IActionResult sectionb()
        {
            return View();
        }
        public IActionResult sectionc()
        {
            return View();
        }
        public IActionResult sectiond()
        {
            return View();
        }
        public IActionResult sectione()
        {
            return View();
        }
        public IActionResult sectionf()
        {
            return View();
        }
        public ActionResult UpdatedPersonelByCPO(string ship)
        {
            ViewBag.ShipList = GetShip();
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            string shipname= ship;
            ViewData["shipSearchedID"] = shipname;
            //if (ship != null)
            //{
            //     shipname = _context.ef_ships.FirstOrDefault(x => x.Id == Convert.ToInt32(ship)).shipName;
            //     ViewData["shipSearchedID"] = shipname;
            //}
            var pp = personinfoService.GetUpdatedPersonnelByCpo(user.Appointment, shipname);
            return View(pp);
        }
        //public ActionResult UpdatedPayroll()
        //{
        //    return View();
        //}
        //[HttpPost]
        public ActionResult UpdatedPayroll(int id)
        {
            //string user = User.Identity.Name;
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePayroll", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@username", user.UserName));



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


            return RedirectToAction("UpdatedPersonelByCPO");
        }
    

       public string getsection(string appointment)
        {
            string sect="";
            if (appointment == "SectionA")
            {
                sect = "1";
            }
            else if (appointment == "SectionB")
            {
                sect = "2";
            }
            else if (appointment == "SectionC")
            {
                sect = "3";
            }
            else if (appointment == "SectionD")
            {
                sect = "4";
            }
            else if (appointment == "SectionE")
            {
                sect = "5";
            }
            else if (appointment == "SectionF")
            {
                sect = "5";
            }
            return sect;
        }
        public List<SelectListItem> GetShip()
        {
            var lgaList = (from rk in _context.ef_ships.OrderBy(x=>x.shipName)
                           select new SelectListItem()
                           {
                               Text = rk.shipName,
                               Value = rk.shipName,
                           }).ToList();

            lgaList.Insert(0, new SelectListItem()
            {
                Text = "All",
                Value = string.Empty
            });
            return lgaList;
        }
        public ActionResult UpdatedPersonel(int id)
        {
            string user = User.Identity.Name;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePayrollWithEF", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@personid", id));
                        cmd.Parameters.Add(new SqlParameter("@username", User.Identity.Name));


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


            return RedirectToAction("UpdatedPersonelList");
         }
        public async Task<IActionResult> ListOfAllStaff(string reporttype, string shipToSearch,string payclass,string type, int? pageNumber)
        {
            try
            {
                if (reporttype == "All")
                    reporttype = null;
                ViewData["type"] = type;
                ViewData["reporttype"] = reporttype;
                ViewData["Payclass"] = payclass;
                ViewData["shipSearchedID"] = shipToSearch;
                var ppersons = await personinfoService.GetPersonnelStatusReport(reporttype, shipToSearch, payclass,pageNumber);

            var allShip = GetShip();

            //if (!string.IsNullOrEmpty(shipToSearch) && shipToSearch != "AllShip")
            //{
            //    //var shipSearched = _context.ef_ships.Where(x=>x.shipName==shipToSearch).FirstOrDefault();

            //    ViewData["shipSearchedID"] = shipToSearch;

            ////    allShip.Insert(0, new SelectListItem { Value = shipSearched.shipName, Text = shipSearched.shipName });
            ////    allShip.Insert(1, new SelectListItem { Value = "AllShip", Text = "All Ship" });
            //}
            //else
            //{
            //    ViewData["shipSearchedID"] = "AllShip";

            //  //  allShip.Insert(0, new SelectListItem { Value = "AllShip", Text = "All Ship" });
            //}

            ViewBag.ShipList = allShip;

            return View(ppersons);

            }
            catch (Exception ex)
            {
                TempData["UpdateMessage"] = "Updated Fail";

                throw;
            }
        }
        public Task<IActionResult> ListOfAllStaffReport(string ship, string shipToSearch,string payclass,string type)
        {

            var pp = personinfoService.GetPersonnelStatusRepoReport(ship, shipToSearch, payclass);
            List<RPTPersonModel> rpt = new List<RPTPersonModel>();
            foreach (var person in pp)
            {
                var pps = new RPTPersonModel
                {
                    ServiceNumber = person.serviceNumber,
                    Rank = person.Rank,
                    Name = person.Surname + " " + person.OtherName,
                    Ship = person.ship,
                    GSM_NO = person.gsm_number,
                    Date_Of_Birth = person.Birthdate == null ? "00-00-0000" : person.Birthdate.ToString(),
                    Residential_Address = person.home_address,
                    Date_Of_Expiration = person.expirationOfEngagementDate == null ? "00-00-0000" : person.expirationOfEngagementDate.ToString(),
                    Seniority_Date = person.seniorityDate == null ? "00-00-0000" : person.seniorityDate.ToString(),
                    Email_Address = person.email,
                    NOK = person.nok_name,
                    NOK_GSM_NO = person.nok_phone,
                    Bank = person.Bankcode,
                    Account_No = person.BankACNumber,
                    Accomodation_Status = person.AcommodationStatus,
                };
                rpt.Add(pps);
             }

            return _generatepdf.GetPdf("Reports/StaffReportList", rpt);
        }
        public Task<IActionResult> ListOfCompletedForm(string ship)
        {
            ViewBag.ShipList = GetShip();
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            List<RPTPersonModel> rpt = new List<RPTPersonModel>();
            var pp = personinfoService.GetUpdatedPersonnelByCpo(user.Appointment, ship);
            foreach (var person in pp)
            {
                var pps = new RPTPersonModel
                {

                    ServiceNumber = person.serviceNumber,
                    Rank = person.Rank,
                    Name = person.Surname + " " + person.OtherName,
                    Ship = person.ship,
                    GSM_NO = person.gsm_number,
                    Date_Of_Birth = person.Birthdate == null ? "00-00-0000" : person.Birthdate.ToString(),
                    Residential_Address = person.home_address,
                    Date_Of_Expiration = person.expirationOfEngagementDate == null ? "00-00-0000" : person.expirationOfEngagementDate.ToString(),
                    Seniority_Date = person.seniorityDate == null ? "00-00-0000" : person.seniorityDate.ToString(),
                    Email_Address = person.email,
                    NOK = person.nok_name,
                    NOK_GSM_NO = person.nok_phone,
                    Bank = person.Bankcode,
                    Account_No = person.BankACNumber,
                    Accomodation_Status = person.AcommodationStatus,
                    HOD = person.hod_svcno,
                    Payclass = person.payrollclass,
                };
                rpt.Add(pps);
            }
            return _generatepdf.GetPdf("Reports/StaffReportList", rpt);
        }
        public ActionResult ListOfAuthForm(string ship)
        {
            ViewBag.ShipList = GetShip();
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            var pp = personinfoService.GetUpdatedPersonnelByCpo(user.Appointment, ship);
            return View(pp);
        }
        public ActionResult ListOfCertifyForm(string ship)
        {
            ViewBag.ShipList = GetShip();
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            var pp = personinfoService.GetUpdatedPersonnelByCpo(user.Appointment, ship);
            return View(pp);
        }
        public ActionResult ListOfProceedForm(string ship)
        {
            ViewBag.ShipList = GetShip();
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            var pp = personinfoService.GetUpdatedPersonnelByCpo(user.Appointment, ship);
            return View(pp);
        }
        [HttpPost]
        public IActionResult Export(string ship, string shipToSearch,string payclass,string type)
        {
            var pp = personinfoService.GetPersonnelStatusRepoReport(ship, shipToSearch,payclass);
            List<RPTPersonModel> rpt = new List<RPTPersonModel>();
            List<RPTPersonModel2> rpt2 = new List<RPTPersonModel2>();
            if (type == "Paycard")
            {
                foreach (var person in pp)
                {
                    var pps = new RPTPersonModel
                    {

                        ServiceNumber=person.serviceNumber,
                        Rank=person.Rank,
                        Name= person.Surname + " " + person.OtherName,
                        Ship=person.ship,
                        GSM_NO=person.gsm_number,
                        Date_Of_Birth=person.Birthdate == null ? "00-00-0000" : person.Birthdate.ToString(),
                        Residential_Address=person.home_address,
                        Date_Of_Expiration= person.expirationOfEngagementDate == null ? "00-00-0000" : person.expirationOfEngagementDate.ToString(),
                        Seniority_Date= person.seniorityDate == null ? "00-00-0000" : person.seniorityDate.ToString(),
                        Email_Address =person.email,
                        NOK=person.nok_name,
                        NOK_GSM_NO=person.nok_phone,
                        Bank=person.Bankcode,
                        Account_No=person.BankACNumber,
                        Accomodation_Status=person.AcommodationStatus,
                        HOD = person.hod_svcno,
                        Payclass = person.payrollclass,
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
                string excelName = $"ReportToUpdatePayroll-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            else
            {
                foreach (var person in pp)
                {
                    var pps = new RPTPersonModel2
                    {
                    ServiceNumber=person.serviceNumber,
                    Rank=person.Rank,
                    SurName=person.Surname,
                    OtherName = person.Surname,
                    GSM_NO =person.gsm_number,
                    Maritial_Status=person.MaritalStatus,
                    Date_Of_Birth= person.Birthdate == null ? "00-00-0000" : person.Birthdate.ToString(),
                    Residential_Address =person.home_address,
                    Date_Of_Expiration= person.expirationOfEngagementDate == null ? "00-00-0000" : person.expirationOfEngagementDate.ToString(),
                    Seniority_Date = person.seniorityDate == null ? "00-00-0000" : person.seniorityDate.ToString(),
                    DOJ = person.DateEmpl == null ? "00-00-0000" : person.DateEmpl.ToString(),
                    Email_Address =person.email,
                    NOK=person.nok_name,
                    NOK_GSM_NO=person.nok_phone,
                    Bank=person.Bankcode,
                    Account_No=person.BankACNumber,
                    Accomodation_Status=person.AcommodationStatus,
                    Branch=person.branch,
                    Command=person.command,
                    Ship=person.ship,
                    Specialisation=person.specialisation,
                    State=person.StateofOrigin,
                    LGA=person.LocalGovt,
                    Religion=person.religion,
                    HOD=person.hod_svcno,
                    
                    Payclass=person.payrollclass,

    };
                    rpt2.Add(pps);
                }
                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(rpt2, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"ReportToUpdatePayroll-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

          
        }
        [HttpPost]
        public IActionResult ExportByHOD(string ship, string shipToSearch, string payclass, string type)
        {
            var pp = personinfoService.GetPersonnelStatusRepoReport(ship, shipToSearch, payclass);
            List<RPTPersonModel> rpt = new List<RPTPersonModel>();
            List<RPTPersonModel2> rpt2 = new List<RPTPersonModel2>();
            foreach (var person in pp)
            {
                var pps = new RPTPersonModel
                {
                    ServiceNumber = person.serviceNumber,
                    Rank = person.Rank,
                    Name = person.Surname + " " + person.OtherName,
                    Ship = person.ship,
                    GSM_NO = person.gsm_number,
                    Date_Of_Birth = person.Birthdate == null ? "00-00-0000" : person.Birthdate.ToString(),
                    Residential_Address = person.home_address,
                    Date_Of_Expiration = person.expirationOfEngagementDate == null ? "00-00-0000" : person.expirationOfEngagementDate.ToString(),
                    Seniority_Date = person.seniorityDate == null ? "00-00-0000" : person.seniorityDate.ToString(),
                    Email_Address = person.email,
                    NOK = person.nok_name,
                    NOK_GSM_NO = person.nok_phone,
                    Bank = person.Bankcode,
                    Account_No = person.BankACNumber,
                    Accomodation_Status = person.AcommodationStatus,
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
            string excelName = $"SectionalReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public async Task<IActionResult> ListOfCertisfiedForm(string ship,string status, int? pageNumber)
        {
            ViewData["status"] = String.IsNullOrEmpty(status) ? "AllStaff" : status;
            ViewBag.ShipList = GetShip();
            ViewData["shipSearchedID"] = ship;
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            var pp  =await personinfoService.GetUpdatedPersonnelByHOD(user.Appointment,status,ship,pageNumber);
            return View(pp);
        }
        [HttpPost]
        public async Task<IActionResult> ExportByHOD2(string ship,string status)
        {
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["status"] = String.IsNullOrEmpty(status) ? "AllStaff" : status;
            var pp = await personinfoService.GetUpdatedPersonnelByHOD2(user.Appointment, status, ship);
            List<RPTPersonModel> rpt = new List<RPTPersonModel>();
            foreach (var person in pp)
            {
                var pps = new RPTPersonModel
                {
                    ServiceNumber = person.serviceNumber,
                    Rank = person.Rank,
                    Name = person.Surname + " " + person.OtherName,
                    Ship = person.ship,
                    GSM_NO = person.gsm_number,
                    Date_Of_Birth = person.Birthdate == null ? "00-00-0000" : person.Birthdate.ToString(),
                    Residential_Address = person.home_address,
                    Date_Of_Expiration = person.expirationOfEngagementDate == null ? "00-00-0000" : person.expirationOfEngagementDate.ToString(),
                    Seniority_Date = person.seniorityDate == null ? "00-00-0000" : person.seniorityDate.ToString(),
                    Email_Address = person.email,
                    NOK = person.nok_name,
                    NOK_GSM_NO = person.nok_phone,
                    Bank = person.Bankcode,
                    Account_No = person.BankACNumber,
                    Accomodation_Status = person.AcommodationStatus,
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
            string excelName = $"SectionalReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public async Task<IActionResult> ListOfAllStaffReportByHOD(string ship, string status)
        {
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var ppersons = await personinfoService.GetUpdatedPersonnelByHOD2(user.Appointment, status, ship);

            List<ef_personalInfo> rpt = new List<ef_personalInfo>();

            foreach (var person in ppersons)
            {
                var pps = new ef_personalInfo
                {
                    Rank = person.Rank,
                    serviceNumber = person.serviceNumber,
                    Surname = person.Surname,
                    OtherName = person.OtherName,
                    ship = person.ship,
                };
                rpt.Add(pps);
            }

            return await _generatepdf.GetPdf("Reports/StaffReportList", rpt);
        }
    }
}
