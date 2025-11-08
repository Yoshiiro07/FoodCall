using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FoodCall.Domain.Entities;

namespace FoodCall.Infrastructure.Data.Context
{
    public class FoodCallDbContext : DbContext
    {

        public FoodCallDbContext(DbContextOptions<FoodCallDbContext> options) : base(options) { }
        
       public DbSet<Order> Orders { get; set; }
    }
}