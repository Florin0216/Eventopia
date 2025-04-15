using System.Linq.Expressions;

namespace Eventopia.Repositories.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    IQueryable<T> FindAll();
}
