using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using Wkhtmltopdf.NetCore;

namespace NNPEFWEB.Controllers
{
    public class CommandController : Controller
    {
        private readonly string connectionstring;
        //private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        //private readonly IUnitOfWorks _unitOfWorks;
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IGeneratePdf _generatepdf;
        private readonly ISystemsInfoService _systemsInfo;
        public CommandController(
            ApplicationDbContext context, IUnitOfWorks unitOfWorks, IConfiguration configuration, IGeneratePdf generatepdf, ISystemsInfoService systemsInfo)
        {
            _context = context;
            _unitOfWorks = unitOfWorks;
            _generatepdf = generatepdf;
            connectionstring = configuration.GetConnectionString("DefaultConnection");
            this._systemsInfo = systemsInfo;
        }

        public IActionResult UpdatePersonnelByCommand()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult UpdatePersonnelByCommand(string payclass)
        {
            string command = HttpContext.Session.GetString("LoginCommand");
            
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdateByCommand", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@command",command ));
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


            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> downloadForm(string svcno)
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
           // string svcno = HttpContext.Session.GetString("personid");
            if (svcno == null)
            {
                return RedirectToAction("Login", "PersonnelLogin");
            }
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new PersonalInfoModel();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
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
                        //pp.Add(new PersonalInfoModel
                        //{
                        sdr.Read();
                        pp.logo = systemsInfo.company_image;
                        pp.formNumber = sdr["formnumber"].ToString();
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
                        pp.advanceDate = Convert.ToDateTime(sdr["advanceDate"]);
                        pp.yearOfPromotion = sdr["yearOfPromotion"].ToString();
                        pp.expirationOfEngagementDate = Convert.ToDateTime(sdr["expirationOfEngagementDate"]);
                        pp.home_address = sdr["home_address"].ToString();
                        pp.branch = sdr["branchName"].ToString();
                        pp.command = sdr["commandName"].ToString();
                        pp.ship = sdr["shipName"].ToString();
                        pp.specialisation = sdr["specName"].ToString();
                        pp.StateofOrigin = sdr["Name"].ToString();
                        pp.LocalGovt = sdr["lgaName"].ToString();
                        pp.religion = sdr["religion"].ToString();
                        pp.MaritalStatus = sdr["MaritalStatus"].ToString();
                        pp.AcommodationStatus = sdr["AcommodationStatus"].ToString();
                        pp.confirmedBy = sdr["confirmedBy"].ToString();

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
                        pp.nok_relation = sdr["nok_relation"].ToString();
                        pp.nok_name2 = sdr["nok_name2"].ToString();
                        pp.nok_phone2 = sdr["nok_phone2"].ToString();
                        pp.AddressofAcommodation = sdr["AddressofAcommodation"].ToString();
                        pp.nok_phone12 = sdr["nok_phone12"].ToString();
                        pp.nok_phone22 = sdr["nok_phone22"].ToString();
                        pp.nok_address2 = sdr["nok_address2"].ToString();
                        pp.nok_email2 = sdr["nok_email2"].ToString();
                        pp.nok_nationalId2 = sdr["nok_nationalId2"].ToString();
                        pp.nok_relation2 = sdr["nok_relation2"].ToString();



                        pp.Passport = ppp.Passport;
                        pp.NokPassport = ppp.NokPassport;
                        pp.AltNokPassport = ppp.AltNokPassport;

