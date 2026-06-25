// src/Infrastructure/Context/AppDbContext.cs
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext : DbContext
{
    // ⚠️ O construtor DEVE ser assim, recebendo DbContextOptions<AppDbContext>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }
}