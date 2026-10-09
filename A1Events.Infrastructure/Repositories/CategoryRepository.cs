using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;
using A1Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace A1Events.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly A1EventsDbContext _context;

    public CategoryRepository(A1EventsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .FromSqlRaw("EXEC dbo.usp_Category_GetAll")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        var categories = await _context.Categories
            .FromSqlInterpolated(
                $"EXEC dbo.usp_Category_GetById @Id={id}")
            .AsNoTracking()
            .ToListAsync();

        return categories.FirstOrDefault();
    }

    public async Task<List<Category>> GetActiveAsync()
    {
        return await _context.Categories
            .FromSqlRaw("EXEC dbo.usp_Category_GetActive")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category> CreateAsync(Category category)
    {
        var categories = await _context.Categories
            .FromSqlInterpolated($"""
                EXEC dbo.usp_Category_Create
                    @Name={category.Name},
                    @Description={category.Description}
                """)
            .AsNoTracking()
            .ToListAsync();

        return categories.Single();
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var categories = await _context.Categories
            .FromSqlInterpolated($"""
                EXEC dbo.usp_Category_Update
                    @Id={category.Id},
                    @Name={category.Name},
                    @Description={category.Description},
                    @IsActive={category.IsActive}
                """)
            .AsNoTracking()
            .ToListAsync();

        return categories.FirstOrDefault();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC dbo.usp_Category_Delete @Id={id}");

        // ExecuteSqlInterpolatedAsync returns rows affected,
        // not the Stored Procedure's SELECT result.
        // Verify deletion separately.
        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return category == null;
    }
}
