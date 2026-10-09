using A1Events.Domain.Entities;

namespace A1Events.Application.Interfaces;

public interface IVendorApplicationRepository
{
    Task<VendorApplication> CreateAsync(VendorApplication application);

    Task<VendorApplication?> GetByIdAsync(int id);

    Task<List<VendorApplication>> GetAllAsync();

    Task<List<VendorApplication>> GetByVendorAsync(int vendorId);

    Task<List<VendorApplication>> GetByEventAsync(int eventId);

    Task<VendorApplication?> ApproveAsync(
        int id,
        string? adminRemarks);

    Task<VendorApplication?> RejectAsync(
        int id,
        string? adminRemarks);
}