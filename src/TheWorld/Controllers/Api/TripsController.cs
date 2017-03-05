using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetTripsByUsername(User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get All Trips:{ e.Message}");
                return BadRequest("Error occured");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var trip = _repository.GetTrip(id);

            if (trip == null)
            {
                return NotFound("Trip was not found in database");
            }

            return Ok(trip);
        }


        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(trip);
                newTrip.Username = User.Identity.Name;
                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
            }
            return BadRequest("Failed to save a trip");
        }

    }
}
