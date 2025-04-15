using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
{
    public TicketRepository(RepositoryContext context) : base(context){}
}