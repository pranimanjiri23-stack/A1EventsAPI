using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A1Events.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorApplicationsController : ControllerBase
{
    private readonly IVendorApplicationService _service;

    public VendorApplicationsController(IVendorApplicationService service)
    {
        _service = service;
    }

    // Vendor submits an application
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVendorApplicationRequest request)
    {
        try
        {
            var result = await _service.CreateAsync(request);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // Admin gets application by ID
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new
                {
                    message = "Vendor application not found."
                });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // Admin gets all applications
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // Admin gets applications for a vendor
    [HttpGet("vendor/{vendorId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByVendor(int vendorId)
    {
        try
        {
            var result = await _service.GetByVendorAsync(vendorId);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // Admin gets applications for an event
    [HttpGet("event/{eventId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByEvent(int eventId)
    {
        try
        {
            var result = await _service.GetByEventAsync(eventId);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // Admin approves application
    [HttpPut("{id:int}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approve(
        int id,
        [FromBody] ReviewVendorApplicationRequest request)
    {
        try
        {
            var result = await _service.ApproveAsync(id, request);

            if (result == null)
                return NotFound(new
                {
                    message = "Vendor application not found."
                });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // Admin rejects application
    [HttpPut("{id:int}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Reject(
        int id,
        [FromBody] ReviewVendorApplicationRequest request)
    {
        try
        {
            var result = await _service.RejectAsync(id, request);

            if (result == null)
                return NotFound(new
                {
                    message = "Vendor application not found."
                });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}