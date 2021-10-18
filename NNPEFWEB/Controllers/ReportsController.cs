using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IronPdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using Wkhtmltopdf.NetCore;

namespace NNPEFWEB.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IPersonService personService;
        private readonly ISystemsInfoService _systemsInfo;
        private readonly IPersonInfoService personinfoService;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IGeneratePdf _generatepdf;
        private readonly string connectionString;
        public ReportsController(IGeneratePdf generatepdf, IUnitOfWorks unitOfWorks, IPersonInfoService personinfoService, IPersonService personService,
                                 ApplicationDbContext _context, IConfiguration configuration,
                                 ISystemsInfoService systemsInfo)
        {
            this._context = _context;
            this._systemsInfo = systemsInfo;
            this.personService = personService;
            this.personinfoService = personinfoService;
            this.unitOfWorks = unitOfWorks;
            //this.webHostEnvironment = HostEnvironment;
            //imapper = _imapper;
            _generatepdf = generatepdf;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RatingReport(string id)
        {
            var per = personinfoService.GetPersonalReport(id);
            //var pdf = new HtmlToPdf();
            //var pdfdoc = pdf.RenderHtmlAsPdf("Reports/RatingReport.cshtml");

            
            //var pix = per.Passport;
            // var nokpix = per.NokPassport;
           // return await _generatepdf.GetPdf("Reports/RatingReport", per);
            return View("RatingReport", per);
            //ReportDocument rrr = new ReportDocument();
            //rrr.Load(Path.Combine(Server.MapPath("~//RDLCReport//RatingsReport.rpt")));
            //rrr.SetDataSource(prv);

            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //CrystalDecisions.Shared.ExportOptions exportOpts = rrr.ExportOptions;
            //exportOpts.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
            //Stream stream = rrr.ExportToStream(exportOpts.ExportFormatType);
            //stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/pdf", "Personel.pdf");

           //return View("RatingReport",per);
            
        }

        public async Task<IActionResult> TrainingReport(string id)
        {
            var per = personinfoService.GetPersonalReport(id);

            return View("TrainingReport", per);
        }

        public async Task<IActionResult> OfficersReport(string id)
        {
            var per = personinfoService.GetPersonalReport(id);

            return View("OfficersReport", per);
        }

        public async Task<IActionResult> downloadForm()
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
            string svcno = HttpContext.Session.GetString("personid");
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new List<PersonalInfoModel>();
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
                        while (sdr.Read())
                        {
                            pp.Add(new PersonalInfoModel
                            {
                                logo = systemsInfo.company_image,
                                formNumber=sdr["formnumber"].ToString(),
                                serviceNumber = sdr["serviceNumber"].ToString(),
                                Surname = sdr["Surname"].ToString(),
                                OtherName = sdr["OtherName"].ToString(),
                                Rank = sdr["rankName"].ToString(),
                                email = sdr["email"].ToString(),
                                gsm_number = sdr["gsm_number"].ToString(),
                                gsm_number2 = sdr["gsm_number2"].ToString(),
                                Birthdate = Convert.ToDateTime(sdr["Birthdate"]),
                                DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]),
                                seniorityDate = Convert.ToDateTime(sdr["seniorityDate"]),
                                home_address = sdr["home_address"].ToString(),
                                branch = sdr["branchName"].ToString(),
                                command = sdr["commandName"].ToString(),
                                ship = sdr["shipName"].ToString(),
                                specialisation = sdr["specName"].ToString(),
                                StateofOrigin = sdr["Name"].ToString(),
                                LocalGovt = sdr["lgaName"].ToString(),
                                religion = sdr["religion"].ToString(),
                                MaritalStatus = sdr["MaritalStatus"].ToString(),

                                chid_name = sdr["chid_name"].ToString(),
                                chid_name2 = sdr["chid_name2"].ToString(),
                                chid_name3 = sdr["chid_name3"].ToString(),
                                chid_name4 = sdr["chid_name4"].ToString(),

                                sp_name = sdr["sp_name"].ToString(),
                                sp_phone = sdr["sp_phone"].ToString(),
                                sp_phone2 = sdr["sp_phone2"].ToString(),
                                sp_email = sdr["sp_email"].ToString(),

                                nok_name = sdr["nok_name"].ToString(),
                                nok_phone = sdr["nok_phone"].ToString(),
                                nok_address = sdr["nok_address"].ToString(),
                                nok_email = sdr["nok_email"].ToString(),
                                //nok_nationalId = sdr["nok_nationalId"].ToString(),
                                nok_relation = sdr["nok_relation"].ToString(),
                                nok_name2 = sdr["nok_name2"].ToString(),
                                nok_phone2 = sdr["nok_phone2"].ToString(),
                                nok_address2 = sdr["nok_address2"].ToString(),
                                nok_email2 = sdr["nok_email2"].ToString(),
                                nok_nationalId2 = sdr["nok_nationalId2"].ToString(),
                                nok_relation2 = sdr["nok_relation2"].ToString(),

                                Bankcode = sdr["bankname"].ToString(),
                                BankACNumber = sdr["BankACNumber"].ToString(),
                                //bankbranch = sdr["bankbranch"].ToString(),

                                rent_subsidy = sdr["rent_subsidy"].ToString(),
                                shift_duty_allow = sdr["shift_duty_allow"].ToString(),
                                aircrew_allow = sdr["aircrew_allow"].ToString(),
                                SBC_allow = sdr["SBC_allow"].ToString(),
                                hazard_allow = sdr["hazard_allow"].ToString(),
                                special_forces_allow = sdr["special_forces_allow"].ToString(),
                                other_allow = sdr["other_allow"].ToString(),

                                FGSHLS_loan = sdr["FGSHLS_loan"].ToString(),
                                welfare_loan = sdr["welfare_loan"].ToString(),
                                car_loan = sdr["car_loan"].ToString(),
                                NNMFBL_loan = sdr["NNMFBL_loan"].ToString(),
                                NNNCS_loan = sdr["NNNCS_loan"].ToString(),
                                PPCFS_loan = sdr["PPCFS_loan"].ToString(),
                                Anyother_Loan = sdr["Anyother_Loan"].ToString(),

                                FGSHLS_loanYear = sdr["FGSHLS_loanYear"].ToString(),
                                welfare_loanYear = sdr["welfare_loanYear"].ToString(),
                                car_loanYear = sdr["car_loanYear"].ToString(),
                                NNMFBL_loanYear = sdr["NNMFBL_loanYear"].ToString(),
                                NNNCS_loanYear = sdr["NNNCS_loanYear"].ToString(),
                                PPCFS_loanYear = sdr["PPCFS_loanYear"].ToString(),
                                Anyother_LoanYear = sdr["Anyother_LoanYear"].ToString(),


                                div_off_name = sdr["div_off_name"].ToString(),
                                div_off_rank = sdr["div_off_rank"].ToString(),
                                div_off_svcno = sdr["div_off_svcno"].ToString(),

                                hod_name = sdr["hod_name"].ToString(),
                                hod_rank = sdr["hod_rank"].ToString(),
                                hod_svcno = sdr["hod_svcno"].ToString(),

                                cdr_name = sdr["cdr_name"].ToString(),
                                cdr_rank = sdr["cdr_rank"].ToString(),
                                cdr_svcno = sdr["cdr_svcno"].ToString(),

                                Passport = ppp.Passport,
                                NokPassport = ppp.NokPassport,
                                AltNokPassport = ppp.AltNokPassport,

                            }) ;
                        }
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/RatingReport", pp);
        }

        public async Task<IActionResult> downloadFormTraining()
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
            string svcno = HttpContext.Session.GetString("personid");
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new List<PersonalInfoModel>();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DownloadFormTraining", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
                    sqlcon.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            pp.Add(new PersonalInfoModel
                            {
                                logo = systemsInfo.company_image,
                                formNumber = sdr["formNumber"].ToString(),
                                serviceNumber = sdr["serviceNumber"].ToString(),
                                Surname = sdr["Surname"].ToString(),
                                OtherName = sdr["OtherName"].ToString(),
                                Rank = sdr["rankName"].ToString(),
                                email = sdr["email"].ToString(),
                                gsm_number = sdr["gsm_number"].ToString(),
                                gsm_number2 = sdr["gsm_number2"].ToString(),
                                Birthdate = Convert.ToDateTime(sdr["Birthdate"]),
                                DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]),
                                seniorityDate = Convert.ToDateTime(sdr["seniorityDate"]),
                                home_address = sdr["home_address"].ToString(),
                                branch = sdr["branchName"].ToString(),
                                command = sdr["commandName"].ToString(),
                                ship = sdr["shipName"].ToString(),
                                specialisation = sdr["specName"].ToString(),
                                StateofOrigin = sdr["Name"].ToString(),
                                LocalGovt = sdr["lgaName"].ToString(),
                                religion = sdr["religion"].ToString(),
                                MaritalStatus = sdr["MaritalStatus"].ToString(),

                                chid_name = sdr["chid_name"].ToString(),
                                chid_name2 = sdr["chid_name2"].ToString(),
                                chid_name3 = sdr["chid_name3"].ToString(),
                                chid_name4 = sdr["chid_name4"].ToString(),

                                sp_name = sdr["sp_name"].ToString(),
                                sp_phone = sdr["sp_phone"].ToString(),
                                sp_phone2 = sdr["sp_phone2"].ToString(),
                                sp_email = sdr["sp_email"].ToString(),

                                nok_name = sdr["nok_name"].ToString(),
                                nok_phone = sdr["nok_phone"].ToString(),
                                nok_address = sdr["nok_address"].ToString(),
                                nok_email = sdr["nok_email"].ToString(),
                                //nok_nationalId = sdr["nok_nationalId"].ToString(),
                                nok_relation = sdr["nok_relation"].ToString(),
                                nok_name2 = sdr["nok_name2"].ToString(),
                                nok_phone2 = sdr["nok_phone2"].ToString(),
                                nok_address2 = sdr["nok_address2"].ToString(),
                                nok_email2 = sdr["nok_email2"].ToString(),
                                nok_nationalId2 = sdr["nok_nationalId2"].ToString(),
                                nok_relation2 = sdr["nok_relation2"].ToString(),

                                Bankcode = sdr["bankname"].ToString(),
                                BankACNumber = sdr["BankACNumber"].ToString(),
                                //bankbranch = sdr["bankbranch"].ToString(),

                                rent_subsidy = sdr["rent_subsidy"].ToString(),
                                shift_duty_allow = sdr["shift_duty_allow"].ToString(),
                                aircrew_allow = sdr["aircrew_allow"].ToString(),
                                SBC_allow = sdr["SBC_allow"].ToString(),
                                hazard_allow = sdr["hazard_allow"].ToString(),
                                special_forces_allow = sdr["special_forces_allow"].ToString(),
                                other_allow = sdr["other_allow"].ToString(),

                                FGSHLS_loan = sdr["FGSHLS_loan"].ToString(),
                                welfare_loan = sdr["welfare_loan"].ToString(),
                                car_loan = sdr["car_loan"].ToString(),
                                NNMFBL_loan = sdr["NNMFBL_loan"].ToString(),
                                NNNCS_loan = sdr["NNNCS_loan"].ToString(),
                                PPCFS_loan = sdr["PPCFS_loan"].ToString(),
                                Anyother_Loan = sdr["Anyother_Loan"].ToString(),

                                FGSHLS_loanYear = sdr["FGSHLS_loanYear"].ToString(),
                                welfare_loanYear = sdr["welfare_loanYear"].ToString(),
                                car_loanYear = sdr["car_loanYear"].ToString(),
                                NNMFBL_loanYear = sdr["NNMFBL_loanYear"].ToString(),
                                NNNCS_loanYear = sdr["NNNCS_loanYear"].ToString(),
                                PPCFS_loanYear = sdr["PPCFS_loanYear"].ToString(),
                                Anyother_LoanYear = sdr["Anyother_LoanYear"].ToString(),


                                div_off_name = sdr["div_off_name"].ToString(),
                                div_off_rank = sdr["div_off_rank"].ToString(),
                                div_off_svcno = sdr["div_off_svcno"].ToString(),

                                hod_name = sdr["hod_name"].ToString(),
                                hod_rank = sdr["hod_rank"].ToString(),
                                hod_svcno = sdr["hod_svcno"].ToString(),

                                cdr_name = sdr["cdr_name"].ToString(),
                                cdr_rank = sdr["cdr_rank"].ToString(),
                                cdr_svcno = sdr["cdr_svcno"].ToString(),

                                Passport = ppp.Passport,
                                NokPassport = ppp.NokPassport,
                                AltNokPassport = ppp.AltNokPassport,

                            }); ;
                        }
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/TrainingReport", pp);
        }

        public async Task<IActionResult> downloadFormOfficer()
        {
            var systemsInfo = _systemsInfo.GetSysteminfo();
            string svcno = HttpContext.Session.GetString("personid");
            var ppp = _context.ef_personalInfos.Where(o => o.serviceNumber == svcno).FirstOrDefault();
            //var per = personinfoService.downloadPersonalReport(svcno);
            var pp = new List<PersonalInfoModel>();
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
                        while (sdr.Read())
                        {
                            pp.Add(new PersonalInfoModel
                            {
                                logo = systemsInfo.company_image,
                                formNumber= sdr["formNumber"].ToString(),
                                serviceNumber = sdr["serviceNumber"].ToString(),
                                Surname = sdr["Surname"].ToString(),
                                OtherName = sdr["OtherName"].ToString(),
                                Rank = sdr["rankName"].ToString(),
                                email = sdr["email"].ToString(),
                                gsm_number = sdr["gsm_number"].ToString(),
                                gsm_number2 = sdr["gsm_number2"].ToString(),
                                Birthdate = Convert.ToDateTime(sdr["Birthdate"]),
                                DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]),
                                seniorityDate = Convert.ToDateTime(sdr["seniorityDate"]),
                                home_address = sdr["home_address"].ToString(),
                                branch = sdr["branchName"].ToString(),
                                command = sdr["commandName"].ToString(),
                                ship = sdr["shipName"].ToString(),
                                specialisation = sdr["specName"].ToString(),
                                StateofOrigin = sdr["Name"].ToString(),
                                LocalGovt = sdr["lgaName"].ToString(),
                                religion = sdr["religion"].ToString(),
                                MaritalStatus = sdr["MaritalStatus"].ToString(),

                                chid_name = sdr["chid_name"].ToString(),
                                chid_name2 = sdr["chid_name2"].ToString(),
                                chid_name3 = sdr["chid_name3"].ToString(),
                                chid_name4 = sdr["chid_name4"].ToString(),

                                sp_name = sdr["sp_name"].ToString(),
                                sp_phone = sdr["sp_phone"].ToString(),
                                sp_phone2 = sdr["sp_phone2"].ToString(),
                                sp_email = sdr["sp_email"].ToString(),

                                nok_name = sdr["nok_name"].ToString(),
                                nok_phone = sdr["nok_phone"].ToString(),
                                nok_address = sdr["nok_address"].ToString(),
                                nok_email = sdr["nok_email"].ToString(),
                                //nok_nationalId = sdr["nok_nationalId"].ToString(),
                                nok_relation = sdr["nok_relation"].ToString(),
                                nok_name2 = sdr["nok_name2"].ToString(),
                                nok_phone2 = sdr["nok_phone2"].ToString(),
                                nok_address2 = sdr["nok_address2"].ToString(),
                                nok_email2 = sdr["nok_email2"].ToString(),
                                nok_nationalId2 = sdr["nok_nationalId2"].ToString(),
                                nok_relation2 = sdr["nok_relation2"].ToString(),

                                Bankcode = sdr["bankname"].ToString(),
                                BankACNumber = sdr["BankACNumber"].ToString(),
                                //bankbranch = sdr["bankbranch"].ToString(),

                                rent_subsidy = sdr["rent_subsidy"].ToString(),
                                shift_duty_allow = sdr["shift_duty_allow"].ToString(),
                                aircrew_allow = sdr["aircrew_allow"].ToString(),
                                SBC_allow = sdr["SBC_allow"].ToString(),
                                hazard_allow = sdr["hazard_allow"].ToString(),
                                special_forces_allow = sdr["special_forces_allow"].ToString(),
                                other_allow = sdr["other_allow"].ToString(),

                                FGSHLS_loan = sdr["FGSHLS_loan"].ToString(),
                                welfare_loan = sdr["welfare_loan"].ToString(),
                                car_loan = sdr["car_loan"].ToString(),
                                NNMFBL_loan = sdr["NNMFBL_loan"].ToString(),
                                NNNCS_loan = sdr["NNNCS_loan"].ToString(),
                                PPCFS_loan = sdr["PPCFS_loan"].ToString(),
                                Anyother_Loan = sdr["Anyother_Loan"].ToString(),

                                FGSHLS_loanYear = sdr["FGSHLS_loanYear"].ToString(),
                                welfare_loanYear = sdr["welfare_loanYear"].ToString(),
                                car_loanYear = sdr["car_loanYear"].ToString(),
                                NNMFBL_loanYear = sdr["NNMFBL_loanYear"].ToString(),
                                NNNCS_loanYear = sdr["NNNCS_loanYear"].ToString(),
                                PPCFS_loanYear = sdr["PPCFS_loanYear"].ToString(),
                                Anyother_LoanYear = sdr["Anyother_LoanYear"].ToString(),


                                div_off_name = sdr["div_off_name"].ToString(),
                                div_off_rank = sdr["div_off_rank"].ToString(),
                                div_off_svcno = sdr["div_off_svcno"].ToString(),

                                hod_name = sdr["hod_name"].ToString(),
                                hod_rank = sdr["hod_rank"].ToString(),
                                hod_svcno = sdr["hod_svcno"].ToString(),

                                cdr_name = sdr["cdr_name"].ToString(),
                                cdr_rank = sdr["cdr_rank"].ToString(),
                                cdr_svcno = sdr["cdr_svcno"].ToString(),

                                Passport = ppp.Passport,
                                NokPassport = ppp.NokPassport,
                                AltNokPassport = ppp.AltNokPassport,

                            }); ;
                        }
                    }
                }
            }


            //return View("Views/Reports/RatingReport.cshtml", pp);
            return await _generatepdf.GetPdf("Reports/OfficersReport", pp);
        }
    }
}
