using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Data;
using NNPEFWEB.Models;

namespace NNPEFWEB.Controllers.API
{
    [Route("api/command")]
    [ApiController]
    public class CommandLoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CommandLoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("getallcommand")]
        [HttpGet]
        public List<ef_command> getcommands()
        {
            var command = _context.ef_commands.ToList();
            
            return  command;

        }

        [Route("getship/{command}")]
        [HttpGet]
        public List<ef_ship> getship(int command)
        {
            var ship = _context.ef_ships.Where(c=>c.commandid==command).ToList();

            return  ship;

        }
    }
}
