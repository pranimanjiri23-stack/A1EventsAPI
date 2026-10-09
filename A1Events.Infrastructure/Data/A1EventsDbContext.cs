using A1Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace A1Events.Infrastructure.Data;

public class A1EventsDbContext : DbContext
{
    public A1EventsDbContext(
        DbContextOptions<A1EventsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();

    public DbSet<Vendor> Vendors => Set<Vendor>();

    public DbSet<VendorApplication> VendorApplications
        => Set<VendorApplication>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<AdminUser> AdminUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(A1EventsDbContext).Assembly);
    }
}