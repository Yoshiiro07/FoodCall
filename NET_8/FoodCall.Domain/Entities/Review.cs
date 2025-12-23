namespace FoodCall.Domain.Entities;

public class Review
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    
    // Avaliação do Restaurante
    public int RestaurantRating { get; private set; } // 1 a 5
    public string? RestaurantComment { get; private set; }
    
    // Avaliação do Entregador
    public int? CourierRating { get; private set; } // Opcional (caso seja retirada no local)
    public string? CourierComment { get; private set; }
    
    public DateTime CreatedAt { get; private set; }

    public Review(Guid orderId, Guid customerId, int restaurantRating, string? restaurantComment)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        CustomerId = customerId;
        RestaurantRating = ValidateRating(restaurantRating);
        RestaurantComment = restaurantComment;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddCourierReview(int rating, string? comment)
    {
        CourierRating = ValidateRating(rating);
        CourierComment = comment;
    }

    private int ValidateRating(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("A nota deve ser entre 1 e 5.");
        return rating;
    }
}