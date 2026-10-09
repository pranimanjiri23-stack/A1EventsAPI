using System.ComponentModel.DataAnnotations;

namespace A1Events.Application.DTOs;

public class ReviewVendorApplicationRequest
{
    [StringLength(1000)]
    public string? AdminRemarks { get; set; }
}