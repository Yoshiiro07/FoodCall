using System;
using System.Collections.Generic;
using FoodCall.Domain.Entities;

namespace FoodCall.Application.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderDto> Orders { get; set; }
        public int phone1 { get; set; }
        public int phone2 { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}