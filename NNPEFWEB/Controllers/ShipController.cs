using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public ActionResult UpdatedPersonelList(string id, string svcno)
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
                return View();

            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                throw;
            }

        }
        public ActionResult PrintList(string id)
        {
            try
            {
                HttpContext.Session.SetString("classid", id);
                int? shipid = HttpContext.Session.GetInt32("ship");
                string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;

                 var pp = personinfoService.GetUpdatedPersonnel(id, ship);
                 return View(pp);
               

            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                throw;
            }

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

        public IActionResult UpdateOfficer2(string personsvcno)
        {
            try
            {
                string Appointment = HttpContext.Session.GetString("Appointment");
                string username = HttpContext.Session.GetString("ShipUser");
                var pers = personService.GetUserByShip(username).Result;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@person_svcno", personsvcno));
                        cmd.Parameters.Add(new SqlParameter("@appointment", pers.Appointment));
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
                      controller = "PersonalInfo",
                      action = "UpdatedPersonelList",
                      id = 2,
                  }));
        }

        public IActionResult UpdateOfficer3(string personsvcno)
        {
            try
            {
                string Appointment = HttpContext.Session.GetString("Appointment");
                string username = HttpContext.Session.GetString("ShipUser");
                var pers = personService.GetUserByShip(username).Result;
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdatePersonByShip", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@person_svcno", personsvcno));
                        cmd.Parameters.Add(new SqlParameter("@appointment", pers.Appointment));
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
                      controller = "PersonalInfo",
                      action = "UpdatedPersonelList",
                      id = 3,
                  }));
        }


       
    }
}
