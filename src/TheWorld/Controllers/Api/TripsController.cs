using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(new Trip() { Name = "My Trip", Date = DateTime.Now });
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

  
        [HttpPost()]
        public IActionResult Post([FromBody]Trip trip)
        {
            return Ok(true);
        }

    }
}
