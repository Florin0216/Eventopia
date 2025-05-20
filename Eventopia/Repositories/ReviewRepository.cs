using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
    public ReviewRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}