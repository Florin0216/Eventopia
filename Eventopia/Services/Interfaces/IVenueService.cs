using Eventopia.Models;

namespace Eventopia.Services.Interfaces;

public interface IVenueService
{
    Task CreateVenue(Venue venue);
    Task<IEnumerable<Venue>> GetAllVenues();
    Task<IEnumerable<Event>> GetEventsByVenueAsync(string venueName);
}