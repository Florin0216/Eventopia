using Eventopia.Models;

namespace Eventopia.Services.Interfaces;

public interface IEventService
{
    Task<int> CreateEvent(Event model, string date, string time, string userId);
    Task UpdateEvent(int id, Event model, string date, string time);
    Task DeleteEvent(Event? eventItem);
    Task<Event?> GetEventById(int id);
    Task<IEnumerable<Event>> GetAllEvents();
    Task<List<Event>> GetEventsByOrganizer(string organizerId);
    Task<IEnumerable<Event>> FilterEventsByDateAsync(IEnumerable<Event> events, string dateFilter);
    Task<Dictionary<int, (decimal MinPrice, decimal MaxPrice)>> GetEventsPriceRangesAsync(IEnumerable<Event> events);
}