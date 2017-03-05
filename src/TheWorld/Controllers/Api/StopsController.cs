using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    [Authorize]
    public class StopsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopsController> _logger;
        private GeoCoordsService _coordsService;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger, GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }
        

        [HttpGet()]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetUserTripByName(tripName, User.Identity.Name);
                var tripStops = trip.Stops.OrderBy(s => s.Order).ToList();

                return Ok(AutoMapper.Mapper.Map<IEnumerable<StopViewModel>>(tripStops));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get stops:{e.Message}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel stopModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = AutoMapper.Mapper.Map<Stop>(stopModel);
                    
                    // Lookup for Geocords
                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }

                    newStop.Latitude = result.Latitude;
                    newStop.Longitude = result.Longitude;

                    _repository.AddStop(tripName, newStop, User.Identity.Name);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.Name}", AutoMapper.Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save a new stop:{e.Message}");
            }

            return BadRequest();
        }
    }
}
