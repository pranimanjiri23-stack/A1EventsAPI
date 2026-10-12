using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A1Events.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vendors = await _vendorService.GetAllAsync();

        return Ok(vendors);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vendor = await _vendorService.GetByIdAsync(id);

        if (vendor == null)
            return NotFound();

        return Ok(vendor);
    }

    [HttpGet("published")]
    public async Task<IActionResult> GetPublished()
    {
        var vendors = await _vendorService.GetPublishedAsync();

        return Ok(vendors);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateVendorRequest request)
    {
        try
        {
            var createdVendor =
                await _vendorService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdVendor.Id },
                createdVendor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }


    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateVendorRequest request)
    {
        try
        {
            var vendor = await _vendorService.UpdateAsync(id, request);

            if (vendor == null)
            {
                return NotFound(new
                {
                    message = "Vendor not found."
                });
            }

            return Ok(vendor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }


    [HttpPut("{id:int}/publish")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Publish(int id)
    {
        try
        {
            var vendor = await _vendorService.PublishAsync(id);

            if (vendor == null)
            {
                return NotFound(new
                {
                    message = "Vendor not found."
                });
            }

            return Ok(vendor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{id:int}/unpublish")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Unpublish(int id)
    {
        try
        {
            var vendor = await _vendorService.UnpublishAsync(id);

            if (vendor == null)
            {
                return NotFound(new
                {
                    message = "Vendor not found."
                });
            }

            return Ok(vendor);
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
