using A1Events.Application.DTOs;

namespace A1Events.Application.Interfaces;

public interface IAdminService
{
    Task<AdminUserDto> CreateAsync(CreateAdminUserRequest request);
}