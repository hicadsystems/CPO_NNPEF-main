using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using Wkhtmltopdf.NetCore;

namespace NNPEFWEB.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly IPersonService personService;
        private readonly ISystemsInfoService _systemsInfoService;
        private readonly IPersonInfoService personinfoService;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper imapper;
        private readonly IGeneratePdf _generatepdf;
        private readonly string connectionString;
        private readonly ILogger<HomeController> _logger;

        public PersonalInfoController(ILogger<HomeController> logger,IConfiguration configuration,IGeneratePdf generatepdf,IMapper _imapper,IWebHostEnvironment HostEnvironment,
                                      IUnitOfWorks unitOfWorks,IPersonInfoService personinfoService,IPersonService personService, ApplicationDbContext _context,
                                      ISystemsInfoService systemsInfoService)
        {
            _logger = logger;
            this._context = _context;
            this.personService = personService;
            this._systemsInfoService = systemsInfoService;
            this.personinfoService = personinfoService;
            this.unitOfWorks = unitOfWorks;
            this.webHostEnvironment = HostEnvironment;
            imapper = _imapper;
            _generatepdf = generatepdf;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult UpdatedPersonelList2(string svcno)
        {
            try
            {
                var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

                int? shipid = HttpContext.Session.GetInt32("ship");
                string Appointment = HttpContext.Session.GetString("Appointment");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;
                 string id= HttpContext.Session.GetString("classid");
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
                return View();

            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                throw;
            }

        }
        // GET: PersonalInfoController
        public ActionResult UpdatedPersonelList2(string id,string svcno)
        {
            try
            {
                var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                HttpContext.Session.SetString("classid", id);
                int ? shipid = HttpContext.Session.GetInt32("ship");
                string Appointment = HttpContext.Session.GetString("Appointment");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;
                if (svcno != null)
                {
                    var pp2 = personinfoService.GetUpdatedPersonnelBySVCNO(id, ship, svcno);
                    return View(pp2);
                }
                else if(ship!=null)
                {
                    var pp = personinfoService.GetUpdatedPersonnel(id, ship);
                    return View(pp);
                }
                else 
                {

                    var pp = personinfoService.GetUpdatedPersonnel(cmdr.Appointment, ship);
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
                var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                if (svcno != null)
                {
                    var pp2 = personinfoService.GetUpdatedPersonnelBySVCNO2(cmdr.Appointment, svcno).ToList();

                    PaginatedList<ef_personalInfo> personalInfos = new PaginatedList<ef_personalInfo>(pp2, 1, 1,1);

                    return View(personalInfos);
                }
                
                else
                {

                    var pp = personinfoService.GetUpdatedPersonnelRepo2(pageNumber).Result;
                    return View(pp);
                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                throw;
            }

        }
        public ActionResult PEFReport(string sortby,string cmd)
        {
            try
            {
                ViewBag.commandList = GetCommand2();
                var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                //cmdr.Command = HttpContext.Session.GetInt32("LoginCommand");
                ApiSearchModel apimodelSearch = new ApiSearchModel()
                {
                    command = cmd,
                    sortby = sortby,

                };
                var pp = personinfoService.PEFReport(apimodelSearch);
                return View(pp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }
          
        }
        public ActionResult PersonelList(string id,string sortOrder)
        {
            try
            {
                var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
               // var com = _context.ef_commands.Where(x => x.Id == cmdr.Command).FirstOrDefault();

                var pp = personinfoService.GetPersonnelByCommand(id);

                return View(pp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }
            
        }
        public ActionResult PersonelReport(string svcno)
        {
            var per = personinfoService.GetPersonalInfo(svcno).Result;
            return View(per);
        }
        public ActionResult ViewPersonel(string svcno)
        {
            try
            {


            var per = personinfoService.GetPersonalInfo(svcno).Result;
             HttpContext.Session.SetString("personid", per.serviceNumber);
         
            var pix = per.Passport;
            var nokpix = per.NokPassport;
            var altnokpic = per.AltNokPassport;


            var systemsInfo = _systemsInfoService.GetSysteminfo();
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
             var pp = new PersonalInfoModel();
            
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormOfficer", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();
                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        //while (sdr.Read())
                        //{

                        //var pp = new PersonalInfoModel

                        //{
                               pp.Birthdate = DateTime.Now;
                               pp.DateEmpl = DateTime.Now;
                        

                                sdr.Read();
                                pp.logo = systemsInfo.company_image;
                                pp.formNumber = sdr["formNumber"].ToString();
                                pp.serviceNumber = sdr["serviceNumber"].ToString();
                                pp.Surname = sdr["Surname"].ToString();
                                pp.OtherName = sdr["OtherName"].ToString();
                                pp.Rank = sdr["rankName"].ToString();
                                pp.email = sdr["email"].ToString();
                                pp.gsm_number = sdr["gsm_number"].ToString();
                                pp.gsm_number2 = sdr["gsm_number2"].ToString();
                                pp.Birthdate = Convert.ToDateTime(sdr["Birthdate"]);
                                pp.DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]);
                                pp.seniorityDate = Convert.ToDateTime(sdr["seniorityDate"]);
                                pp.runOutDate = Convert.ToDateTime(sdr["runOutDate"]);
                                pp.home_address = sdr["home_address"].ToString();
                                pp.branch = sdr["branchName"].ToString();
                                pp.command = sdr["commandName"].ToString();
                                pp.ship = sdr["shipName"].ToString();
                                pp.specialisation = sdr["specName"].ToString();
                                pp.StateofOrigin = sdr["Name"].ToString();
                                pp.LocalGovt = sdr["lgaName"].ToString();
                                pp.religion = sdr["religion"].ToString();
                                pp.MaritalStatus = sdr["MaritalStatus"].ToString();

                                pp.appointment = sdr["appointment"].ToString();

                                pp.chid_name = sdr["chid_name"].ToString();
                                pp.chid_name2 = sdr["chid_name2"].ToString();
                                pp.chid_name3 = sdr["chid_name3"].ToString();
                                pp.chid_name4 = sdr["chid_name4"].ToString();

                                pp.sp_name = sdr["sp_name"].ToString();
                                pp.sp_phone = sdr["sp_phone"].ToString();
                                pp.sp_phone2 = sdr["sp_phone2"].ToString();
                                pp.sp_email = sdr["sp_email"].ToString();

                                pp.nok_name = sdr["nok_name"].ToString();
                                pp.nok_phone = sdr["nok_phone"].ToString();
                                pp.nok_address = sdr["nok_address"].ToString();
                                pp.nok_email = sdr["nok_email"].ToString();
                                pp.nok_nationalId = sdr["nok_nationalId"].ToString();
                                pp.nok_nationalId2 = sdr["nok_nationalId"].ToString();
                                pp.nok_relation = sdr["nok_relation"].ToString();
                                pp.nok_name2 = sdr["nok_name2"].ToString();
                                pp.nok_phone2 = sdr["nok_phone2"].ToString();
                                pp.nok_address2 = sdr["nok_address2"].ToString();
                                pp.nok_email2 = sdr["nok_email2"].ToString();
                                pp.nok_nationalId2 = sdr["nok_nationalId2"].ToString();
                                pp.nok_relation2 = sdr["nok_relation2"].ToString();

                                pp.Bankcode = sdr["bankname"].ToString();
                                pp.BankACNumber = sdr["BankACNumber"].ToString();
                                pp.bankbranch = sdr["bankbranch"].ToString();

                                pp.rent_subsidy = sdr["rent_subsidy"].ToString();
                                pp.shift_duty_allow = sdr["shift_duty_allow"].ToString();
                                pp.aircrew_allow = sdr["aircrew_allow"].ToString();
                                pp.SBC_allow = sdr["SBC_allow"].ToString();
                                pp.hazard_allow = sdr["hazard_allow"].ToString();
                                pp.special_forces_allow = sdr["special_forces_allow"].ToString();
                                pp.other_allow = sdr["other_allow"].ToString();

                                pp.FGSHLS_loan = sdr["FGSHLS_loan"].ToString();
                                pp.welfare_loan = sdr["welfare_loan"].ToString();
                                pp.car_loan = sdr["car_loan"].ToString();
                                pp.NNMFBL_loan = sdr["NNMFBL_loan"].ToString();
                                pp.NNNCS_loan = sdr["NNNCS_loan"].ToString();
                                pp.PPCFS_loan = sdr["PPCFS_loan"].ToString();
                                pp.Anyother_Loan = sdr["Anyother_Loan"].ToString();

                                pp.FGSHLS_loanYear = sdr["FGSHLS_loanYear"].ToString();
                                pp.welfare_loanYear = sdr["welfare_loanYear"].ToString();
                                pp.car_loanYear = sdr["car_loanYear"].ToString();
                                pp.NNMFBL_loanYear = sdr["NNMFBL_loanYear"].ToString();
                                pp.NNNCS_loanYear = sdr["NNNCS_loanYear"].ToString();
                                pp.PPCFS_loanYear = sdr["PPCFS_loanYear"].ToString();
                                pp.Anyother_LoanYear = sdr["Anyother_LoanYear"].ToString();

                                pp.Passport = ppp.Passport;
                                pp.NokPassport = ppp.NokPassport;
                                pp.AltNokPassport = ppp.AltNokPassport;

                           // };
                                 pp.rankList = GetRank2();
                        }
                    //}
                }
            }
            

            return View(pp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "Authentication");
            }
        }
        public ActionResult ViewPersonelRating(string svcno)
        {
            try
            {

                var systemsInfo = _systemsInfoService.GetSysteminfo();
                var per = personinfoService.GetPersonalInfo(svcno).Result;
                HttpContext.Session.SetString("personid", per.serviceNumber);
               
            var pix = per.Passport;
            var nokpix = per.NokPassport;
            var altnokpix = per.AltNokPassport;
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new PersonalInfoModel();

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormRating", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        //while (sdr.Read())
                        //{

                        //var pp = new PersonalInfoModel

                        //{
                        sdr.Read();
                        pp.logo = systemsInfo.company_image;
                        pp.formNumber = sdr["formNumber"].ToString();
                        pp.serviceNumber = sdr["serviceNumber"].ToString();
                        pp.Surname = sdr["Surname"].ToString();
                        pp.OtherName = sdr["OtherName"].ToString();
                        pp.Rank = sdr["rankName"].ToString();
                        pp.email = sdr["email"].ToString();
                        pp.gsm_number = sdr["gsm_number"].ToString();
                        pp.gsm_number2 = sdr["gsm_number2"].ToString();
                            if(per.Birthdate!=null)
                        pp.Birthdate = Convert.ToDateTime(sdr["Birthdate"]);
                            if (per.DateEmpl!= null)
                                pp.DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]);
                            if (per.seniorityDate != null)
                                pp.seniorityDate = Convert.ToDateTime(sdr["seniorityDate"]);
                            if (per.runoutDate != null)
                                pp.runOutDate = Convert.ToDateTime(sdr["runOutDate"]);
                            pp.home_address = sdr["home_address"].ToString();
                        pp.branch = sdr["branchName"].ToString();
                        pp.command = sdr["commandName"].ToString();
                        pp.ship = sdr["shipName"].ToString();
                        pp.specialisation = sdr["specName"].ToString();
                        pp.StateofOrigin = sdr["Name"].ToString();
                        pp.LocalGovt = sdr["lgaName"].ToString();
                        pp.religion = sdr["religion"].ToString();
                        pp.MaritalStatus = sdr["MaritalStatus"].ToString();

                        pp.yearOfPromotion = sdr["yearOfPromotion"].ToString();
                            //pp.runOutDate = Convert.ToDateTime(sdr["runoutDate"]);
                            if (per.expirationOfEngagementDate != null)
                                pp.expirationOfEngagementDate = Convert.ToDateTime(sdr["expirationOfEngagementDate"]);

                        pp.chid_name = sdr["chid_name"].ToString();
                        pp.chid_name2 = sdr["chid_name2"].ToString();
                        pp.chid_name3 = sdr["chid_name3"].ToString();
                        pp.chid_name4 = sdr["chid_name4"].ToString();

                        pp.Sex= sdr["Sex"].ToString();
                        pp.sp_name = sdr["sp_name"].ToString();
                        pp.sp_phone = sdr["sp_phone"].ToString();
                        pp.sp_phone2 = sdr["sp_phone2"].ToString();
                        pp.sp_email = sdr["sp_email"].ToString();

                        pp.nok_name = sdr["nok_name"].ToString();
                        pp.nok_phone = sdr["nok_phone"].ToString();
                        pp.nok_address = sdr["nok_address"].ToString();
                        pp.nok_email = sdr["nok_email"].ToString();
                        pp.nok_nationalId = sdr["nok_nationalId"].ToString();
                        pp.nok_relation = sdr["nok_relation"].ToString();
                        pp.nok_name2 = sdr["nok_name2"].ToString();
                        pp.nok_phone2 = sdr["nok_phone2"].ToString();
                        pp.nok_address2 = sdr["nok_address2"].ToString();
                        pp.nok_email2 = sdr["nok_email2"].ToString();
                        pp.nok_nationalId2 = sdr["nok_nationalId2"].ToString();
                        pp.nok_relation2 = sdr["nok_relation2"].ToString();

                        pp.Bankcode = sdr["bankname"].ToString();
                        pp.BankACNumber = sdr["BankACNumber"].ToString();
                        pp.AccountName = sdr["AccountName"].ToString();
                        pp.bankbranch = sdr["bankbranch"].ToString();

                        pp.rent_subsidy = sdr["rent_subsidy"].ToString();
                        pp.shift_duty_allow = sdr["shift_duty_allow"].ToString();
                        pp.aircrew_allow = sdr["aircrew_allow"].ToString();
                        pp.SBC_allow = sdr["SBC_allow"].ToString();
                        pp.hazard_allow = sdr["hazard_allow"].ToString();
                        pp.special_forces_allow = sdr["special_forces_allow"].ToString();
                        pp.other_allow = sdr["other_allow"].ToString();

                        pp.FGSHLS_loan = sdr["FGSHLS_loan"].ToString();
                        pp.welfare_loan = sdr["welfare_loan"].ToString();
                        pp.car_loan = sdr["car_loan"].ToString();
                        pp.NNMFBL_loan = sdr["NNMFBL_loan"].ToString();
                        pp.NNNCS_loan = sdr["NNNCS_loan"].ToString();
                        pp.PPCFS_loan = sdr["PPCFS_loan"].ToString();
                        pp.Anyother_Loan = sdr["Anyother_Loan"].ToString();

                        pp.FGSHLS_loanYear = sdr["FGSHLS_loanYear"].ToString();
                        pp.welfare_loanYear = sdr["welfare_loanYear"].ToString();
                        pp.car_loanYear = sdr["car_loanYear"].ToString();
                        pp.NNMFBL_loanYear = sdr["NNMFBL_loanYear"].ToString();
                        pp.NNNCS_loanYear = sdr["NNNCS_loanYear"].ToString();
                        pp.PPCFS_loanYear = sdr["PPCFS_loanYear"].ToString();
                        pp.Anyother_LoanYear = sdr["Anyother_LoanYear"].ToString();


                       

                        pp.Passport = ppp.Passport;
                        pp.NokPassport = ppp.NokPassport;
                        pp.AltNokPassport = ppp.AltNokPassport;

                        // };
                        pp.rankList = GetRank2();
                    }
                    //}
                }
            }



            return View(pp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "Authentication");
            }

        }
        public ActionResult ViewPersonelTraining(string svcno)
        {
            try
            {
            var systemInfo = _systemsInfoService.GetSysteminfo();
            var per = personinfoService.GetPersonalInfo(svcno).Result;
             HttpContext.Session.SetString("personid", per.serviceNumber);
            
            var pix = per.Passport;
            var nokpix = per.NokPassport;
            var altnokpix = per.AltNokPassport;
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            var pp = new PersonalInfoModel();

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormOfficer", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        //while (sdr.Read())
                        //{

                        //var pp = new PersonalInfoModel

                        //{
                        sdr.Read();
                        pp.logo = systemInfo.company_image;
                        pp.formNumber = sdr["formNumber"].ToString();
                        pp.serviceNumber = sdr["serviceNumber"].ToString();
                        pp.Surname = sdr["Surname"].ToString();
                        pp.OtherName = sdr["OtherName"].ToString();
                        pp.Rank = sdr["rankName"].ToString();
                        pp.email = sdr["email"].ToString();
                        pp.gsm_number = sdr["gsm_number"].ToString();
                        pp.gsm_number2 = sdr["gsm_number2"].ToString();
                        pp.Birthdate = Convert.ToDateTime(sdr["Birthdate"]);
                        pp.DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]);
                        pp.seniorityDate = Convert.ToDateTime(sdr["seniorityDate"]);
                        pp.home_address = sdr["home_address"].ToString();
                        pp.branch = sdr["branchName"].ToString();
                        pp.command = sdr["commandName"].ToString();
                        pp.ship = sdr["shipName"].ToString();
                        pp.specialisation = sdr["specName"].ToString();
                        pp.StateofOrigin = sdr["Name"].ToString();
                        pp.LocalGovt = sdr["lgaName"].ToString();
                        pp.religion = sdr["religion"].ToString();
                        pp.MaritalStatus = sdr["MaritalStatus"].ToString();


                        pp.nok_name = sdr["nok_name"].ToString();
                        pp.nok_phone = sdr["nok_phone"].ToString();
                        pp.nok_address = sdr["nok_address"].ToString();
                        pp.nok_email = sdr["nok_email"].ToString();
                        //nok_nationalId = sdr["nok_nationalId"].ToString();
                        pp.nok_relation = sdr["nok_relation"].ToString();
                        pp.nok_name2 = sdr["nok_name2"].ToString();
                        pp.nok_phone2 = sdr["nok_phone2"].ToString();
                        pp.nok_address2 = sdr["nok_address2"].ToString();
                        pp.nok_email2 = sdr["nok_email2"].ToString();
                        pp.nok_nationalId2 = sdr["nok_nationalId2"].ToString();
                        pp.nok_relation2 = sdr["nok_relation2"].ToString();

                        pp.Bankcode = sdr["bankname"].ToString();
                        pp.BankACNumber = sdr["BankACNumber"].ToString();
                        pp.bankbranch = sdr["bankbranch"].ToString();


                        pp.div_off_name = sdr["div_off_name"].ToString();
                        pp.div_off_rank = sdr["div_off_rank"].ToString();
                        pp.div_off_svcno = sdr["div_off_svcno"].ToString();

                        pp.hod_name = sdr["hod_name"].ToString();
                        pp.hod_rank = sdr["hod_rank"].ToString();
                        pp.hod_svcno = sdr["hod_svcno"].ToString();

                        pp.cdr_name = sdr["cdr_name"].ToString();
                        pp.cdr_rank = sdr["cdr_rank"].ToString();
                        pp.cdr_svcno = sdr["cdr_svcno"].ToString();

                        pp.Passport = ppp.Passport;
                        pp.NokPassport = ppp.NokPassport;
                        pp.AltNokPassport = ppp.AltNokPassport;

                        // };
                        pp.rankList = GetRank2();
                    }
                    //}
                }
            }



            return View(pp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "Authentication");
            }
        }
        public ActionResult OfficerRecord(string id)
        {
            try
            {
            string personnel = HttpContext.Session.GetString("personnel");
                if (personnel == null)
                    return RedirectToAction("Login", "PersonnelLogin");
            var systeminfo = _systemsInfoService.GetSysteminfo();
            var pers = personService.GetPersonelByPassword(id, personnel).Result;
            var per = personinfoService.GetPersonalInfo(pers.svcNo).Result;
            string name ="Welcome "+ per.Surname + " " + per.OtherName;
            HttpContext.Session.SetString("SVC_No", name);
            HttpContext.Session.SetString("personid", per.serviceNumber);
            var pix = per.Passport;
            var nokpix = per.NokPassport;
            var altnokpic = per.AltNokPassport;
                int birthyears = DateTime.Now.Year-per.Birthdate.Value.Year;
                int empyears = DateTime.Now.Year-per.DateEmpl.Value.Year;
                if (per.entry_mode == "NDA")
                {
                    DateTime runoutDateBD = per.Birthdate.Value.AddYears(60);
                    DateTime runoutDateEM = per.DateEmpl.Value.AddYears(35);

                    if (runoutDateBD.Year > runoutDateEM.Year)
                    {
                        per.runoutDate = per.DateEmpl.Value.AddYears(35);
                    }
                    else
                    {
                        per.runoutDate = per.Birthdate.Value.AddYears(60);
                    }
                }
                else
                {
                    if (per.Birthdate != null && per.advanceDate != null)
                    {
                        DateTime runoutDateBD = per.Birthdate.Value.AddYears(60);
                        DateTime runoutDateEM = per.advanceDate.Value.AddYears(35);

                        if (runoutDateBD.Year > runoutDateEM.Year)
                        {
                            per.runoutDate = per.advanceDate.Value.AddYears(35);
                        }
                        else
                        {
                            per.runoutDate = per.Birthdate.Value.AddYears(60);
                        }
                    }
                }
            var p = new PersonalInfoModel
            {
                logo = systeminfo.company_image,
                serviceNumber = per.serviceNumber,
                Surname = per.Surname,
                OtherName = per.OtherName,
                Rank = per.Rank,
                email = per.email,
                gsm_number = per.gsm_number,
                gsm_number2 = per.gsm_number2,
                Birthdate=per.Birthdate,
                DateEmpl=per.DateEmpl,
                seniorityDate=per.seniorityDate,
                home_address=per.home_address,
                branch=per.branch,
                command=per.command,
                ship=per.ship,
                specialisation=per.specialisation,
                StateofOrigin=per.StateofOrigin,
                entry_mode=per.entry_mode,
                advanceDate=per.advanceDate,
                LocalGovt=per.LocalGovt,
                religion=per.religion,
                MaritalStatus=per.MaritalStatus,
                Passport =pix,
                NokPassport=nokpix,
                AltNokPassport=altnokpic,
                AcommodationStatus=per.AcommodationStatus,
                AddressofAcommodation=per.AddressofAcommodation,
                nok_phone22=per.nok_phone22,
                nok_phone12=per.nok_phone12,
                appointment=per.appointment,
                runOutDate=per.runoutDate,
                entitlement=per.entitlement,
                confirmedBy=per.confirmedBy,
                chid_name=per.chid_name,
                chid_name2 = per.chid_name2,
                chid_name3 = per.chid_name3,
                chid_name4 = per.chid_name4,

                sp_name = per.sp_name,
                sp_phone = per.sp_phone,
                sp_phone2 = per.sp_phone2,
                sp_email = per.sp_email,

                nok_name = per.nok_name,
                nok_phone = per.nok_phone,
                nok_address = per.nok_address,
                nok_email = per.nok_email,
                nok_nationalId = per.nok_nationalId,
                nok_relation=per.nok_relation,
                nok_name2 = per.nok_name2,
                nok_phone2 = per.nok_phone2,
                nok_address2 = per.nok_address2,
                nok_email2 = per.nok_email2,
                nok_nationalId2 = per.nok_nationalId2,
                nok_relation2=per.nok_relation2,
                
                AccountName = per.Surname+" "+per.OtherName,
                Bankcode = per.Bankcode,
                BankACNumber = per.BankACNumber,
                bankbranch = per.bankbranch,

                rent_subsidy = per.rent_subsidy,
                shift_duty_allow = per.shift_duty_allow,
                aircrew_allow = per.aircrew_allow,
                SBC_allow = per.SBC_allow,
                hazard_allow = per.hazard_allow,
                special_forces_allow = per.special_forces_allow,
                other_allow = per.other_allow,
                pilot_allow=per.pilot_allow,
                other_allowspecify=per.other_allowspecify,

                FGSHLS_loan = per.FGSHLS_loan,
                welfare_loan = per.welfare_loan,
                car_loan = per.car_loan,
                NNMFBL_loan = per.NNMFBL_loan,
                NNNCS_loan = per.NNNCS_loan,
                PPCFS_loan = per.PPCFS_loan,
                Anyother_Loan = per.Anyother_Loan,

                FGSHLS_loanYear = per.FGSHLS_loanYear,
                welfare_loanYear = per.welfare_loanYear,
                car_loanYear = per.car_loanYear,
                NNMFBL_loanYear = per.NNMFBL_loanYear,
                NNNCS_loanYear = per.NNNCS_loanYear,
                PPCFS_loanYear = per.PPCFS_loanYear,
                Anyother_LoanYear = per.Anyother_LoanYear,

            };
            p.entry_modeList = GetEntryMode();
            p.bankList = GetBank(); 
            p.rankList=GetRank2();
            p.specialisationList = GetSpec();
            p.commandList = GetCommand();
            p.branchList = GetBranch();
            p.StateofOriginList = GetState();
            p.shipList = GetShip();
            p.entry_modeList = GetEntryMode();
            p.nok_relationList = GetRelationship();
            p.nok_relation2List = GetRelationship();
                p.LocalGovtList = GetLGA2();



            return View(p);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "PersonnelLogin");
            }
        }
        [HttpPost]
        public async Task<ActionResult> OfficerRecord(ef_personalInfo value )
        {

            try
            {
                if (value.FGSHLS_loan == "No")
                    value.FGSHLS_loanYear = "";
                if (value.welfare_loan == "No")
                    value.welfare_loanYear = "";
                if (value.car_loan == "No")
                    value.car_loanYear = "";
                if (value.NNMFBL_loan == "No")
                    value.NNMFBL_loanYear = "";
                if (value.NNNCS_loan == "No")
                    value.NNNCS_loanYear = "";
                if (value.PPCFS_loanYear == "No")
                    value.PPCFS_loanYear = "";
                if (value.Anyother_Loan == "No")
                    value.Anyother_LoanYear = "";

                var per = personService.GetPersonel(value.serviceNumber).Result;
           // string profilepicture = UploadedFile(PassportFile);
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.Where(x=>x.Name=="Passport").FirstOrDefault();
                //IFormFile file2 = Request.Form.Files.Where(x => x.Name == "NokPassport").FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                   await file.CopyToAsync(dataStream);
                    //file2.CopyToAsync(dataStream);
                    value.Passport = dataStream.ToArray();
                 
                }
            }
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file2 = Request.Form.Files.Where(x => x.Name == "NokPassport").FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                   
                    await file2.CopyToAsync(dataStream);
                    value.NokPassport = dataStream.ToArray();
                }
            }
            //if (Request.Form.Files.Count > 0)
            //{
            //    IFormFile file3 = Request.Form.Files.Where(x => x.Name == "AltNokPassport").FirstOrDefault();
            //    using (var dataStream = new MemoryStream())
            //    {

            //        await file3.CopyToAsync(dataStream);
            //        value.AltNokPassport = dataStream.ToArray();
            //    }
            //}
            if (per == null)
            {
                HttpContext.Session.SetString("Message", "Record Not Found");
                return View();
            }
            else
            {
                var person = personinfoService.GetPersonalInfo(value.serviceNumber).Result;
                if (person.formNumber==null)
                {
                    var systeminfo = _systemsInfoService.GetSysteminfo();
                    int formnumber = systeminfo.OfficersFormNo;
                    _systemsInfoService.UpdateFormNumber(formnumber, person.classes);
                    value.formNumber = formnumber.ToString();

                }
                if (value.Passport == null)
                {
                    value.Passport = person.Passport;
                    value.NokPassport = person.NokPassport;
                    value.AltNokPassport = person.AltNokPassport;
                }
                    if (value.advanceDate != null)
                    {
                        DateTime runoutDateBD = value.Birthdate.Value.AddYears(60);
                        DateTime runoutDateEM = value.advanceDate.Value.AddYears(35);

                        if (runoutDateBD.Year > runoutDateEM.Year)
                        {
                            value.runoutDate = value.advanceDate.Value.AddYears(35);
                        }
                        else
                        {
                            value.runoutDate = value.Birthdate.Value.AddYears(60);
                        }

                    }
                person.formNumber = value.formNumber;
                person.Rank = value.Rank;
                person.serviceNumber = value.serviceNumber;
                person.Surname = value.Surname;
                person.OtherName = value.OtherName;
                person.email = value.email;
                person.home_address = value.home_address;
                person.gsm_number = value.gsm_number;
                person.gsm_number2 = value.gsm_number2;
                person.StateofOrigin = value.StateofOrigin;
                person.MaritalStatus = value.MaritalStatus;
                person.LocalGovt = value.LocalGovt;
                person.religion = value.religion;
                person.AccountName = value.AccountName;
                person.BankACNumber = value.BankACNumber;
                person.bankbranch = value.bankbranch;
                person.Bankcode = value.Bankcode;
                person.Birthdate = value.Birthdate;
                person.branch = value.branch;
                person.command = value.command;
                person.ship = value.ship;
                    person.entry_mode = value.entry_mode;
                    person.advanceDate = value.advanceDate;
                person.specialisation = value.specialisation;
                person.dateconfirmed = value.dateconfirmed;
                person.DateEmpl = value.DateEmpl;
                person.DateLeft = value.DateLeft;
                person.dateModify = DateTime.Now;
                person.seniorityDate = value.seniorityDate;
                    person.runoutDate = value.runoutDate;
                    person.entitlement = value.entitlement;
                    person.appointment = value.appointment;

                    person.confirmedBy = value.confirmedBy;
                person.chid_name = value.chid_name;
                person.chid_name2 = value.chid_name2;
                person.chid_name3 = value.chid_name3;
                person.chid_name4 = value.chid_name4;

                person.Passport = value.Passport;
                person.NokPassport = value.NokPassport;
                person.AltNokPassport = value.AltNokPassport;

                person.sp_name = value.sp_name;
                person.sp_phone = value.sp_phone;
                person.sp_phone2 = value.sp_phone2;
                person.sp_email = value.sp_email;
                person.AcommodationStatus = value.AcommodationStatus;
                    //added 12/13/2021
                    person.AddressofAcommodation = value.AddressofAcommodation;
                    person.nok_phone12 = value.nok_phone12;
                    person.nok_phone22 = value.nok_phone22;

                    person.nok_name = value.nok_name;
                person.nok_phone = value.nok_phone;
                person.nok_address = value.nok_address;
                person.nok_email = value.nok_email;
                person.nok_nationalId = value.nok_nationalId;
                    person.nok_relation = value.nok_relation;
                    person.nok_relation2 = value.nok_relation2;
                    person.nok_name2 = value.nok_name2;
                person.nok_phone2 = value.nok_phone2;
                person.nok_address2 = value.nok_address2;
                person.nok_email2 = value.nok_email2;
                person.nok_nationalId2 = value.nok_nationalId2;
                person.classes = 1;
                person.Bankcode = value.Bankcode;
                person.BankACNumber = value.BankACNumber;
                person.bankbranch = value.bankbranch;

                person.rent_subsidy = value.rent_subsidy;
                person.shift_duty_allow = value.shift_duty_allow;
                person.aircrew_allow = value.aircrew_allow;
                person.SBC_allow = value.SBC_allow;
                person.hazard_allow = value.hazard_allow;
                person.special_forces_allow = value.special_forces_allow;
                person.other_allow = value.other_allow;
                person.pilot_allow = value.pilot_allow;
                    person.other_allowspecify = value.other_allowspecify;

                person.FGSHLS_loan = value.FGSHLS_loan;
                person.welfare_loan = value.welfare_loan;
                person.car_loan = value.car_loan;
                person.NNMFBL_loan = value.NNMFBL_loan;
                person.NNNCS_loan = value.NNNCS_loan;
                person.PPCFS_loan = value.PPCFS_loan;
                person.Anyother_Loan = value.Anyother_Loan;

                person.FGSHLS_loanYear = value.FGSHLS_loanYear;
                person.welfare_loanYear = value.welfare_loanYear;
                person.car_loanYear = value.car_loanYear;
                person.NNMFBL_loanYear = value.NNMFBL_loanYear;
                person.NNNCS_loanYear = value.NNNCS_loanYear;
                person.PPCFS_loanYear = value.PPCFS_loanYear;
                person.Anyother_LoanYear = value.Anyother_LoanYear;
                person.Status = "SHIP";
                    person.datecreated = DateTime.Now.Date;

                    //value.Passport = profilepicture;
                await personinfoService.AddPersonalInfo(person);
              TempData["message"]="Record Added Successfully";
            
            }

            return RedirectToAction("OfficerRecord");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                TempData["message"] = "Unable to update, make sure the form is properly filled.";
                return RedirectToAction("OfficerRecord");
            }
        }
        public ActionResult RatingRecord(string id)
        {

            try
            {
            string personnel = HttpContext.Session.GetString("personnel");
                if (personnel == null)
                {
                    return RedirectToAction("Login", "PersonnelLogin");
                }
                string svcno;
            var systemsInfo = _systemsInfoService.GetSysteminfo();
            var pers = personService.GetPersonelByPassword(id, personnel).Result;
            if (pers == null)
            {
                svcno = id;
            }
            else { svcno = pers.svcNo; }
            

            var per = personinfoService.GetPersonalInfo(svcno).Result;
            if (per == null)
            {
                per.Surname = pers.surName;
                per.OtherName = pers.otheName;
                per.Rank = pers.rank;
                per.payrollclass = pers.payClass;
                per.email = pers.email;
            }
            var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            HttpContext.Session.SetString("personid", per.serviceNumber);
            //if (per.Status == null)
            //{
                
            //    per.div_off_name = cmdr.LastName + "" + cmdr.FirstName;
            //    per.div_off_rank = cmdr.Rank;
            //    per.div_off_svcno = cmdr.UserName;
            //}
            //if (per.Status == "DO")
            //{
            //    per.hod_name = cmdr.LastName + "" + cmdr.FirstName;
            //    per.hod_rank = cmdr.Rank;
            //    per.hod_svcno = cmdr.UserName;

            //}
            //if (per.Status == "HOD")
            //{
            //    per.cdr_name = cmdr.LastName + "" + cmdr.FirstName;
            //    per.cdr_rank = cmdr.Rank;
            //    per.cdr_svcno = cmdr.UserName;
            //}

            string name = "Welcome " + per.Surname + " " + per.OtherName;
            HttpContext.Session.SetString("SVC_No", name);
            var pix = per.Passport;
            var nokpix = per.NokPassport;
            var altnokpix = per.AltNokPassport;
                var p = new PersonalInfoModel
                {
                    logo = systemsInfo.company_image,
                    serviceNumber = per.serviceNumber,
                    Surname = per.Surname,
                    OtherName = per.OtherName,
                    Rank = per.Rank,
                    email = per.email,
                    gsm_number = per.gsm_number,
                    gsm_number2 = per.gsm_number2,
                    Birthdate = per.Birthdate,
                    DateEmpl = per.DateEmpl,
                    advanceDate=per.advanceDate,
                    seniorityDate = per.seniorityDate,
                    expirationOfEngagementDate = per.expirationOfEngagementDate,
                    runOutDate = per.runoutDate,
                    yearOfPromotion = per.yearOfPromotion,
                    home_address = per.home_address,
                    branch = per.branch,
                    command = per.command,
                    ship = per.ship,
                    AcommodationStatus = per.AcommodationStatus,
                    AddressofAcommodation = per.AddressofAcommodation,
                    nok_phone22 = per.nok_phone22,
                    nok_phone12 = per.nok_phone12,
                    specialisation = per.specialisation,
                StateofOrigin = per.StateofOrigin,
                LocalGovt = per.LocalGovt,
                religion = per.religion,
                MaritalStatus = per.MaritalStatus,
                Passport = pix,
                NokPassport = nokpix,
                AltNokPassport=altnokpix,
                    confirmedBy = per.confirmedBy,

                    chid_name = per.chid_name,
                chid_name2 = per.chid_name2,
                chid_name3 = per.chid_name3,
                chid_name4 = per.chid_name4,

                sp_name = per.sp_name,
                sp_phone = per.sp_phone,
                sp_phone2 = per.sp_phone2,
                sp_email = per.sp_email,

                nok_name = per.nok_name,
                nok_phone = per.nok_phone,
                nok_address = per.nok_address,
                nok_email = per.nok_email,
                nok_nationalId = per.nok_nationalId,
                nok_relation=per.nok_relation,
                nok_name2 = per.nok_name2,
                nok_phone2 = per.nok_phone2,
                nok_address2 = per.nok_address2,
                nok_email2 = per.nok_email2,
                nok_nationalId2 = per.nok_nationalId2,
                nok_relation2=per.nok_relation2,

                    AccountName = per.Surname + " " + per.OtherName,
                    Bankcode = per.Bankcode,
                BankACNumber = per.BankACNumber,
                bankbranch = per.bankbranch,

                rent_subsidy = per.rent_subsidy,
                shift_duty_allow = per.shift_duty_allow,
                aircrew_allow = per.aircrew_allow,
                SBC_allow = per.SBC_allow,
                hazard_allow = per.hazard_allow,
                special_forces_allow = per.special_forces_allow,
                other_allow = per.other_allow,
                GBC_Number=per.GBC_Number,
                    other_allowspecify = per.other_allowspecify,
                    FGSHLS_loan = per.FGSHLS_loan,
                welfare_loan = per.welfare_loan,
                car_loan = per.car_loan,
                NNMFBL_loan = per.NNMFBL_loan,
                NNNCS_loan = per.NNNCS_loan,
                PPCFS_loan = per.PPCFS_loan,
                Anyother_Loan = per.Anyother_Loan,

                FGSHLS_loanYear = per.FGSHLS_loanYear,
                welfare_loanYear = per.welfare_loanYear,
                car_loanYear = per.car_loanYear,
                NNMFBL_loanYear = per.NNMFBL_loanYear,
                NNNCS_loanYear = per.NNNCS_loanYear,
                PPCFS_loanYear = per.PPCFS_loanYear,
                Anyother_LoanYear = per.Anyother_LoanYear,

                Status = per.Status,

                div_off_name = per.div_off_name,
                div_off_rank = per.div_off_rank,
                div_off_svcno = per.div_off_svcno,

                hod_name = per.hod_name,
                hod_rank = per.hod_rank,
                hod_svcno = per.hod_svcno,

                cdr_name = per.cdr_name,
                cdr_rank = per.cdr_rank,
                cdr_svcno = per.cdr_svcno,

            };

            p.bankList = GetBank();
            p.rankList = GetRank();
            p.specialisationList = GetSpec();
            p.commandList = GetCommand();
            p.branchList = GetBranch();
            p.StateofOriginList = GetState();
            p.shipList = GetShip();
            p.entry_modeList = GetEntryMode();
            p.nok_relationList = GetRelationship();
            p.nok_relation2List = GetRelationship();
            p.LocalGovtList = GetLGA2();

                return View(p);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "PersonnelLogin");
            }


            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RatingRecord(ef_personalInfo value)
        {
            try
            {
                if (value.FGSHLS_loan == "No")
                  value.FGSHLS_loanYear="";
                if (value.welfare_loan == "No")
                    value.welfare_loanYear = "";
                if (value.car_loan == "No")
                    value.car_loanYear = "";
                if (value.NNMFBL_loan == "No")
                    value.NNMFBL_loanYear = "";
                if (value.NNNCS_loan == "No")
                    value.NNNCS_loanYear = "";
                if (value.Anyother_Loan == "No")
                    value.Anyother_LoanYear = "";



                var cmdr = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var per = personService.GetPersonel(value.serviceNumber).Result;
            // string profilepicture = UploadedFile(PassportFile);
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.Where(x => x.Name == "Passport").FirstOrDefault();
                if (file != null)
                {
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        //file2.CopyToAsync(dataStream);
                        value.Passport = dataStream.ToArray();

                    }
                }
                
            }
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file2 = Request.Form.Files.Where(x => x.Name == "NokPassport").FirstOrDefault();
                if(file2!=null)
                using (var dataStream = new MemoryStream())
                {

                    await file2.CopyToAsync(dataStream);
                    value.NokPassport = dataStream.ToArray();
                }
            }
            //if (Request.Form.Files.Count > 0)
            //{
            //    IFormFile file3 = Request.Form.Files.Where(x => x.Name == "AltNokPassport").FirstOrDefault();
            //    using (var dataStream = new MemoryStream())
            //    {

            //        await file3.CopyToAsync(dataStream);
            //        value.AltNokPassport = dataStream.ToArray();
            //    }
            //}
            if (per == null)
            {
                HttpContext.Session.SetString("Message", "Record Not Found");
                return View();
            }
            else
            {
                var person = personinfoService.GetPersonalInfo(value.serviceNumber).Result;
                if (person.formNumber == null)
                {
                    var systeminfo = _systemsInfoService.GetSysteminfo();
                    int formnumber = systeminfo.RatingsFormNo;
                    _systemsInfoService.UpdateFormNumber(formnumber, person.classes);
                    value.formNumber = formnumber.ToString();

                }
                if (value.Passport == null)
                    value.Passport = person.Passport;
                    if (value.NokPassport == null)
                    value.NokPassport = person.NokPassport;
                    if (value.AltNokPassport == null)
                    value.AltNokPassport = person.AltNokPassport;

                person.formNumber = value.formNumber;
                person.Rank = value.Rank;
                person.serviceNumber = value.serviceNumber;
                person.Surname = value.Surname;
                person.OtherName = value.OtherName;
                person.email = value.email;
                person.home_address = value.home_address;
                person.gsm_number = value.gsm_number;
                person.gsm_number2 = value.gsm_number2;
                person.StateofOrigin = value.StateofOrigin;
                person.MaritalStatus = value.MaritalStatus;
                person.LocalGovt = value.LocalGovt;
                person.religion = value.religion;
                person.BankACNumber = value.BankACNumber;
                person.bankbranch = value.bankbranch;
                person.Bankcode = value.Bankcode;
                person.Birthdate = value.Birthdate;
                person.branch = value.branch;
                person.command = value.command;
                person.ship = value.ship;
                person.specialisation = value.specialisation;
                person.dateconfirmed = value.dateconfirmed;
                person.DateEmpl = value.DateEmpl;
                person.DateLeft = value.DateLeft;
                    person.advanceDate = value.advanceDate;
                person.dateModify = DateTime.Now;
                person.seniorityDate = value.seniorityDate;
                person.expirationOfEngagementDate = value.expirationOfEngagementDate;
                person.yearOfPromotion = value.yearOfPromotion;
                    person.AcommodationStatus = value.AcommodationStatus;
                    //added 12/13/2021
                    person.AddressofAcommodation = value.AddressofAcommodation;
                    person.nok_phone12 = value.nok_phone12;
                    person.nok_phone22 = value.nok_phone22;
                    //

                    person.classes = 2;

                person.chid_name = value.chid_name;
                person.chid_name2 = value.chid_name2;
                person.chid_name3 = value.chid_name3;
                person.chid_name4 = value.chid_name4;

                person.sp_name = value.sp_name;
                person.sp_phone = value.sp_phone;
                person.sp_phone2 = value.sp_phone2;
                person.sp_email = value.sp_email;

                person.nok_name = value.nok_name;
                person.nok_phone = value.nok_phone;
                person.nok_address = value.nok_address;
                person.nok_email = value.nok_email;
                person.nok_nationalId = value.nok_nationalId;
                person.nok_relation = value.nok_relation;
                person.nok_name2 = value.nok_name2;
                person.nok_phone2 = value.nok_phone2;
                person.nok_address2 = value.nok_address2;
                person.nok_email2 = value.nok_email2;
                person.nok_nationalId2 = value.nok_nationalId2;
                person.nok_relation2 = value.nok_relation2;

                person.runoutDate = value.runoutDate;
                person.AccountName = value.AccountName;
                person.BankACNumber = value.BankACNumber;
                person.bankbranch = value.bankbranch;

                person.rent_subsidy = value.rent_subsidy;
                person.shift_duty_allow = value.shift_duty_allow;
                person.aircrew_allow = value.aircrew_allow;
                person.SBC_allow = value.SBC_allow;
                person.hazard_allow = value.hazard_allow;
                person.special_forces_allow = value.special_forces_allow;
                person.other_allow = value.other_allow;
                person.GBC_Number = value.GBC_Number;
                    person.GBC = value.GBC;
                    person.other_allowspecify = value.other_allowspecify;
                person.FGSHLS_loan = value.FGSHLS_loan;
                person.welfare_loan = value.welfare_loan;
                person.car_loan = value.car_loan;
                person.NNMFBL_loan = value.NNMFBL_loan;
                person.NNNCS_loan = value.NNNCS_loan;
                person.PPCFS_loan = value.PPCFS_loan;
                person.Anyother_Loan = value.Anyother_Loan;

                person.FGSHLS_loanYear = value.FGSHLS_loanYear;
                person.welfare_loanYear = value.welfare_loanYear;
                person.car_loanYear = value.car_loanYear;
                person.NNMFBL_loanYear = value.NNMFBL_loanYear;
                person.NNNCS_loanYear = value.NNNCS_loanYear;
                person.PPCFS_loanYear = value.PPCFS_loanYear;
                person.Anyother_LoanYear = value.Anyother_LoanYear;

                person.div_off_name = value.div_off_name;
                person.div_off_rank = value.div_off_rank;
                person.div_off_svcno = value.div_off_svcno;
                person.div_off_date = value.div_off_date;

                person.hod_name = value.hod_name;
                person.hod_rank = value.hod_rank;
                person.hod_svcno = value.hod_svcno;
                person.hod_date = value.hod_date;

                person.cdr_name = value.cdr_name;
                person.cdr_rank = value.cdr_rank;
                person.cdr_svcno = value.cdr_svcno;
                person.cdr_date = value.cdr_date;

                person.Status = "SHIP";
                    person.datecreated = DateTime.Now.Date;

                    //person.Status = cmdr.Appointment;
                    person.Passport = value.Passport;
                person.NokPassport = value.NokPassport;
                person.AltNokPassport = value.AltNokPassport;
                await personinfoService.AddPersonalInfo(person);
                HttpContext.Session.SetString("Message", "Record Added Successfully");

            }

            return RedirectToAction("RatingRecord");

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "PersonnelLogin");
            }
        }
        public ActionResult TrainingRecord(string id)
        {
            try
            {

           string personnel= HttpContext.Session.GetString("personnel");
            var systemInfo = _systemsInfoService.GetSysteminfo();
            var pers = personService.GetPersonelByPassword(id, personnel).Result;
            var per = personinfoService.GetPersonalInfo(pers.svcNo).Result;
            string name = "Welcome " + per.Surname + " " + per.OtherName;
            HttpContext.Session.SetString("SVC_No", name);
            HttpContext.Session.SetString("personid", per.serviceNumber);
            var pix = per.Passport;
            var nokpix = per.NokPassport;
            var altnokpix = per.AltNokPassport;
            var p = new PersonalInfoModel
            {
                logo = systemInfo.company_image,
                serviceNumber = per.serviceNumber,
                Surname = per.Surname,
                OtherName = per.OtherName,
                Rank = per.Rank,
                email = per.email,
                gsm_number = per.gsm_number,
                gsm_number2 = per.gsm_number2,
                Birthdate = per.Birthdate,
                DateEmpl = per.DateEmpl,
                seniorityDate = per.seniorityDate,
                home_address = per.home_address,
                branch = per.branch,
                command = per.command,
                ship = per.ship,
                specialisation = per.specialisation,
                StateofOrigin = per.StateofOrigin,
                LocalGovt = per.LocalGovt,
                religion = per.religion,
                MaritalStatus = per.MaritalStatus,
                nok_phone22 = per.nok_phone22,
                nok_phone12 = per.nok_phone12,
                Passport = pix,
                NokPassport = nokpix,
                AltNokPassport = altnokpix,
                division = per.division,
                qualification = per.qualification,

                nok_name = per.nok_name,
                nok_phone = per.nok_phone,
                nok_address = per.nok_address,
                nok_email = per.nok_email,
                nok_nationalId = per.nok_nationalId,
                nok_relation=per.nok_relation,
                nok_relation2=per.nok_relation2,
                nok_name2 = per.nok_name2,
                nok_phone2 = per.nok_phone2,
                nok_address2 = per.nok_address2,
                nok_email2 = per.nok_email2,
                nok_nationalId2 = per.nok_nationalId2,

                AccountName = per.Surname + " " + per.OtherName,
                Bankcode = per.Bankcode,
                BankACNumber = per.BankACNumber,
                bankbranch = per.bankbranch,
            };

            p.bankList = GetBank();
            p.rankList = GetRank();
            p.specialisationList = GetSpec();
            p.commandList = GetCommand();
            p.branchList = GetBranch();
            p.StateofOriginList = GetState();
            p.shipList = GetShip();
            p.entry_modeList = GetEntryMode();
            p.nok_relationList = GetRelationship();
            p.nok_relation2List = GetRelationship();
            p.LocalGovtList = GetLGA2();




                return View(p);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "PersonnelLogin");
            }
        }
        [HttpPost]
        public async Task<ActionResult> TrainingRecord(ef_personalInfo value)
        {
            try
            {

            var per = personService.GetPersonel(value.serviceNumber).Result;
            // string profilepicture = UploadedFile(PassportFile);
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.Where(x => x.Name == "Passport").FirstOrDefault();
                //IFormFile file2 = Request.Form.Files.Where(x => x.Name == "NokPassport").FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    //file2.CopyToAsync(dataStream);
                    value.Passport = dataStream.ToArray();

                }
            }
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file2 = Request.Form.Files.Where(x => x.Name == "NokPassport").FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {

                    await file2.CopyToAsync(dataStream);
                    value.NokPassport = dataStream.ToArray();
                }
            }
            //if (Request.Form.Files.Count > 0)
            //{
            //    IFormFile file3 = Request.Form.Files.Where(x => x.Name == "AltNokPassport").FirstOrDefault();
            //    using (var dataStream = new MemoryStream())
            //    {

            //        await file3.CopyToAsync(dataStream);
            //        value.AltNokPassport = dataStream.ToArray();
            //    }
            //}
            if (per == null)
            {
                HttpContext.Session.SetString("Message", "Record Not Found");
                return View();
            }
            else
            {
                var person = personinfoService.GetPersonalInfo(value.serviceNumber).Result;
                if (person.formNumber == null)
                {
                    var systeminfo = _systemsInfoService.GetSysteminfo();
                    int formnumber = systeminfo.TrainingFormNo;
                    _systemsInfoService.UpdateFormNumber(formnumber, person.classes);
                    value.formNumber = formnumber.ToString();

                }
                if (value.Passport == null)
                {
                    value.Passport = person.Passport;
                    value.NokPassport = person.NokPassport;
                    value.AltNokPassport = person.AltNokPassport;
                }
                person.formNumber = value.formNumber;
                person.Rank = value.Rank;
                person.serviceNumber = value.serviceNumber;
                person.Surname = value.Surname;
                person.OtherName = value.OtherName;
                person.email = value.email;
                person.home_address = value.home_address;
                person.gsm_number = value.gsm_number;
                person.gsm_number2 = value.gsm_number2;
                person.StateofOrigin = value.StateofOrigin;
                person.MaritalStatus = value.MaritalStatus;
                person.LocalGovt = value.LocalGovt;
                person.religion = value.religion;
                person.AccountName = value.AccountName;
                person.BankACNumber = value.BankACNumber;
                person.bankbranch = value.bankbranch;
                person.Bankcode = value.Bankcode;
                //person.Bankcode = value.Bankcode;
                person.Birthdate = value.Birthdate;
                person.branch = value.branch;
                person.command = value.command;
                person.ship = value.ship;
                person.specialisation = value.specialisation;
                person.dateconfirmed = value.dateconfirmed;
                person.DateEmpl = value.DateEmpl;
                person.DateLeft = value.DateLeft;
                person.dateModify = DateTime.Now;
                person.seniorityDate = value.seniorityDate;
                person.classes = 3;
                    //added 12/13/2021
                    person.nok_phone12 = value.nok_phone12;
                    person.nok_phone22 = value.nok_phone22;
                    person.Status = "SHIP";

                person.qualification = value.qualification;
                person.division = value.division;

                person.nok_name = value.nok_name;
                person.nok_phone = value.nok_phone;
                person.nok_address = value.nok_address;
                person.nok_email = value.nok_email;
                person.nok_nationalId = value.nok_nationalId;
                person.nok_relation = value.nok_relation;
                person.nok_name2 = value.nok_name2;
                person.nok_phone2 = value.nok_phone2;
                person.nok_address2 = value.nok_address2;
                person.nok_email2 = value.nok_email2;
                person.nok_nationalId2 = value.nok_nationalId2;
                person.nok_relation2 = value.nok_relation2;

                person.Bankcode = value.Bankcode;
                person.BankACNumber = value.BankACNumber;
                person.bankbranch = value.bankbranch;
                    person.datecreated = DateTime.Now.Date;

                    //person.rent_subsidy = value.rent_subsidy;
                    //person.shift_duty_allow = value.shift_duty_allow;
                    //person.aircrew_allow = value.aircrew_allow;
                    //person.SBC_allow = value.SBC_allow;
                    //person.hazard_allow = value.hazard_allow;
                    //person.special_forces_allow = value.special_forces_allow;
                    //person.other_allow = value.other_allow;

                    //person.FGSHLS_loan = value.FGSHLS_loan;
                    //person.welfare_loan = value.welfare_loan;
                    //person.car_loan = value.car_loan;
                    //person.NNMFBL_loan = value.NNMFBL_loan;
                    //person.NNNCS_loan = value.NNNCS_loan;
                    //person.PPCFS_loan = value.PPCFS_loan;
                    //person.Anyother_Loan = value.Anyother_Loan;




                    person.Passport = value.Passport;
                person.NokPassport = value.NokPassport;
                person.AltNokPassport = value.AltNokPassport;
                await personinfoService.AddPersonalInfo(person);
                HttpContext.Session.SetString("Message", "Record Added Successfully");

            }

            return RedirectToAction("TrainingRecord");
        }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Login", "PersonnelLogin");
    }
}
        private string UploadedFile(IFormFile Passport)
        {
            string uniqueFileName = null;

            if (Passport != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Passport.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Passport.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public List<SelectListItem> GetBank()
        {
            var banksList = (from rk in _context.ef_banks
                             select new SelectListItem()
                             {
                                 Text = rk.bankname,
                                 Value = rk.bankcode.ToString(),
                             }).ToList();

            banksList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return banksList;
        }
        public List<SelectListItem> GetRank()
        {
            var ranksList = (from rk in _context.ef_ranks
                             where rk.Id < 9
                             select new SelectListItem()
                             {
                                 Text = rk.rankName,
                                 Value = rk.rankName.ToString(),
                             }).ToList();

            ranksList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return ranksList;
        }
        public List<SelectListItem> GetRank2()
        {
            var ranksList = (from rk in _context.ef_ranks
                             where rk.Id>8
                             select new SelectListItem()
                             {
                                 Text = rk.rankName,
                                 Value = rk.rankName.ToString(),
                             }).ToList();

            ranksList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return ranksList;
        }
        public List<SelectListItem> GetSpec()
        {
            var specList = (from rk in _context.ef_specialisationareas
                             select new SelectListItem()
                             {
                                 Text = rk.specName,
                                 Value = rk.specName.ToString(),
                             }).ToList();

            specList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return specList;
        }
        public List<SelectListItem> GetCommand()
        {
            var commandList = (from rk in _context.ef_commands
                             select new SelectListItem()
                             {
                                 Text = rk.commandName,
                                 Value = rk.code.ToString(),
                             }).ToList();

            commandList.Insert(0, new SelectListItem()
            {
                Text = "Select Command",
                Value = string.Empty
            });
            return commandList;
        }
        public List<SelectListItem> GetCommand2()
        {
            var commandList = (from rk in _context.ef_commands
                               select new SelectListItem()
                               {
                                   Text = rk.commandName,
                                   Value = rk.code.ToString(),
                               }).ToList();

            commandList.Insert(0, new SelectListItem()
            {
                Text = "ALL",
                Value = "ALL"
            });
            return commandList;
        }
        public List<SelectListItem> GetBranch()
        {
            var branchList = (from rk in _context.ef_branches
                             select new SelectListItem()
                             {
                                 Text = rk.branchName,
                                 Value = rk.code.ToString(),
                             }).ToList();

            branchList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return branchList;
        }
        public List<SelectListItem> GetState()
        {
            var stateList = (from rk in _context.ef_states
                             select new SelectListItem()
                             {
                                 Text = rk.Name,
                                 Value = rk.StateId.ToString(),
                             }).ToList();

            stateList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return stateList;
        }
        public List<ef_localgovt> GetLGA(int StateId)
        {
            var lgaList = (from rk in _context.ef_localgovts 
                                   where (rk.StateId == StateId)
                             select new ef_localgovt()
                             {
                                 lgaName = rk.lgaName,
                                 Id = rk.Id,
                             }).ToList();

           
            return lgaList;
        }
        public List<SelectListItem> GetLGA2()
        {
            var lgaList = (from rk in _context.ef_localgovts
                           select new SelectListItem()
                           {
                               Text = rk.lgaName,
                               Value = rk.Id.ToString(),
                           }).ToList();


            return lgaList;
        }
        [HttpPost]
        public ActionResult GetLgasNew(int? id)
        {
            var lgas = _context.ef_localgovts.Where(x => x.StateId == id.Value).ToList();
            return Json(new SelectList(lgas, "Id", "lgaName"));
        }
        [HttpPost]
        public ActionResult Getshipbycommand(string id)
        {
            var lgas = _context.ef_ships.Where(x => x.code == id).ToList();
            return Json(new SelectList(lgas, "shipName", "shipName"));
        }
        public List<SelectListItem> GetShip()
        {
            var shipList = (from rk in _context.ef_ships
                             select new SelectListItem()
                             {
                                 Text = rk.shipName,
                                 Value = rk.shipName
                             }).ToList();

            shipList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return shipList;
        }
        public List<SelectListItem> GetRelationship()
        {
            var shipList = (from rk in _context.ef_relationships
                            select new SelectListItem()
                            {
                                Text = rk.description,
                                Value = rk.Id.ToString()
                            }).ToList();

            shipList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return shipList;
        }
        public List<SelectListItem> GetEntryMode()
        {
            var shipList = (from rk in _context.ef_entrymodes
                            select new SelectListItem()
                            {
                                Text = rk.Name,
                                Value = rk.Name
                            }).ToList();

            shipList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return shipList;
        }

            [HttpGet]
            public IActionResult PersonalBatchUpload()
            {
               return View();
            }
            [HttpPost]
            public async Task<IActionResult> PersonalBatchUpload(IFormFile formFile, CancellationToken cancellationToken, string batch)
            {
            try
            {
                if (formFile == null || formFile.Length <= 0)
                {
                    TempData["message"] = "No File Uploaded";
                    //return BadRequest("File not an Excel Format");
                    return View();
                    //return BadRequest("No File Uploaded");
                }

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["message"] = "File not an Excel Format";
                    //return BadRequest("File not an Excel Format");
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
                        string OTHERNAME = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                        string PAYCLASS = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                        string PHONE = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                        string EMAIL = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                                             
                        if (SVC_NO != "SVC_NO" || RANK != "RANK" || SURNAME != "SURNAME" || OTHERNAME != "OTHERNAME" || PAYCLASS != "PAYCLASS" || EMAIL != "EMAIL" 
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



                            string svcno = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                            string rank = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                            string surname = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                            string othername = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                            string payclass = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                            string phone = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();
                            string email = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? "" : worksheet.Cells[row, 7].Value.ToString().Trim();

                                       

                            if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()))
                              {
                                listapplicationofrecordnotavailable.Add(new personLoginVM
                                {
                                    svcNo = svcno,
                                    rank = rank,
                                    surName = surname,
                                    otheName = othername,
                                    payClass = payclass,
                                    phoneNumber = phone,
                                    email = email,
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
                                    payClass = payclass,
                                    phoneNumber = phone,
                                    email = email,
                                });
                            }

                        }
                        string userp = User.Identity.Name;

                        ProcesUpload procesUpload2 = new ProcesUpload(null,null, listapplication, unitOfWorks, userp);
                        await procesUpload2.processUploadInThread();
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
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
                //return RedirectToAction("Login", "PersonnelLogin");
            }
        }
        [HttpPost]
        public IActionResult CommandUpdateOfficer(PersonalInfoModel model)
        {
            try
            {
                string Appointment = HttpContext.Session.GetString("Appointment");

           
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                if (Appointment == "DO")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@person_svcno", model.serviceNumber));
                        cmd.Parameters.Add(new SqlParameter("@appointment", Appointment));
                        cmd.Parameters.Add(new SqlParameter("@doname", model.div_off_name));
                        cmd.Parameters.Add(new SqlParameter("@dosvcno", model.div_off_svcno));
                        cmd.Parameters.Add(new SqlParameter("@dorank", model.div_off_rank));
                        cmd.Parameters.Add(new SqlParameter("@dodate", DateTime.Now));

                            cmd.Parameters.Add(new SqlParameter("@hodname", model.hod_name==""));
                            cmd.Parameters.Add(new SqlParameter("@hodsvcno", model.hod_svcno==""));
                            cmd.Parameters.Add(new SqlParameter("@hodrank", model.hod_rank==""));
                            cmd.Parameters.Add(new SqlParameter("@hoddate", DateTime.Now));

                            cmd.Parameters.Add(new SqlParameter("@cdrname", model.cdr_name == ""));
                            cmd.Parameters.Add(new SqlParameter("@cdrsvcno", model.cdr_svcno == ""));
                            cmd.Parameters.Add(new SqlParameter("@cdrrank", model.cdr_rank == ""));
                            cmd.Parameters.Add(new SqlParameter("@cdrdate", DateTime.Now));

                            sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (Appointment == "HOD")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@person_svcno", model.serviceNumber));
                        cmd.Parameters.Add(new SqlParameter("@appointment", Appointment));
                        cmd.Parameters.Add(new SqlParameter("@doname", model.div_off_name == ""));
                        cmd.Parameters.Add(new SqlParameter("@dosvcno", model.div_off_svcno == ""));
                        cmd.Parameters.Add(new SqlParameter("@dorank", model.div_off_rank == ""));
                        cmd.Parameters.Add(new SqlParameter("@dodate", DateTime.Now));

                        cmd.Parameters.Add(new SqlParameter("@hodname", model.hod_name));
                        cmd.Parameters.Add(new SqlParameter("@hodsvcno", model.hod_svcno));
                        cmd.Parameters.Add(new SqlParameter("@hodrank", model.hod_rank));
                        cmd.Parameters.Add(new SqlParameter("@hoddate", DateTime.Now));

                        cmd.Parameters.Add(new SqlParameter("@cdrname", model.cdr_name == ""));
                        cmd.Parameters.Add(new SqlParameter("@cdrsvcno", model.cdr_svcno == ""));
                        cmd.Parameters.Add(new SqlParameter("@cdrrank", model.cdr_rank == ""));
                        cmd.Parameters.Add(new SqlParameter("@cdrdate", DateTime.Now));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (Appointment == "CDR")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@person_svcno", model.serviceNumber));
                        cmd.Parameters.Add(new SqlParameter("@appointment", Appointment));
                        cmd.Parameters.Add(new SqlParameter("@doname", model.div_off_name == ""));
                        cmd.Parameters.Add(new SqlParameter("@dosvcno", model.div_off_svcno == ""));
                        cmd.Parameters.Add(new SqlParameter("@dorank", model.div_off_rank == ""));
                        cmd.Parameters.Add(new SqlParameter("@dodate", DateTime.Now));

                        cmd.Parameters.Add(new SqlParameter("@hodname", model.hod_name == ""));
                        cmd.Parameters.Add(new SqlParameter("@hodsvcno", model.hod_svcno == ""));
                        cmd.Parameters.Add(new SqlParameter("@hodrank", model.hod_rank == ""));
                        cmd.Parameters.Add(new SqlParameter("@hoddate", DateTime.Now));

                        cmd.Parameters.Add(new SqlParameter("@cdrname", model.cdr_name));
                        cmd.Parameters.Add(new SqlParameter("@cdrsvcno", model.cdr_svcno));
                        cmd.Parameters.Add(new SqlParameter("@cdrrank", model.cdr_rank));
                        cmd.Parameters.Add(new SqlParameter("@cdrdate", DateTime.Now));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
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
                      controller = "PersonalInfo",
                      action = "UpdatedPersonelList",
                      id = model.payrollclass,
                  }));
        }
        public IActionResult CommisionedPersonnelUpload()
        {
            return View();
        }
            public async Task<IActionResult> CommisionedPersonnelUpload(IFormFile formFile, CancellationToken cancellationToken, string batch)
        {
            try
            {
                if (formFile == null || formFile.Length <= 0)
                {
                    TempData["message"] = "No File Uploaded";
                    //return BadRequest("File not an Excel Format");
                    return View();
                    //return BadRequest("No File Uploaded");
                }

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["message"] = "File not an Excel Format";
                    //return BadRequest("File not an Excel Format");
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
                        string OTHERNAME = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                        string PAYCLASS = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                        string PHONE = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                        string EMAIL = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();

                        if (SVC_NO != "SVC_NO" || RANK != "RANK" || SURNAME != "SURNAME" || OTHERNAME != "OTHERNAME" || PAYCLASS != "PAYCLASS" || EMAIL != "EMAIL"
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



                            string svcno = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                            string rank = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                            string surname = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                            string othername = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                            string payclass = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                            string phone = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();
                            string email = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? "" : worksheet.Cells[row, 7].Value.ToString().Trim();



                            if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()))
                            {
                                listapplicationofrecordnotavailable.Add(new personLoginVM
                                {
                                    svcNo = svcno,
                                    rank = rank,
                                    surName = surname,
                                    otheName = othername,
                                    payClass = payclass,
                                    phoneNumber = phone,
                                    email = email,
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
                                    payClass = payclass,
                                    phoneNumber = phone,
                                    email = email,
                                });
                            }

                        }
                        string userp = User.Identity.Name;

                        ProcesUpload procesUpload2 = new ProcesUpload(null, null, listapplication, unitOfWorks, userp);
                        await procesUpload2.processUploadInThread();
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
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
                //return RedirectToAction("Login", "PersonnelLogin");
            }
        }

    }
}
