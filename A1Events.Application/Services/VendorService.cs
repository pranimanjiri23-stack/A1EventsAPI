using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;

namespace A1Events.Application.Services;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _vendorRepository;

    public VendorService(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<IEnumerable<VendorDto>> GetAllAsync()
    {
        var vendors = await _vendorRepository.GetAllAsync();

        return vendors.Select(MapToDto);
    }

    public async Task<VendorDto?> GetByIdAsync(int id)
    {
        var vendor = await _vendorRepository.GetByIdAsync(id);

        return vendor == null ? null : MapToDto(vendor);
    }

    public async Task<IEnumerable<PublicVendorDto>> GetPublishedAsync()
    {
        var vendors = await _vendorRepository.GetPublishedAsync();

        return vendors.Select(MapToPublicDto);
    }

    private static VendorDto MapToDto(
        A1Events.Domain.Entities.Vendor vendor)
    {
        return new VendorDto
        {
            Id = vendor.Id,
            BusinessName = vendor.BusinessName,
            ContactPerson = vendor.ContactPerson,
            Phone = vendor.Phone,
            Email = vendor.Email,
            Description = vendor.Description,
            LogoUrl = vendor.LogoUrl,
            WebsiteUrl = vendor.WebsiteUrl,
            InstagramUrl = vendor.InstagramUrl,
            CategoryId = vendor.CategoryId,
            IsPublished = vendor.IsPublished
        };
    }

    private static PublicVendorDto MapToPublicDto(
        A1Events.Domain.Entities.Vendor vendor)
    {
        return new PublicVendorDto
        {
            Id = vendor.Id,
            BusinessName = vendor.BusinessName,
            Description = vendor.Description,
            LogoUrl = vendor.LogoUrl,
            WebsiteUrl = vendor.WebsiteUrl,
            InstagramUrl = vendor.InstagramUrl,
            CategoryId = vendor.CategoryId
        };
    }

    public async Task<VendorDto> CreateAsync(CreateVendorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.BusinessName))
            throw new ArgumentException("Business name is required.");

        if (string.IsNullOrWhiteSpace(request.ContactPerson))
            throw new ArgumentException("Contact person is required.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.");

        if (request.CategoryId <= 0)
            throw new ArgumentException("A valid category is required.");

        var vendor = new A1Events.Domain.Entities.Vendor
        {
            BusinessName = request.BusinessName.Trim(),
            ContactPerson = request.ContactPerson.Trim(),
            Phone = request.Phone.Trim(),
            Email = request.Email.Trim(),
            Description = request.Description.Trim(),
            LogoUrl = request.LogoUrl?.Trim(),
            WebsiteUrl = request.WebsiteUrl?.Trim(),
            InstagramUrl = request.InstagramUrl?.Trim(),
            CategoryId = request.CategoryId,
            IsPublished = false,
            CreatedAt = DateTime.UtcNow
        };

        var createdVendor =
            await _vendorRepository.CreateAsync(vendor);

        return MapToDto(createdVendor);
    }
}