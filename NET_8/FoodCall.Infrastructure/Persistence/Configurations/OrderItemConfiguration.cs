using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProductId)
            .IsRequired();

        builder.Property(e => e.ProductName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.UnitPrice)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(e => e.Quantity)
            .IsRequired();

        // SubTotal é calculado, não persiste no banco
        builder.Ignore(e => e.SubTotal);

        builder.HasIndex(e => e.ProductId);
    }
}