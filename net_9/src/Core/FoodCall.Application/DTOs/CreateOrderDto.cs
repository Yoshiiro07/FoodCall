using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FoodCall.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one order item is required.")]
        public List<CreateOrderItemDto> Items { get; set; }
    }
}