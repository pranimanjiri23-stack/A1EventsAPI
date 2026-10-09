using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace A1Events.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAdminUserRepository _adminUserRepository;
    private readonly IPasswordHasher<AdminUser> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(
        IAdminUserRepository adminUserRepository,
        IPasswordHasher<AdminUser> passwordHasher,
        IConfiguration configuration)
    {
        _adminUserRepository = adminUserRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var username = request.Username.Trim();

        var adminUser =
            await _adminUserRepository.GetByUsernameAsync(username);

        if (adminUser == null)
            return null;

        if (!adminUser.IsActive)
            return null;

        var passwordResult =
            _passwordHasher.VerifyHashedPassword(
                adminUser,
                adminUser.PasswordHash,
                request.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
            return null;

        var token = GenerateToken(adminUser);

        return new LoginResponse
        {
            Token = token,
            Username = adminUser.Username,
            Role = adminUser.Role
        };
    }

    private string GenerateToken(AdminUser adminUser)
    {
        var key = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException(
                "JWT key is not configured.");

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var expirationMinutes =
            _configuration.GetValue<int>("Jwt:ExpirationMinutes");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, adminUser.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, adminUser.Username),
            new(ClaimTypes.Name, adminUser.Username),
            new(ClaimTypes.Role, adminUser.Role)
        };

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}