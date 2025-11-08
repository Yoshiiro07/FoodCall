using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodCall.Application.DTOs;
using FoodCall.Application.Queries;
using FoodCall.Domain.Interfaces;
using MediatR;

namespace FoodCall.Application.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            
            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                DeliveryAddress = order.DeliveryAddress,
                Status = order.Status,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                TotalAmount = order.TotalAmount,
                DeliveryFee = order.DeliveryFee,
                Notes = order.Notes,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice,
                    Observations = i.Observations
                }).ToList()
            };
        }
    }
}