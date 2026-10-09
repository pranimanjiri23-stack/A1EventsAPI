using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace A1Events.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAdminUserRequest request)
    {
        try
        {
            var admin = await _adminService.CreateAsync(request);

            return Ok(admin);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}