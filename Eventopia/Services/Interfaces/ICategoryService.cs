using Eventopia.Models;

namespace Eventopia.Services.Interfaces;

public interface ICategoryService
{
    Task CreateCategory(Category category);

    Task<IEnumerable<Category>> GetAllCategories();

    Task<IEnumerable<Event>> GetEventsByCategoryAsync(string categoryName);
}