using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class CheckoutTicketRepository : RepositoryBase<CheckoutTicket>, ICheckoutTicketRepository
{
    public CheckoutTicketRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}