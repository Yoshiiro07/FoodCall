using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations;

public class CourierConfiguration : IEntityTypeConfiguration<Courier>
{
    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.VehiclePlate)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(e => e.VehiclePlate)
            .IsUnique();

        builder.Property(e => e.IsAvailable)
            .IsRequired();

        builder.HasIndex(e => e.IsAvailable);
    }
}