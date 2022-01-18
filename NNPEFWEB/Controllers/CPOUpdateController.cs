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

            var pp = personinfoService.GetUpdatedPersonnelByCpo(user.Appointment, ship);
            return View(pp);
        }
        public ActionResult UpdatedPayroll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdatedPayroll(string payclass)
        {
            string user = User.Identity.Name;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePayroll", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@payclass", payclass));
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
                               Value = rk.Id.ToString(),
                           }).ToList();


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
        public ActionResult ListOfAllStaff(string reporttype)
        {
            //if (ship == "AllStaff")
            //{
            //    var ppersons = personinfoService.GetUpdatedPersonnel2();
            //    return View(ppersons);
            //}
            //else if (ship == "Completed")
            //{
            //    var ppersons = personinfoService.GetUpdatedPersonnel2().Where(x => x.Status == "SHIP");
            //    return View(ppersons);
            //}
            //if (ship == "Auth")
            //{
            //    var ppersons = personinfoService.GetUpdatedPersonnel2().Where(x => x.Status == "CPO");
            //    return View(ppersons);
            //}
            //else if (ship == "Verified")
            //{
            //    var ppersons = personinfoService.GetUpdatedPersonnel2().Where(x => x.Status == "Verified");
            //    return View(ppersons);
            //}
            //else
            //{
            var ppersons = personinfoService.GetPersonnelStatusReport(reporttype);

            return View(ppersons);
            
           // }
        }
        public async Task<IActionResult> ListOfAllStaffReport(string ship)
        {
            var pperson = new List<ef_personalInfo>();
            if (ship== "AllStaff")
            {
               var ppersons= personinfoService.GetUpdatedPersonnel2();
                return await _generatepdf.GetPdf("Reports/StaffReportList", ppersons);
            }
            else if (ship == "Completed")
            {
                var ppersons = personinfoService.GetUpdatedPersonnel2().Where(x=>x.Status=="SHIP");
                return await _generatepdf.GetPdf("Reports/StaffReportList", ppersons);
            }
            if (ship == "Auth")
            {
                var ppersons = personinfoService.GetUpdatedPersonnel2().Where(x => x.Status == "CPO");
                return await _generatepdf.GetPdf("Reports/StaffReportList", ppersons);
            }
            else if (ship == "Verified")
            {
                var ppersons = personinfoService.GetUpdatedPersonnel2().Where(x => x.Status == "Verified");
                return await _generatepdf.GetPdf("Reports/StaffReportList", ppersons);
            }
            else 
            {
                var ppersons = personinfoService.GetUpdatedPersonnel2();
                return await _generatepdf.GetPdf("Reports/StaffReportList", ppersons);
            }
            
        }
        public ActionResult ListOfCompletedForm(string ship)
        {
            ViewBag.ShipList = GetShip();
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            var pp = personinfoService.GetUpdatedPersonnelByCpo(user.Appointment, ship);
            return View(pp);
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
        public IActionResult Export(string ship)
        {
            var pp = personinfoService.GetUpdatedPersonnel2();
            List<RPTPersonModel> rpt = new List<RPTPersonModel>();
            foreach (var person in pp)
            {
                var pps = new RPTPersonModel
                {
                    ServiceNumber = person.serviceNumber,
                    Rank = person.Rank,
                    Name = person.Surname + " " + person.OtherName,
                    Status = person.Status,
                    Ship = person.ship
                };
                rpt.Add(pps);
            }
            if (ship == "AllStaff")
            {
                 rpt=rpt.ToList();
                
            }
            else if (ship == "Completed")
            {
                rpt = rpt.Where(x=>x.Status=="SHIP").ToList();
            }
            if (ship == "Auth")
            {
                rpt = rpt.Where(x => x.Status == "CPO").ToList();
            }
            else if (ship == "Verified")
            {
                rpt = rpt.Where(x => x.Status == "Verified").ToList();
            }
            else
            {
                rpt = rpt.ToList();
            }
            var stream = new MemoryStream();


            int row = 2;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(rpt, true);
                package.Save();
            }
            string excelname = "ShipReport.xlsx";
            stream.Position = 2;
            string excelName = $"ShipReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }
    }
}
