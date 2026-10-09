namespace A1Events.Application.DTOs;

public class EventDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string Location { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string Status { get; set; } = string.Empty;
}