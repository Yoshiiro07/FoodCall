using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Document)
            .IsRequired()
            .HasMaxLength(18);

        builder.HasIndex(e => e.Document)
            .IsUnique();

        builder.Property(e => e.IsActive)
            .IsRequired();

        // Relacionamento com Products
        builder.HasMany(e => e.Menu)
            .WithOne()
            .HasForeignKey(p => p.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}