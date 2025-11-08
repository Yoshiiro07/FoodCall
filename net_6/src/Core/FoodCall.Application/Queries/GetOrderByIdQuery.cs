using System;
using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto?>
    {
        public Guid OrderId { get; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}