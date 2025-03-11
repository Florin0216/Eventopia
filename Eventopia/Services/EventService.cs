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
    
    public async Task CreateEvent(Event model, string date, string time)
    {
        model.date = DateOnly.Parse(date);
        model.time = TimeOnly.Parse(time);
        
        _context.Events.Add(model);
        await _context.SaveChangesAsync();
        
    }

    public async Task<List<Event>> GetAllEvents()
    {
        return await _context.Events.ToListAsync();
    }
}