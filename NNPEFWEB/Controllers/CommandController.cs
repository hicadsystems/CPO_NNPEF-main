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

namespace NNPEFWEB.Controllers
{
    public class CommandController : Controller
    {
        private readonly string connectionstring;
        //private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        //private readonly IUnitOfWorks _unitOfWorks;
        private readonly IUnitOfWorks _unitOfWorks;
        public CommandController(
            ApplicationDbContext context, IUnitOfWorks unitOfWorks, IConfiguration configuration)
        {
            _context = context;
            _unitOfWorks = unitOfWorks;
            connectionstring = configuration.GetConnectionString("DefaultConnection");
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
    }
}
