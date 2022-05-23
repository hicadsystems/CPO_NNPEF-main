using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Models;

namespace NNPEFWEB.Controllers
{
    public class StaticTablesController : Controller
    {
        private readonly ApplicationDbContext _context;
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<ef_ship> addresses = new List<ef_ship>();
        private readonly ILogger<HomeController> _logger;
        private readonly string connectionString;
        public StaticTablesController(ApplicationDbContext _context, ILogger<HomeController> logger, IConfiguration configuration)
        {
            this._context = _context;
            _logger = logger;
            con.ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Index2()
        {
            FetchData();
            return View(addresses);
        }
        private void FetchData()
        {
            if (addresses.Count > 0)
            {
                addresses.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [Id],[shipName],[code] from ef_ships";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    addresses.Add(new ef_ship()
                    {
                        Id=Convert.ToInt32(dr["Id"]),
                        shipName = dr["shipName"].ToString()
                    ,
                        code = dr["code"].ToString()
                    });;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Index()
        {
            var ship= _context.ef_ships.ToList();
            return View(ship);
        }
        public IActionResult createship()
        {
           ViewBag.GetCommands=GetCommand();
            return View();
        }
        [HttpPost]
        public IActionResult createship(ef_ship ships)
        {
            try
            {

            if (ships != null)
            {
                var checkship = _context.ef_ships.Where(x => x.shipName == ships.shipName).Count();
                if (checkship == 0)
                {
                    _context.ef_ships.Add(ships);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index2));
                }
            }
                else
                {
                    TempData["messages"] = "Ship already Exist";
                    return RedirectToAction(nameof(createship));
                }

               
            return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                TempData["messages"] = "Error Contact Admin";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Editship(int id)
        {
            ViewBag.GetCommands = GetCommand();
            var ships = _context.ef_ships.Where(x => x.Id == id).FirstOrDefault();
            var rec = new ef_ship
            {
                Id=ships.Id,
                shipName = ships.shipName,
                code = ships.code,
                commandid = ships.commandid
            };
            return View(rec);
        }
        [HttpPost]
        public IActionResult Editship(ef_ship ships)
        {
            try
            {

                if (ships != null)
                {
                    var checkship = _context.ef_ships.Where(x => x.Id == ships.Id).Count();
                    if (checkship != 0)
                    {
                        _context.ef_ships.Update(ships);
                        _context.SaveChanges();

                        return RedirectToAction(nameof(Index2));
                    }
                }
                

                return RedirectToAction(nameof(Index2));

            }
            catch (Exception ex)
            {
                TempData["messages"] = "Error Contact Admin";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Deleteship(int id)
        {
            var ships = _context.ef_ships.Where(x => x.Id == id).FirstOrDefault();
            _context.ef_ships.Remove(ships);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index2));
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
    }
}
