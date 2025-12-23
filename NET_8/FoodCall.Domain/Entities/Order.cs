using FoodCall.Domain.Enums;

namespace FoodCall.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid RestaurantId { get; private set; }
    public decimal TotalValue { get; private set; }
    public OrderStatus Status { get; private set; }
    
    // Esta é a linha que você citou
    public List<OrderItem> Items { get; private set; } = new();

    public Order(Guid customerId, Guid restaurantId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        RestaurantId = restaurantId;
        Status = OrderStatus.Pending;
    }

    // Método para adicionar itens garantindo a regra de negócio
    public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        var item = new OrderItem(productId, productName, unitPrice, quantity);
        Items.Add(item);
        
        // Recalcula o total do pedido sempre que um item é adicionado
        TotalValue += item.SubTotal;
    }
}