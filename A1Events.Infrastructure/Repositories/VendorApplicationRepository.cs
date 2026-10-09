using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;
using A1Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace A1Events.Infrastructure.Repositories;

public class VendorApplicationRepository : IVendorApplicationRepository
{
    private readonly A1EventsDbContext _context;

    public VendorApplicationRepository(A1EventsDbContext context)
    {
        _context = context;
    }

    public async Task<VendorApplication> CreateAsync(
        VendorApplication application)
    {
        var result = await _context.VendorApplications
            .FromSqlInterpolated($"""
                EXEC dbo.usp_VendorApplication_Create
                    @VendorId={application.VendorId},
                    @EventId={application.EventId}
                """)
            .AsNoTracking()
            .ToListAsync();

        return result.Single();
    }

    public async Task<VendorApplication?> GetByIdAsync(int id)
    {
        var result = await _context.VendorApplications
            .FromSqlInterpolated(
                $"EXEC dbo.usp_VendorApplication_GetById @Id={id}")
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<List<VendorApplication>> GetAllAsync()
    {
        return await _context.VendorApplications
            .FromSqlRaw(
                "EXEC dbo.usp_VendorApplication_GetAll")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<VendorApplication>> GetByVendorAsync(
        int vendorId)
    {
        return await _context.VendorApplications
            .FromSqlInterpolated(
                $"EXEC dbo.usp_VendorApplication_GetByVendor @VendorId={vendorId}")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<VendorApplication>> GetByEventAsync(
        int eventId)
    {
        return await _context.VendorApplications
            .FromSqlInterpolated(
                $"EXEC dbo.usp_VendorApplication_GetByEvent @EventId={eventId}")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<VendorApplication?> ApproveAsync(
        int id,
        string? adminRemarks)
    {
        var result = await _context.VendorApplications
            .FromSqlInterpolated($"""
                EXEC dbo.usp_VendorApplication_Approve
                    @Id={id},
                    @AdminRemarks={adminRemarks}
                """)
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<VendorApplication?> RejectAsync(
        int id,
        string? adminRemarks)
    {
        var result = await _context.VendorApplications
            .FromSqlInterpolated($"""
                EXEC dbo.usp_VendorApplication_Reject
                    @Id={id},
                    @AdminRemarks={adminRemarks}
                """)
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }
}