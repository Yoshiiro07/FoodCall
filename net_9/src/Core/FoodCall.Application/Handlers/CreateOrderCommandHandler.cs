using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodCall.Application.Commands;
using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Interfaces;
using MediatR;

namespace FoodCall.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderData = request.OrderData;
            
            // Create domain entity
            var order = new Order(
                orderData.CustomerId,
                orderData.CustomerName,
                orderData.DeliveryAddress,
                orderData.Notes
            );

            // Add items to order
            foreach (var itemDto in orderData.Items)
            {
                order.AddItem(
                    itemDto.ProductId,
                    itemDto.ProductName,
                    itemDto.UnitPrice,
                    itemDto.Quantity,
                    itemDto.Observations
                );
            }

            // Save to repository
            var savedOrder = await _orderRepository.AddAsync(order);

            // Map to DTO
            return new OrderDto
            {
                Id = savedOrder.Id,
                CustomerId = savedOrder.CustomerId,
                CustomerName = savedOrder.CustomerName,
                DeliveryAddress = savedOrder.DeliveryAddress,
                Status = savedOrder.Status,
                OrderDate = savedOrder.OrderDate,
                DeliveryDate = savedOrder.DeliveryDate,
                TotalAmount = savedOrder.TotalAmount,
                DeliveryFee = savedOrder.DeliveryFee,
                Notes = savedOrder.Notes,
                Items = savedOrder.Items.Select(i => new OrderItemDto
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