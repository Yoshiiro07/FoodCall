package com.foodcall.domain.entities;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Entity
@Table(name = "order_items")
@Getter
@NoArgsConstructor
public class OrderItem {
    
    @Id
    private UUID id;
    
    @Column(nullable = false)
    private UUID productId;
    
    @Column(nullable = false, length = 200)
    private String productName;
    
    @Column(nullable = false)
    private double unitPrice;
    
    @Column(nullable = false)
    private int quantity;
    
    public OrderItem(UUID productId, String productName, double unitPrice, int quantity) {
        if (quantity <= 0) {
            throw new IllegalArgumentException("A quantidade deve ser maior que zero.");
        }
        if (unitPrice < 0) {
            throw new IllegalArgumentException("O preço unitário não pode ser negativo.");
        }
        
        this.id = UUID.randomUUID();
        this.productId = productId;
        this.productName = productName;
        this.unitPrice = unitPrice;
        this.quantity = quantity;
    }
    
    public double getSubTotal() {
        return unitPrice * quantity;
    }
}