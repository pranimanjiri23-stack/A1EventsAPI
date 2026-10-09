namespace A1Events.Application.DTOs;

public class PublicVendorDto
{
    public int Id { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public int CategoryId { get; set; }
}