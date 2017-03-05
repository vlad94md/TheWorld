using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetTripsByUsername(string name);
        void AddTrip(Trip trip);
        Task<bool> SaveChangesAsync();
        Trip GetTrip(int id);
        Trip GetTripByName(string name);
        Trip GetUserTripByName(string name, string username);
        void AddStop(string tripName, Stop newStop, string username);
    }
}