using A1Events.Application.DTOs;

namespace A1Events.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}