using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNPEFWEB.Controllers.API
{
    [Route("api/Reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IPersonInfoService personinfoService;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWorks unitOfWorks;
        public ReportController(IUnitOfWorks unitOfWorks, IPersonInfoService personinfoService, IPersonService personService, ApplicationDbContext _context)
        {
            this._context = _context;
            this.personService = personService;
            this.personinfoService = personinfoService;
            this.unitOfWorks = unitOfWorks;
        }
        // GET: api/<ReportController>
        [HttpGet("{id}")]
        public IEnumerable<PersonalInfoModel> Get(string id)
        {
           return personinfoService.GetPersonalReport(id);
        }
        [Route("getListPerson")]
        [HttpGet]
        public List<ef_personalInfo> GetAllpersonel()
        {
            var pp= personinfoService.GetPEFReport();
            return pp;
        }
        [Route("getAllPersons")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> Get(int? pageno)
        {
            int iDisplayLength = 10;
            pageno = pageno == null ? 0 : (pageno--);
            var _personlist = await personinfoService.getPersonList(((int)pageno * iDisplayLength), iDisplayLength);
            var countall = await personinfoService.getPersonListCount();
            return Ok(new { responseCode = 200, personlist = _personlist, total = countall });
        }
        // POST api/<ReportController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        //[HttpPut]
        //public ef_personalInfo Put([FromForm] ef_personalInfo ef)
        //{
        //    //…
        //}

        // DELETE api/<ReportController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
