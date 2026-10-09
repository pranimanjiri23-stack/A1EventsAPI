using System.ComponentModel.DataAnnotations;

namespace A1Events.Application.DTOs;

public class CreateVendorApplicationRequest
{
    [Required]
    public int VendorId { get; set; }

    [Required]
    public int EventId { get; set; }
}