using System.Linq.Expressions;
using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext { get; set; }

    public RepositoryBase(RepositoryContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
    }

    public void Create(T entity)
    {
        RepositoryContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        RepositoryContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        RepositoryContext.Set<T>().Remove(entity);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await RepositoryContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await RepositoryContext.Set<T>().ToListAsync();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return RepositoryContext.Set<T>().Where(expression);
    }

    public IQueryable<T> FindAll()
    {
        return RepositoryContext.Set<T>();
    }
}