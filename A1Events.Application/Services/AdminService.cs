using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace A1Events.Application.Services;

public class AdminService : IAdminService
{
    private readonly IAdminUserRepository _adminUserRepository;
    private readonly IPasswordHasher<AdminUser> _passwordHasher;

    public AdminService(
        IAdminUserRepository adminUserRepository,
        IPasswordHasher<AdminUser> passwordHasher)
    {
        _adminUserRepository = adminUserRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AdminUserDto> CreateAsync(
        CreateAdminUserRequest request)
    {
        var adminUser = new AdminUser
        {
            Username = request.Username.Trim(),
            Role = "Admin",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        adminUser.PasswordHash =
            _passwordHasher.HashPassword(
                adminUser,
                request.Password);

        var createdAdmin =
            await _adminUserRepository.CreateAsync(adminUser);

        return new AdminUserDto
        {
            Id = createdAdmin.Id,
            Username = createdAdmin.Username,
            Role = createdAdmin.Role,
            IsActive = createdAdmin.IsActive,
            CreatedAt = createdAdmin.CreatedAt
        };
    }
}