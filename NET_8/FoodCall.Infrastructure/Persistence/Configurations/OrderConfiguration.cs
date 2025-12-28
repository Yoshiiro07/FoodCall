using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.Property(e => e.RestaurantId)
            .IsRequired();

        builder.Property(e => e.TotalValue)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasIndex(e => e.CustomerId);
        builder.HasIndex(e => e.RestaurantId);
        builder.HasIndex(e => e.Status);

        // Relacionamento com OrderItems
        builder.HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}