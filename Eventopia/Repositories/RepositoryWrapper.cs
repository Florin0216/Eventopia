using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly RepositoryContext _repositoryContext;
    private IEventRepository _event;
    private ITicketRepository _ticket;
    private IEventUserRepository _eventUser;
    private ICategoryRepository _category;
    private IUserRepository _user;
    private IVenueRepository _venue;
    private ICheckoutRepository _checkout;
    private ICheckoutTicketRepository _checkoutTicket;
    private IReviewRepository _review;
    
    public RepositoryWrapper(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
    
    public IEventRepository Event => _event ??= new EventRepository(_repositoryContext);

    public ITicketRepository Ticket => _ticket ??= new TicketRepository(_repositoryContext);

    public IEventUserRepository EventUser => _eventUser ??= new EventUserRepository(_repositoryContext);
    public ICategoryRepository Category => _category ??= new CategoryRepository(_repositoryContext);
    public IUserRepository User => _user ??= new UserRepository(_repositoryContext);
    public IVenueRepository Venue => _venue ??= new VenueRepository(_repositoryContext);
    public ICheckoutRepository Checkout => _checkout ??= new CheckoutRepository(_repositoryContext);
    public ICheckoutTicketRepository CheckoutTicket => _checkoutTicket ??= new CheckoutTicketRepository(_repositoryContext);
    public IReviewRepository Review => _review ??= new ReviewRepository(_repositoryContext);

    public async Task SaveAsync()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}