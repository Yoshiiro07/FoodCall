using FoodCall.Domain.Enums;

namespace FoodCall.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid RestaurantId { get; private set; }
    public decimal TotalValue { get; private set; }
    public OrderStatus Status { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public string? CancellationReason { get; private set; }

    public Order(Guid customerId, Guid restaurantId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        RestaurantId = restaurantId;
        Status = OrderStatus.Pending;
    }

    public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Items can only be added when the order is pending.");

        var item = new OrderItem(productId, productName, unitPrice, quantity);
        Items.Add(item);
        TotalValue += item.SubTotal;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order must be pending to confirm.");
        Status = OrderStatus.Confirmed;
    }

    public void StartPreparing()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Order must be confirmed to start preparing.");
        Status = OrderStatus.Preparing;
    }

    public void Dispatch()
    {
        if (Status != OrderStatus.Preparing)
            throw new InvalidOperationException("Order must be preparing to dispatch.");
        Status = OrderStatus.OutForDelivery;
    }

    public void Deliver()
    {
        if (Status != OrderStatus.OutForDelivery)
            throw new InvalidOperationException("Order must be out for delivery to deliver.");
        Status = OrderStatus.Delivered;
    }

    public void Cancel(string reason)
    {
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Delivered orders cannot be cancelled.");
        Status = OrderStatus.Cancelled;
        CancellationReason = string.IsNullOrWhiteSpace(reason) ? "Unspecified" : reason.Trim();
    }
}