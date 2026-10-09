using A1Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace A1Events.Infrastructure.Data.Configurations;

public class VendorApplicationConfiguration
    : IEntityTypeConfiguration<VendorApplication>
{
    public void Configure(
        EntityTypeBuilder<VendorApplication> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AdminRemarks)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Event)
            .WithMany(x => x.VendorApplications)
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.VendorId,
            x.EventId
        }).IsUnique();
    }
}