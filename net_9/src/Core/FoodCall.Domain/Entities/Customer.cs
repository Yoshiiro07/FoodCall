using System;
using System.Collections.Generic;
using FoodCall.Domain.Entities;

namespace Foodcall.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public int Phone1 { get; set; }
        public int Phone2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Order> Orders { get; set; }
    }
}