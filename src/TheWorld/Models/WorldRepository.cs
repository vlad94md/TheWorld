using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models.Context;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAlTrips()
        {
            _logger.LogInformation("Getting all trips from database");
            return _context.Trips.ToList();
        }

        public void AddTrip(Trip trip)
        {
            _context.Trips.Add(trip);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetTrip(int id)
        {
            return _context.Trips.FirstOrDefault(trip => trip.Id == id);
        }

        public Trip GetTripByName(string name)
        {
            return _context.Trips.Where(x => x.Name == name).Include(x => x.Stops).FirstOrDefault();
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var trip = GetTripByName(tripName);

            if (trip != null)
            {
                _context.Stops.Add(newStop);
                trip.Stops.Add(newStop);
            }
        }
    }
}
