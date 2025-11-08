using System;
using System.Collections.Generic;
using Foodcall.Domain.Entities;

namespace FoodCall.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}