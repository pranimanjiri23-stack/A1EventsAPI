using A1Events.Application.DTOs;

namespace A1Events.Application.Interfaces;

public interface IVendorApplicationService
{
    Task<VendorApplicationDto> CreateAsync(
        CreateVendorApplicationRequest request);

    Task<VendorApplicationDto?> GetByIdAsync(int id);

    Task<IEnumerable<VendorApplicationDto>> GetAllAsync();

    Task<IEnumerable<VendorApplicationDto>> GetByVendorAsync(
        int vendorId);

    Task<IEnumerable<VendorApplicationDto>> GetByEventAsync(
        int eventId);

    Task<VendorApplicationDto?> ApproveAsync(
        int id,
        ReviewVendorApplicationRequest request);

    Task<VendorApplicationDto?> RejectAsync(
        int id,
        ReviewVendorApplicationRequest request);
}