
using System.ComponentModel.DataAnnotations;

namespace A1Events.Application.DTOs;

public class UpdateVendorRequest
{
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string BusinessName { get; set; } = string.Empty;

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string ContactPerson { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Url]
    [StringLength(500)]
    public string? LogoUrl { get; set; }

    [Url]
    [StringLength(500)]
    public string? WebsiteUrl { get; set; }

    [Url]
    [StringLength(500)]
    public string? InstagramUrl { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}
