using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class VenueRepository : RepositoryBase<Venue>, IVenueRepository
{
    public VenueRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}
    