using A1Events.Domain.Entities;

namespace A1Events.Application.Interfaces;

public interface IVendorRepository
{
    Task<List<Vendor>> GetAllAsync();
    Task<Vendor?> GetByIdAsync(int id);
    Task<List<Vendor>> GetPublishedAsync();

    Task<Vendor> CreateAsync(Vendor vendor);
}

