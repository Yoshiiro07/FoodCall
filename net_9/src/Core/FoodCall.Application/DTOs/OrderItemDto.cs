using System;
using System.Collections.Generic;
using FoodCall.Application.DTOs;

namespace FoodCall.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}