using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class EventUserRepository : RepositoryBase<EventUser>, IEventUserRepository
{
    public EventUserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}