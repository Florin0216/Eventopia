using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Eventopia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class VenueService : IVenueService
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public VenueService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task CreateVenue(Venue venue)
    {
        _repositoryWrapper.Venue.Create(venue);
        await _repositoryWrapper.SaveAsync();
    }
    
    public Task<IEnumerable<Venue>> GetAllVenues()
    {
        return _repositoryWrapper.Venue.GetAllAsync();
    } 
    
    public async Task<IEnumerable<Event>> GetEventsByVenueAsync(string venueName)
    {

        return await _repositoryWrapper.Event
            .FindByCondition(e => e.Venue != null && e.Venue.Name == venueName) 
            .ToListAsync();
    }
}