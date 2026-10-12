using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;
using A1Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace A1Events.Infrastructure.Repositories;

public class VendorRepository : IVendorRepository
{
    private readonly A1EventsDbContext _context;

    public VendorRepository(A1EventsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vendor>> GetAllAsync()
    {
        return await _context.Vendors
            .FromSqlRaw("EXEC dbo.usp_Vendor_GetAll")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vendor?> GetByIdAsync(int id)
    {
        var result = await _context.Vendors
            .FromSqlInterpolated(
                $"EXEC dbo.usp_Vendor_GetById @Id={id}")
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<List<Vendor>> GetPublishedAsync()
    {
        return await _context.Vendors
            .FromSqlRaw("EXEC dbo.usp_Vendor_GetPublished")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vendor> CreateAsync(Vendor vendor)
    {
        var result = await _context.Vendors
            .FromSqlInterpolated($"""
            EXEC dbo.usp_Vendor_Create
                @BusinessName={vendor.BusinessName},
                @ContactPerson={vendor.ContactPerson},
                @Phone={vendor.Phone},
                @Email={vendor.Email},
                @Description={vendor.Description},
                @LogoUrl={vendor.LogoUrl},
                @WebsiteUrl={vendor.WebsiteUrl},
                @InstagramUrl={vendor.InstagramUrl},
                @CategoryId={vendor.CategoryId}
            """)
            .AsNoTracking()
            .ToListAsync();

        return result.Single();
    }


    public async Task<Vendor?> UpdateAsync(Vendor vendor)
    {
        var vendors = await _context.Vendors
            .FromSqlInterpolated($"""
            EXEC dbo.usp_Vendor_Update
                @Id={vendor.Id},
                @BusinessName={vendor.BusinessName},
                @ContactPerson={vendor.ContactPerson},
                @Phone={vendor.Phone},
                @Email={vendor.Email},
                @Description={vendor.Description},
                @LogoUrl={vendor.LogoUrl},
                @WebsiteUrl={vendor.WebsiteUrl},
                @InstagramUrl={vendor.InstagramUrl},
                @CategoryId={vendor.CategoryId}
            """)
            .AsNoTracking()
            .ToListAsync();

        return vendors.FirstOrDefault();
    }


    public async Task<Vendor?> PublishAsync(int id)
    {
        var vendors = await _context.Vendors
            .FromSqlInterpolated(
                $"EXEC dbo.usp_Vendor_SetPublished @Id={id}, @IsPublished={true}")
            .AsNoTracking()
            .ToListAsync();

        return vendors.FirstOrDefault();
    }

    public async Task<Vendor?> UnpublishAsync(int id)
    {
        var vendors = await _context.Vendors
            .FromSqlInterpolated(
                $"EXEC dbo.usp_Vendor_SetPublished @Id={id}, @IsPublished={false}")
            .AsNoTracking()
            .ToListAsync();

        return vendors.FirstOrDefault();
    }


}