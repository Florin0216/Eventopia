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
        return await _repositoryWrapper.Event.GetByIdAsync(id);
    }
    
}