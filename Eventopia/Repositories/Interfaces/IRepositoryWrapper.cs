namespace Eventopia.Repositories.Interfaces;

public interface IRepositoryWrapper
{
     IEventRepository Event { get; }
     ITicketRepository Ticket { get; }
     IEventUserRepository EventUser { get; }
     ICategoryRepository Category { get; }
     IUserRepository User { get; }
     IVenueRepository Venue { get; }
     ICheckoutRepository Checkout { get; }
     ICheckoutTicketRepository CheckoutTicket { get; }
     IReviewRepository Review { get; }
     Task SaveAsync();
}