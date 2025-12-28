using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.OrderId)
            .IsRequired();

        builder.HasIndex(e => e.OrderId)
            .IsUnique();

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.HasIndex(e => e.CustomerId);

        builder.Property(e => e.RestaurantRating)
            .IsRequired();

        builder.Property(e => e.RestaurantComment)
            .HasMaxLength(500);

        builder.Property(e => e.CourierRating);

        builder.Property(e => e.CourierComment)
            .HasMaxLength(500);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.HasIndex(e => e.CreatedAt);
    }
}