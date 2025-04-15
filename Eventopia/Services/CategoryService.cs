using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Eventopia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public CategoryService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task CreateCategory(Category category)
    {
        _repositoryWrapper.Category.Create(category);
        await _repositoryWrapper.SaveAsync();
    }

    public Task<IEnumerable<Category>> GetAllCategories()
    {
        return _repositoryWrapper.Category.GetAllAsync();
    }
    
    public async Task<IEnumerable<Event>> GetEventsByCategoryAsync(string categoryName)
    {

        return await _repositoryWrapper.Event
            .FindByCondition(e => e.Category != null && e.Category.Name == categoryName) 
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> getAllCategories()
    {
        return await _repositoryWrapper.Category.FindAll().ToListAsync();
    }
}