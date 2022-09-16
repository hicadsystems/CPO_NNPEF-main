using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNPEFWEB.Controllers.API
{
    [Route("api/PEFPersonAPI")]
    [ApiController]
    public class OficerProfileController : ControllerBase
    {
        private readonly IDapper _dapper;
        private readonly IPersonService personService;
        private readonly IPersonInfoService personinfoService;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper imapper;
        public OficerProfileController(IDapper dapper,IMapper _imapper, IWebHostEnvironment HostEnvironment, IUnitOfWorks unitOfWorks, IPersonInfoService personinfoService, IPersonService personService, ApplicationDbContext _context)
        {
            this._context = _context;
            this.personService = personService;
            this.personinfoService = personinfoService;
            this.unitOfWorks = unitOfWorks;
            this.webHostEnvironment = HostEnvironment;
            imapper = _imapper;
            _dapper = dapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getPersonBySearch/{Searching}")]
        public List<PersonListID_Name> getperson(string searching)
        {
            List<PersonListID_Name> ps = new List<PersonListID_Name>();
            var pp = personinfoService.FilterBySearch(searching).Result;
            foreach(var p in pp)
            {
                ps.Add(new PersonListID_Name
                {
                    Id = p.serviceNumber,
                    name = p.Surname + "" + p.OtherName + "" + p.serviceNumber
                });
            }
            return ps;
        }
        [Route("getAllPersons")]
        [HttpGet]
        public async Task<IActionResult> Get(int? pageno)
        {
            int? shipid = HttpContext.Session.GetInt32("ship");
            string payclass = HttpContext.Session.GetString("class");
            string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _personlist = await personinfoService.getPersonList(((int)pageno * iDisplayLength), iDisplayLength,payclass, ship);
            var countall = await personinfoService.getPersonListCount(payclass, ship);
            return Ok(new { responseCode = 200, personlist = _personlist, total = countall });
        }
       
        // GET: api/<OficerProfileController>
        [HttpGet]
        [AllowAnonymous]
        [Route("getAllPersonsByServiceNo/{serviceno}")]
        public IActionResult Get(string serviceno)
        {
            int? shipid = HttpContext.Session.GetInt32("ship");
            string Appointment = HttpContext.Session.GetString("Appointment");
            string ship = _context.ef_ships.Where(x => x.Id == shipid).FirstOrDefault().shipName;
            string id ="1";
            var pp = personinfoService.GetUpdatedPersonnelBySVCNO(id, ship, serviceno);
            return Ok(new { respondCode=200, plist= pp});
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OficerProfileController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OficerProfileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OficerProfileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet(nameof(GetById))]
        public async Task<ef_personalInfo> GetById(int Id)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<ef_personalInfo>($"Select * from [ef_personalInfos] where Id = {Id}", null, commandType: CommandType.Text));
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
       
        [HttpGet(nameof(Count))]
        public Task<int> Count(int num)
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [Dummy] WHERE Age like '%{num}%'", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(ef_personalInfo data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", data.Id);
            dbPara.Add("Name", data.Surname, DbType.String);

            var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}
