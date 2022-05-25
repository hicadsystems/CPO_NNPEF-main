using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Controllers
{
    public class ControlSystemController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IControlService controlSystem;
      
        public ControlSystemController(ApplicationDbContext context, IControlService controlSystem)
        {
            this.context = context;
            this.controlSystem = controlSystem;
        }

        // GET: ControlSystemController
        public ActionResult Index()
        {
            var dd = controlSystem.getcontrol();
            return View(dd);
        }

        // GET: ControlSystemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ControlSystemController/Create
        public ActionResult Create()
        {
           ViewBag.shiplist=getship();
            return View();
        }

        // POST: ControlSystemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    ef_control con = new ef_control();
                    con.ship = collection["ship"];
                    con.processingyear = collection["processingyear"];
                    con.startdate = Convert.ToDateTime(collection["startdate"]);
                    con.enddate = Convert.ToDateTime(collection["enddate"]);
                    con.status = collection["status"];

                    context.ef_control.Add(con);
                    context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public List<SelectListItem> getship()
        {
            var ships = (from sh in context.ef_ships
                         select new SelectListItem()
                         {
                             Text = sh.shipName,
                             Value = sh.shipName,
                         }).ToList();

            ships.Insert(0, new SelectListItem()
            {
                Text = "ALL",
                Value = "ALL"
            });

            return ships;
        }
        // GET: ControlSystemController/Edit/5
        public ActionResult Edit(int id)
        {
            var cont = controlSystem.getcontrolbyid(id).FirstOrDefault();
            if (cont != null)
            {
                var con = new ef_control { 
                    Id=cont.Id,
                    ship=cont.ship,
                    processingyear=cont.processingyear,
                    startdate=cont.startdate,
                    enddate=cont.enddate,
                    status=cont.status
                };
                ViewBag.shiplist = getship();
                return View(con);
            }
            
            return View();
        }

        // POST: ControlSystemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ef_control con = new ef_control();
                    con.Id = id;
                    con.ship = collection["ship"];
                    con.processingyear = collection["processingyear"];
                    con.startdate = Convert.ToDateTime(collection["startdate"]);
                    con.enddate = Convert.ToDateTime(collection["enddate"]);
                    con.status = collection["status"];

                    context.ef_control.Update(con);
                    context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ControlSystemController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ControlSystemController/Delete/5
        //[HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                controlSystem.Deletcontrol(id);
                return RedirectToAction(nameof(Index));
            }
            
            catch (Exception ex)
            {
             
                return View();
            }
        }
    }
}
