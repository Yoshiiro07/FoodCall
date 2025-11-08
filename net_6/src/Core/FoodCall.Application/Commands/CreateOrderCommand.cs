using System;
using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using MediatR;

namespace FoodCall.Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public CreateOrderDto OrderData { get; }

        public CreateOrderCommand(CreateOrderDto orderData)
        {
            OrderData = orderData ?? throw new ArgumentNullException(nameof(orderData));
        }
    }
}