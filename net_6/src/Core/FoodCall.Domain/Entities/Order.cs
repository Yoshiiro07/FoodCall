using System;
using System.Collections.Generic;
using System.Linq;
using Foodcall.Domain.Entities;

namespace FoodCall.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public Order(Guid customerId, string customerName, string deliveryAddress, string notes) : this()
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CustomerName = customerName;
            DeliveryAddress = deliveryAddress;
            Notes = notes;
            Status = "Pending";
            OrderDate = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; private set; }
        public decimal DeliveryFee { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity, string observations)
        {
            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = Id,
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity,
                TotalPrice = unitPrice * quantity,
                Observations = observations
            };

            Items.Add(orderItem);
            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = Items.Sum(i => i.TotalPrice) + DeliveryFee;
        }
    }
}