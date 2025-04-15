namespace Eventopia.Repositories.Interfaces;

public interface IRepositoryWrapper
{
     IEventRepository Event { get; }
     ITicketRepository Ticket { get; }
     IEventUserRepository EventUser { get; }
     ICategoryRepository Category { get; }
     Task SaveAsync();
}