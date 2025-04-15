using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(RepositoryContext context) : base(context){}
}