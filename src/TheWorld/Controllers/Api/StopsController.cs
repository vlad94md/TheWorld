using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopsController> _logger;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        

        [HttpGet()]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
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
                    _repository.AddStop(tripName, newStop);

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
