using System;
using System.Collections.Generic;
using FoodCall.Application.DTOs;

namespace FoodCall.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Observations { get; set; }
    }
}