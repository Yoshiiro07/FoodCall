using FoodCall.Domain.Exceptions;

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
        ValidateOrderId(orderId);
        ValidateCustomerId(customerId);
        ValidateRating(restaurantRating);
        ValidateComment(restaurantComment, "Restaurant");

        Id = Guid.NewGuid();
        OrderId = orderId;
        CustomerId = customerId;
        RestaurantRating = restaurantRating;
        RestaurantComment = restaurantComment;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddCourierReview(int rating, string? comment)
    {
        if (CourierRating.HasValue)
            throw new BusinessRuleException("AddCourierReview", "Avaliação do entregador já foi registrada");

        ValidateRating(rating);
        ValidateComment(comment, "Courier");

        CourierRating = rating;
        CourierComment = comment;
    }

    public void UpdateRestaurantReview(int rating, string? comment)
    {
        ValidateRating(rating);
        ValidateComment(comment, "Restaurant");

        RestaurantRating = rating;
        RestaurantComment = comment;
    }

    public void UpdateCourierReview(int rating, string? comment)
    {
        if (!CourierRating.HasValue)
            throw new BusinessRuleException("UpdateCourierReview", "Não há avaliação do entregador para atualizar");

        ValidateRating(rating);
        ValidateComment(comment, "Courier");

        CourierRating = rating;
        CourierComment = comment;
    }

    private void ValidateOrderId(Guid orderId)
    {
        if (orderId == Guid.Empty)
            throw new InvalidEntityException("Review", "OrderId não pode ser vazio");
    }

    private void ValidateCustomerId(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new InvalidEntityException("Review", "CustomerId não pode ser vazio");
    }

    private void ValidateRating(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new InvalidEntityException("Review", "A nota deve ser entre 1 e 5");
    }

    private void ValidateComment(string? comment, string entity)
    {
        if (!string.IsNullOrWhiteSpace(comment) && comment.Length > 500)
            throw new InvalidEntityException("Review", $"Comentário do {entity} deve ter no máximo 500 caracteres");
    }
}
