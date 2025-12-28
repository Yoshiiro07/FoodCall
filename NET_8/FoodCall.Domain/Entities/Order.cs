using FoodCall.Domain.Enums;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.ValueObjects;

namespace FoodCall.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid RestaurantId { get; private set; }
    public decimal TotalValue { get; private set; }
    public OrderStatus Status { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();

    public Order(Guid customerId, Guid restaurantId)
    {
        ValidateCustomerId(customerId);
        ValidateRestaurantId(restaurantId);

        Id = Guid.NewGuid();
        CustomerId = customerId;
        RestaurantId = restaurantId;
        Status = OrderStatus.Pending;
        TotalValue = 0;
    }

    public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        if (Status != OrderStatus.Pending)
            throw new BusinessRuleException("AddItem", "Só é possível adicionar itens em pedidos pendentes");

        var item = new OrderItem(productId, productName, unitPrice, quantity);
        Items.Add(item);
        RecalculateTotal();
    }

    public void RemoveItem(Guid itemId)
    {
        if (Status != OrderStatus.Pending)
            throw new BusinessRuleException("RemoveItem", "Só é possível remover itens de pedidos pendentes");

        var item = Items.FirstOrDefault(i => i.Id == itemId);
        
        if (item == null)
            throw new EntityNotFoundException("OrderItem", itemId);

        Items.Remove(item);
        RecalculateTotal();
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new BusinessRuleException("ConfirmOrder", "Apenas pedidos pendentes podem ser confirmados");

        if (!Items.Any())
            throw new BusinessRuleException("ConfirmOrder", "Pedido deve ter pelo menos um item");

        Status = OrderStatus.Confirmed;
    }

    public void StartPreparing()
    {
        if (Status != OrderStatus.Confirmed)
            throw new BusinessRuleException("StartPreparing", "Apenas pedidos confirmados podem iniciar preparo");

        Status = OrderStatus.Preparing;
    }

    public void SendForDelivery()
    {
        if (Status != OrderStatus.Preparing)
            throw new BusinessRuleException("SendForDelivery", "Apenas pedidos em preparo podem ser enviados");

        Status = OrderStatus.OutForDelivery;
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.OutForDelivery)
            throw new BusinessRuleException("MarkAsDelivered", "Apenas pedidos em entrega podem ser marcados como entregues");

        Status = OrderStatus.Delivered;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new BusinessRuleException("CancelOrder", "Pedidos já entregues não podem ser cancelados");

        if (Status == OrderStatus.Cancelled)
            throw new BusinessRuleException("CancelOrder", "Pedido já está cancelado");

        Status = OrderStatus.Cancelled;
    }

    private void RecalculateTotal()
    {
        TotalValue = Items.Sum(i => i.SubTotal);
    }

    private void ValidateCustomerId(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new InvalidEntityException("Order", "CustomerId não pode ser vazio");
    }

    private void ValidateRestaurantId(Guid restaurantId)
    {
        if (restaurantId == Guid.Empty)
            throw new InvalidEntityException("Order", "RestaurantId não pode ser vazio");
    }
}
