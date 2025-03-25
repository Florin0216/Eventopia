using Eventopia.Data;
using Eventopia.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class EventService
{
    private readonly EventopiaDbContext _context;

    public EventService(EventopiaDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> Create(Event model, string date, string time, string userId)
    {
        model.Date = DateOnly.Parse(date);
        model.Time = TimeOnly.Parse(time);
        
        _context.Events.Add(model);
        await _context.SaveChangesAsync();

        var eventUser = new EventUser
        {
            userId = userId,
            EventId = model.Id
        };
        
        _context.EventUsers.Add(eventUser);
        await _context.SaveChangesAsync();
        
        return model.Id;
    }

    public async Task Update(int id, Event model,string date, string time)
    {
        var oldEvent = await _context.Events.FindAsync(id);
        if (oldEvent != null)
        {
            oldEvent.Date = DateOnly.Parse(date);
            oldEvent.Time = TimeOnly.Parse(time);
            oldEvent.Name = model.Name;
            oldEvent.Description = model.Description;
            oldEvent.PhotoPath = model.PhotoPath;
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task Delete(Event eventItem)
    { 
        var tickets = _context.Tickets.Where(x => x.EventId == eventItem.Id).ToList();
        _context.Tickets.RemoveRange(tickets);
        _context.Events.Remove(eventItem);
        
        await _context.SaveChangesAsync();
    }

    public async Task<List<Event>> GetAllEvents()
    {
        return await _context.Events.ToListAsync();
    }
    
    public async Task<List<Event>> GetEventsByOrganizer(string organizerId)
    {
        return await _context.Events
            .Where(e => _context.EventUsers.Any(eu => eu.EventId == e.Id && eu.userId == organizerId))
            .ToListAsync();
    }

    public async Task<Event> GetEventById(int id)
    {
        return await _context.Events.FindAsync(id);
    }
    
    
}