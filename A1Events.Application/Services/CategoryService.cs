
using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;

namespace A1Events.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return categories.Select(MapToDto).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid category ID.");

        var category = await _repository.GetByIdAsync(id);

        return category == null ? null : MapToDto(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetActiveAsync()
    {
        var categories = await _repository.GetActiveAsync();

        return categories.Select(MapToDto).ToList();
    }

    public async Task<CategoryDto> CreateAsync(
        CreateCategoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var name = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.");

        if (name.Length < 2 || name.Length > 100)
            throw new ArgumentException(
                "Category name must be between 2 and 100 characters.");

        if (request.Description?.Length > 500)
            throw new ArgumentException(
                "Category description cannot exceed 500 characters.");

        var category = new Category
        {
            Name = name,
            Description = request.Description?.Trim(),
            IsActive = true
        };

        var createdCategory = await _repository.CreateAsync(category);

        return MapToDto(createdCategory);
    }

    public async Task<CategoryDto?> UpdateAsync(
        int id,
        UpdateCategoryRequest request)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid category ID.");

        ArgumentNullException.ThrowIfNull(request);

        var name = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.");

        if (name.Length < 2 || name.Length > 100)
            throw new ArgumentException(
                "Category name must be between 2 and 100 characters.");

        if (request.Description?.Length > 500)
            throw new ArgumentException(
                "Category description cannot exceed 500 characters.");

        var existingCategory = await _repository.GetByIdAsync(id);

        if (existingCategory == null)
            return null;

        existingCategory.Name = name;
        existingCategory.Description = request.Description?.Trim();
        existingCategory.IsActive = request.IsActive;

        var updatedCategory =
            await _repository.UpdateAsync(existingCategory);

        return updatedCategory == null ? null : MapToDto(updatedCategory);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid category ID.");

        var existingCategory = await _repository.GetByIdAsync(id);

        if (existingCategory == null)
            return false;

        return await _repository.DeleteAsync(id);
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt
        };
    }
}
