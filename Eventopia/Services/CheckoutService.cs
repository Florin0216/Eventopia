using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Eventopia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public CheckoutService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task<Checkout> CreateCheckout(Checkout checkout, int eventId, string location, string userId)
    {
        checkout.EventId = eventId;
        checkout.Location = location;
        checkout.CheckoutDate = DateTime.UtcNow;
        checkout.UserId = userId;
        
        _repositoryWrapper.Checkout.Create(checkout);
        await _repositoryWrapper.SaveAsync();

        return checkout;
    }

    public async Task<Checkout?> FindCheckoutById(int id)
    {
        return await _repositoryWrapper.Checkout
            .FindByCondition(c => c.Id == id)
            .Include(c => c.Event)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Checkout>> FindCheckoutsByUserId(string userId)
    {
        return await _repositoryWrapper.Checkout
            .FindByCondition(c => c.UserId == userId)
            .Include(c => c.Event)
            .ThenInclude(e => e.Venue)
            .ToListAsync();
    }

    public async Task<IEnumerable<Checkout>> FindCheckoutsByEventId(int eventId)
    {
        return await _repositoryWrapper.Checkout.FindByCondition(c => c.EventId == eventId).ToListAsync();
    }

    
}