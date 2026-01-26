package com.foodcall.domain.entities;

import com.foodcall.domain.exceptions.BusinessRuleException;
import com.foodcall.domain.exceptions.InvalidEntityException;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.UUID;

@Entity
@Table(name = "reviews")
@Getter
@NoArgsConstructor
public class Review {
    
    @Id
    private UUID id;
    
    @Column(nullable = false)
    private UUID orderId;
    
    @Column(nullable = false)
    private UUID customerId;
    
    @Column(nullable = false)
    private int restaurantRating; // 1 a 5
    
    @Column(length = 500)
    private String restaurantComment;
    
    private Integer courierRating; // Opcional
    
    @Column(length = 500)
    private String courierComment;
    
    @Column(nullable = false)
    private LocalDateTime createdAt;
    
    public Review(UUID orderId, UUID customerId, int restaurantRating, String restaurantComment) {
        validateOrderId(orderId);
        validateCustomerId(customerId);
        validateRating(restaurantRating);
        validateComment(restaurantComment, "Restaurant");
        
        this.id = UUID.randomUUID();
        this.orderId = orderId;
        this.customerId = customerId;
        this.restaurantRating = restaurantRating;
        this.restaurantComment = restaurantComment;
        this.createdAt = LocalDateTime.now();
    }
    
    public void addCourierReview(int rating, String comment) {
        if (courierRating != null) {
            throw new BusinessRuleException("AddCourierReview", "Avaliação do entregador já foi registrada");
        }
        
        validateRating(rating);
        validateComment(comment, "Courier");
        
        this.courierRating = rating;
        this.courierComment = comment;
    }
    
    public void updateRestaurantReview(int rating, String comment) {
        validateRating(rating);
        validateComment(comment, "Restaurant");
        
        this.restaurantRating = rating;
        this.restaurantComment = comment;
    }
    
    public void updateCourierReview(int rating, String comment) {
        if (courierRating == null) {
            throw new BusinessRuleException("UpdateCourierReview", "Não há avaliação do entregador para atualizar");
        }
        
        validateRating(rating);
        validateComment(comment, "Courier");
        
        this.courierRating = rating;
        this.courierComment = comment;
    }
    
    private void validateOrderId(UUID orderId) {
        if (orderId == null) {
            throw new InvalidEntityException("Review", "OrderId não pode ser vazio");
        }
    }
    
    private void validateCustomerId(UUID customerId) {
        if (customerId == null) {
            throw new InvalidEntityException("Review", "CustomerId não pode ser vazio");
        }
    }
    
    private void validateRating(int rating) {
        if (rating < 1 || rating > 5) {
            throw new InvalidEntityException("Review", "A nota deve ser entre 1 e 5");
        }
    }
    
    private void validateComment(String comment, String entity) {
        if (comment != null && !comment.trim().isEmpty() && comment.length() > 500) {
            throw new InvalidEntityException("Review", 
                String.format("Comentário do %s deve ter no máximo 500 caracteres", entity));
        }
    }
}