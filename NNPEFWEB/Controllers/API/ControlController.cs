using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNPEFWEB.Controllers.API
{
    [Route("api/ControlAPI")]
    [ApiController]
    public class ControlController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IControlService _controlService;
        public ControlController(ApplicationDbContext context, IControlService controlService)
        {
            _context = context;
            _controlService = controlService;
        }
        // GET: api/<ControlController>
        [Route("getcontrol")]
        [HttpGet]
        public IEnumerable<ef_control> Getcontrol()
        {
            var con = _controlService.getcontrol();
            return con;
        }

        // GET api/<ControlController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ControlController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ControlController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ControlController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
