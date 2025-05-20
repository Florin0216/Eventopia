using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Eventopia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class EventService : IEventService
{
    private IRepositoryWrapper _repositoryWrapper;

    public EventService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }
    
    public async Task<int> CreateEvent(Event model, string date, string time, string userId)
    {
        model.Date = DateOnly.Parse(date);
        model.Time = TimeOnly.Parse(time);
        model.Status = "Upcoming";
        
        _repositoryWrapper.Event.Create(model);
        await _repositoryWrapper.SaveAsync();

        var eventUser = new EventUser
        {
            userId = userId,
            EventId = model.Id
        };
        
        _repositoryWrapper.EventUser.Create(eventUser);
        await _repositoryWrapper.SaveAsync();
        
        return model.Id;
    }

    public async Task UpdateEvent(int id, Event model,string date, string time)
    {
        var oldEvent = await _repositoryWrapper.Event.GetByIdAsync(id);
        if (oldEvent != null)
        {
            oldEvent.Date = DateOnly.Parse(date);
            oldEvent.Time = TimeOnly.Parse(time);
            oldEvent.Name = model.Name;
            oldEvent.Description = model.Description;
            oldEvent.PhotoPath = model.PhotoPath;
            await _repositoryWrapper.SaveAsync();
        }
    }
    
    public async Task DeleteEvent(Event? eventItem)
    { 
        var tickets = await _repositoryWrapper.Ticket
            .FindByCondition(x => x.EventId == eventItem.Id)
            .ToListAsync();
            
        foreach (var ticket in tickets)
        {
            _repositoryWrapper.Ticket.Delete(ticket);
        }
        
        _repositoryWrapper.Event.Delete(eventItem);
        
        await _repositoryWrapper.SaveAsync();
    }

    public async Task<IEnumerable<Event>> GetAllEvents()
    {
        return await _repositoryWrapper.Event.FindAll().ToListAsync();
    }
    
    public async Task<List<Event>> GetEventsByOrganizer(string organizerId)
    {
        return await _repositoryWrapper.Event.FindAll()
            .Where(e => _repositoryWrapper.EventUser.FindAll()
                .Any(eu => eu.EventId == e.Id && eu.userId == organizerId))
            .ToListAsync();
    }

    public async Task<Event?> GetEventById(int id)
    {
        return await _repositoryWrapper.Event
            .FindByCondition(e => e.Id == id)
            .Include(e => e.Venue)
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync();
    }
    
    public Task<IEnumerable<Event>> FilterEventsByDateAsync(IEnumerable<Event> events, string dateFilter)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        IEnumerable<Event> filteredEvents;

        switch (dateFilter.ToLower())
        {
            case "today":
                filteredEvents = events.Where(e => e.Date == today).ToList();
                break;
                
            case "this-week":
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                var endOfWeek = startOfWeek.AddDays(6);
                filteredEvents = events.Where(e => e.Date >= startOfWeek && e.Date <= endOfWeek).ToList();
                break;
                
            case "this-month":
                var startOfMonth = new DateOnly(today.Year, today.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                filteredEvents = events.Where(e => e.Date >= startOfMonth && e.Date <= endOfMonth).ToList();
                break;
                
            default:
                filteredEvents = events;
                break;
        }

        return Task.FromResult(filteredEvents);
    }
    
    public async Task<Dictionary<int, (decimal MinPrice, decimal MaxPrice)>> GetEventsPriceRangesAsync(IEnumerable<Event> events)
    {
        var eventIds = events.Select(e => e.Id).ToList();

        var tickets = await _repositoryWrapper.Ticket
            .FindByCondition(t => t.EventId.HasValue && eventIds.Contains(t.EventId.Value))
            .ToListAsync();

        var priceRanges = tickets
            .GroupBy(t => t.EventId.Value) 
            .ToDictionary(
                g => g.Key,
                g => (
                    MinPrice: g.Min(t => t.Price),
                    MaxPrice: g.Max(t => t.Price)
                )
            );

        return priceRanges;
    }
    
}