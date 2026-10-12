using A1Events.Application.DTOs;

namespace A1Events.Application.Interfaces;

public interface IVendorService
{
    Task<IEnumerable<VendorDto>> GetAllAsync();
    Task<VendorDto?> GetByIdAsync(int id);
    Task<IEnumerable<PublicVendorDto>> GetPublishedAsync();
    Task<VendorDto> CreateAsync(CreateVendorRequest request);

    Task<VendorDto?> UpdateAsync(int id, UpdateVendorRequest request);

    Task<VendorDto?> PublishAsync(int id);

    Task<VendorDto?> UnpublishAsync(int id);


}