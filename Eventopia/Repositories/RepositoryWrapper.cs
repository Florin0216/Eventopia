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
    
    public RepositoryWrapper(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
    
    public IEventRepository Event => _event ??= new EventRepository(_repositoryContext);

    public ITicketRepository Ticket => _ticket ??= new TicketRepository(_repositoryContext);

    public IEventUserRepository EventUser => _eventUser ??= new EventUserRepository(_repositoryContext);
    public ICategoryRepository Category => _category ??= new CategoryRepository(_repositoryContext);

    public async Task SaveAsync()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}