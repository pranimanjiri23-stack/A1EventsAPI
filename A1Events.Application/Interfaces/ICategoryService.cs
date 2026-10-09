
using A1Events.Application.DTOs;

namespace A1Events.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();

    Task<CategoryDto?> GetByIdAsync(int id);

    Task<IEnumerable<CategoryDto>> GetActiveAsync();

    Task<CategoryDto> CreateAsync(CreateCategoryRequest request);

    Task<CategoryDto?> UpdateAsync(
        int id,
        UpdateCategoryRequest request);

    Task<bool> DeleteAsync(int id);
}
