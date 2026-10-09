namespace A1Events.Application.DTOs;

public class VendorApplicationDto
{
    public int Id { get; set; }
    public int VendorId { get; set; }
    public int EventId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? AdminRemarks { get; set; }
    public DateTime AppliedAt { get; set; }
}