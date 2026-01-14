using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCall.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.Property(e => e.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        // Configuração do Value Object Address como Owned Entity
        builder.OwnsMany(e => e.Addresses, address =>
        {
            address.ToTable("Address");
            
            address.WithOwner().HasForeignKey("UserId");
            address.Property<int>("Id");
            address.HasKey("UserId", "Id");
            
            address.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200);

            address.Property(a => a.Number)
                .IsRequired()
                .HasMaxLength(10);

            address.Property(a => a.Neighborhood)
                .IsRequired()
                .HasMaxLength(100);

            address.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            address.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(10);
        });
    }
}