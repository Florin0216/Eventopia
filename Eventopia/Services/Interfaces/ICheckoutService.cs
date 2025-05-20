using Eventopia.Models;

namespace Eventopia.Services.Interfaces;

public interface ICheckoutService
{
    Task<Checkout> CreateCheckout(Checkout checkout, int eventId, string location, string userId);
    Task<Checkout?> FindCheckoutById(int id);
    Task<IEnumerable<Checkout>> FindCheckoutsByUserId(string userId);
    Task<IEnumerable<Checkout>> FindCheckoutsByEventId(int eventId);
}