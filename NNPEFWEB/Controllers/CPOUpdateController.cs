using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
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
        public ActionResult UpdatedPersonelByCPO(string id)
        {
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            user.Command = HttpContext.Session.GetInt32("LoginCommand");
            HttpContext.Session.SetString("Appointment", user.Appointment);

            var pp = personinfoService.GetUpdatedPersonnelByCpo(id);
            return View(pp);
        }
        [HttpPost]
        public ActionResult UpdatedPersonel(ef_personalInfo value)
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
                        cmd.Parameters.Add(new SqlParameter("@emolument", value.emolumentform));
                        cmd.Parameters.Add(new SqlParameter("@svcno", value.serviceNumber));
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
    }
}
