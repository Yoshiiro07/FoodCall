namespace FoodCall.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    
    // Propriedade calculada: não precisa de um campo no banco, 
    // pois é baseada no preço unitário x quantidade.
    public decimal SubTotal => UnitPrice * Quantity;

    // Construtor para garantir que o item seja criado com dados válidos
    public OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (unitPrice < 0)
            throw new ArgumentException("O preço unitário não pode ser negativo.");

        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}