                        //    }) ;
                        //}
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/RatingReport", pp);
        }
        public async Task<IActionResult> downloadForm2(string svcno)
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
           // string svcno = HttpContext.Session.GetString("personid");
            if (svcno == null)
            {
                return RedirectToAction("Login", "PersonnelLogin");
            }
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new PersonalInfoModel();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
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
                        //pp.Add(new PersonalInfoModel
                        //{
                        sdr.Read();


                        pp.logo = systemsInfo.company_image;
                        pp.Bankcode = sdr["bankname"].ToString();
                        pp.BankACNumber = sdr["BankACNumber"].ToString();
                        pp.bankbranch = sdr["bankbranch"].ToString();
                        pp.AccountName = sdr["AccountName"].ToString();

                        pp.yearOfPromotion = sdr["yearOfPromotion"].ToString();

                        pp.rent_subsidy = sdr["rent_subsidy"].ToString();
                        pp.shift_duty_allow = sdr["shift_duty_allow"].ToString();
                        pp.aircrew_allow = sdr["aircrew_allow"].ToString();
                        pp.SBC_allow = sdr["SBC_allow"].ToString();
                        pp.hazard_allow = sdr["hazard_allow"].ToString();
                        pp.GBC = sdr["GBC"].ToString();
                        pp.GBC_Number = sdr["GBC_Number"].ToString();
                        pp.special_forces_allow = sdr["special_forces_allow"].ToString();
                        pp.other_allow = sdr["other_allow"].ToString();
                        pp.other_allowspecify = sdr["other_allowspecify"].ToString();

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


                        pp.div_off_name = sdr["div_off_name"].ToString();
                        pp.div_off_rank = sdr["div_off_rank"].ToString();
                        pp.div_off_svcno = sdr["div_off_svcno"].ToString();

                        pp.hod_name = sdr["hod_name"].ToString();
                        pp.hod_rank = sdr["hod_rank"].ToString();
                        pp.hod_svcno = sdr["hod_svcno"].ToString();

                        pp.cdr_name = sdr["cdr_name"].ToString();
                        pp.cdr_rank = sdr["cdr_rank"].ToString();
                        pp.cdr_svcno = sdr["cdr_svcno"].ToString();

                        pp.AltNokPassport = ppp.AltNokPassport;

                        //    }) ;
                        //}
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/RatingReport2", pp);
        }
        public async Task<IActionResult> downloadFormTraining(string svcno)
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
            //string svcno = HttpContext.Session.GetString("personid");
            if (svcno == null)
            {
                return RedirectToAction("Login", "PersonnelLogin");
            }
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new PersonalInfoModel();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormTraining", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        pp.logo = systemsInfo.company_image;
                        pp.formNumber = sdr["formnumber"].ToString();
                        pp.serviceNumber = sdr["serviceNumber"].ToString();
                        pp.Surname = sdr["Surname"].ToString();
                        pp.OtherName = sdr["OtherName"].ToString();
                        pp.Rank = sdr["rankName"].ToString();
                        pp.email = sdr["email"].ToString();
                        pp.gsm_number = sdr["gsm_number"].ToString();
                        pp.gsm_number2 = sdr["gsm_number2"].ToString();
                        pp.Birthdate = Convert.ToDateTime(sdr["Birthdate"]);
                        pp.DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]);
                        pp.home_address = sdr["home_address"].ToString();
                        pp.qualification = sdr["qualification"].ToString();
                        pp.division = sdr["division"].ToString();
                        //pp.ship = sdr["shipName"].ToString();
                        pp.specialisation = sdr["specialisation"].ToString();
                        pp.StateofOrigin = sdr["Name"].ToString();
                        pp.LocalGovt = sdr["lgaName"].ToString();
                        pp.religion = sdr["religion"].ToString();
                        pp.MaritalStatus = sdr["MaritalStatus"].ToString();


                        pp.nok_name = sdr["nok_name"].ToString();
                        pp.nok_phone = sdr["nok_phone"].ToString();
                        pp.nok_phone12 = sdr["nok_phone12"].ToString();
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



                        pp.Passport = ppp.Passport;
                        pp.NokPassport = ppp.NokPassport;
                        pp.AltNokPassport = ppp.AltNokPassport;
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/TrainingReport", pp);
        }
        public async Task<IActionResult> downloadFormTraining2(string svcno)
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
           // string svcno = HttpContext.Session.GetString("personid");
            if (svcno == null)
            {
                return RedirectToAction("Login", "PersonnelLogin");
            }
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new PersonalInfoModel();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormTraining", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        pp.logo = systemsInfo.company_image;
                        pp.nok_name = sdr["nok_name"].ToString();
                        pp.nok_phone = sdr["nok_phone"].ToString();
                        pp.nok_address = sdr["nok_address"].ToString();
                        pp.nok_email = sdr["nok_email"].ToString();
                        //nok_nationalId = sdr["nok_nationalId"].ToString();
                        pp.nok_relation = sdr["nok_relation"].ToString();
                        pp.nok_name2 = sdr["nok_name2"].ToString();
                        pp.nok_phone22 = sdr["nok_phone22"].ToString();
                        pp.nok_phone2 = sdr["nok_phone2"].ToString();
                        pp.nok_address2 = sdr["nok_address2"].ToString();
                        pp.nok_email2 = sdr["nok_email2"].ToString();
                        pp.nok_nationalId2 = sdr["nok_nationalId2"].ToString();
                        pp.nok_relation2 = sdr["nok_relation2"].ToString();

                        pp.Bankcode = sdr["bankname"].ToString();
                        pp.BankACNumber = sdr["BankACNumber"].ToString();
                        pp.bankbranch = sdr["bankbranch"].ToString();
                        pp.AccountName = sdr["AccountName"].ToString();



                        pp.div_off_name = sdr["div_off_name"].ToString();
                        pp.div_off_rank = sdr["div_off_rank"].ToString();
                        pp.div_off_svcno = sdr["div_off_svcno"].ToString();

                        pp.hod_name = sdr["hod_name"].ToString();
                        pp.hod_rank = sdr["hod_rank"].ToString();
                        pp.hod_svcno = sdr["hod_svcno"].ToString();

                        pp.cdr_name = sdr["cdr_name"].ToString();
                        pp.cdr_rank = sdr["cdr_rank"].ToString();
                        pp.cdr_svcno = sdr["cdr_svcno"].ToString();

                        pp.AltNokPassport = ppp.AltNokPassport;
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/TrainingReport2", pp);
        }
        public async Task<IActionResult> downloadFormOfficer(string svcno)
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
         //   string svcno = HttpContext.Session.GetString("personid");
            if (svcno == null)
            {
                return RedirectToAction("Login", "PersonnelLogin");
            }
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new PersonalInfoModel();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormOfficer", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        pp.logo = systemsInfo.company_image;
                        pp.formNumber = sdr["formnumber"].ToString();
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
                        pp.advanceDate = Convert.ToDateTime(sdr["advanceDate"]);
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
                        pp.AcommodationStatus = sdr["AcommodationStatus"].ToString();
                        pp.AddressofAcommodation = sdr["AddressofAcommodation"].ToString();
                        pp.confirmedBy = sdr["confirmedBy"].ToString();
                        pp.entry_mode = sdr["entry_mode"].ToString();
                        pp.nok_phone12 = sdr["nok_phone12"].ToString();
                        pp.nok_phone22 = sdr["nok_phone22"].ToString();

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
                        pp.nok_relation = sdr["nok_relation"].ToString();
                        pp.nok_name2 = sdr["nok_name2"].ToString();
                        pp.nok_phone2 = sdr["nok_phone2"].ToString();
                        pp.nok_address2 = sdr["nok_address2"].ToString();
                        pp.nok_email2 = sdr["nok_email2"].ToString();
                        pp.nok_nationalId2 = sdr["nok_nationalId2"].ToString();
                        pp.nok_relation2 = sdr["nok_relation2"].ToString();


                        pp.Passport = ppp.Passport;
                        pp.NokPassport = ppp.NokPassport;
                        pp.AltNokPassport = ppp.AltNokPassport;
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/OfficersReport", pp);
        }
        public async Task<IActionResult> downloadFormOfficer2(string svcno)
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
           // string svcno = HttpContext.Session.GetString("personid");
            if (svcno == null)
            {
                return RedirectToAction("Login", "PersonnelLogin");
            }
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new PersonalInfoModel();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormOfficer", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        pp.logo = systemsInfo.company_image;
                        pp.Bankcode = sdr["bankname"].ToString();
                        pp.BankACNumber = sdr["BankACNumber"].ToString();
                        pp.bankbranch = sdr["bankbranch"].ToString();
                        pp.AccountName = sdr["AccountName"].ToString();

                        pp.rent_subsidy = sdr["rent_subsidy"].ToString();
                        pp.shift_duty_allow = sdr["shift_duty_allow"].ToString();
                        pp.aircrew_allow = sdr["aircrew_allow"].ToString();
                        pp.SBC_allow = sdr["SBC_allow"].ToString();
                        pp.pilot_allow = sdr["pilot_allow"].ToString();
                        pp.hazard_allow = sdr["hazard_allow"].ToString();
                        pp.special_forces_allow = sdr["special_forces_allow"].ToString();
                        pp.other_allow = sdr["other_allow"].ToString();
                        pp.other_allowspecify = sdr["other_allowspecify"].ToString();

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



                        pp.hod_name = sdr["hod_name"].ToString();
                        pp.hod_rank = sdr["hod_rank"].ToString();
                        pp.hod_svcno = sdr["hod_svcno"].ToString();

                        pp.cdr_name = sdr["cdr_name"].ToString();
                        pp.cdr_rank = sdr["cdr_rank"].ToString();
                        pp.cdr_svcno = sdr["cdr_svcno"].ToString();

                        pp.AltNokPassport = ppp.AltNokPassport;
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/OfficersReport2", pp);
        }
    }
}
