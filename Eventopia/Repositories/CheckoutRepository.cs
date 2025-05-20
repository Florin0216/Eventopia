using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class CheckoutRepository : RepositoryBase<Checkout> , ICheckoutRepository
{
    public CheckoutRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
    
}