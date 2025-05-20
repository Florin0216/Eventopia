using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Eventopia.Repositories;

public class UserRepository : RepositoryBase<Users>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext){}
}