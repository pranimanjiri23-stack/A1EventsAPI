using System.ComponentModel.DataAnnotations;

namespace A1Events.Application.DTOs;

public class UpdateEventRequest
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(2000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime EventDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    [Required]
    [StringLength(300)]
    public string Location { get; set; } = string.Empty;

    [Url]
    [StringLength(500)]
    public string? ImageUrl { get; set; }
}