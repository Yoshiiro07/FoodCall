using FoodCall.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCall.Infrastructure.Persistence;

public class FoodCallDbContext : DbContext
{
    public FoodCallDbContext(DbContextOptions<FoodCallDbContext> options) 
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodCallDbContext).Assembly);
    }
}