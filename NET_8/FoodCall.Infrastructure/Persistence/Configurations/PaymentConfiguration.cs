using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.OrderId)
            .IsRequired();

        builder.HasIndex(e => e.OrderId)
            .IsUnique();

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(e => e.Method)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.IsPaid)
            .IsRequired();

        builder.Property(e => e.PaidAt);

        builder.HasIndex(e => e.IsPaid);
    }
}