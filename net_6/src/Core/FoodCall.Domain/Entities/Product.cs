using System;
using System.Collections.Generic;
using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}