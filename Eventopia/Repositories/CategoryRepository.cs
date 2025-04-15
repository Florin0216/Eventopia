using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;

namespace Eventopia.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}