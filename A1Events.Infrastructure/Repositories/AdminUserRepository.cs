using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;
using A1Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace A1Events.Infrastructure.Repositories;

public class AdminUserRepository : IAdminUserRepository
{
    private readonly A1EventsDbContext _context;

    public AdminUserRepository(A1EventsDbContext context)
    {
        _context = context;
    }

    public async Task<AdminUser> CreateAsync(AdminUser adminUser)
    {
        var result = await _context.AdminUsers
            .FromSqlInterpolated($"""
                EXEC dbo.usp_AdminUser_Create
                    @Username={adminUser.Username},
                    @PasswordHash={adminUser.PasswordHash},
                    @Role={adminUser.Role}
                """)
            .AsNoTracking()
            .ToListAsync();

        return result.Single();
    }

    public async Task<AdminUser?> GetByUsernameAsync(string username)
    {
        var result = await _context.AdminUsers
            .FromSqlInterpolated($"""
            EXEC dbo.usp_AdminUser_GetByUsername
                @Username={username}
            """)
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }
}