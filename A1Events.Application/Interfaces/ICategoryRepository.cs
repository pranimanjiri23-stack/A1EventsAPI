
using A1Events.Domain.Entities;

namespace A1Events.Application.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(int id);

    Task<List<Category>> GetActiveAsync();

    Task<Category> CreateAsync(Category category);

    Task<Category?> UpdateAsync(Category category);

    Task<bool> DeleteAsync(int id);
}
