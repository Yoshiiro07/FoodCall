package com.foodcall.domain.entities;

import com.foodcall.domain.exceptions.InvalidEntityException;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Entity
@Table(name = "products")
@Getter
@NoArgsConstructor
public class Product {
    
    @Id
    private UUID id;
    
    @Column(nullable = false)
    private UUID restaurantId;
    
    @Column(nullable = false, length = 200)
    private String name;
    
    @Column(nullable = false, length = 500)
    private String description;
    
    @Column(nullable = false)
    private double price;
    
    public Product(UUID restaurantId, String name, String description, double price) {
        validateRestaurantId(restaurantId);
        validateName(name);
        validateDescription(description);
        validatePrice(price);
        
        this.id = UUID.randomUUID();
        this.restaurantId = restaurantId;
        this.name = name;
        this.description = description;
        this.price = price;
    }
    
    public void updatePrice(double price) {
        validatePrice(price);
        this.price = price;
    }
    
    public void updateName(String name) {
        validateName(name);
        this.name = name;
    }
    
    public void updateDescription(String description) {
        validateDescription(description);
        this.description = description;
    }
    
    private void validateRestaurantId(UUID restaurantId) {
        if (restaurantId == null) {
            throw new InvalidEntityException("Product", "RestaurantId não pode ser vazio");
        }
    }
    
    private void validateName(String name) {
        if (name == null || name.trim().isEmpty()) {
            throw new InvalidEntityException("Product", "Nome não pode ser vazio");
        }
        if (name.length() < 3) {
            throw new InvalidEntityException("Product", "Nome deve ter no mínimo 3 caracteres");
        }
        if (name.length() > 200) {
            throw new InvalidEntityException("Product", "Nome deve ter no máximo 200 caracteres");
        }
    }
    
    private void validateDescription(String description) {
        if (description == null || description.trim().isEmpty()) {
            throw new InvalidEntityException("Product", "Descrição não pode ser vazia");
        }
        if (description.length() > 500) {
            throw new InvalidEntityException("Product", "Descrição deve ter no máximo 500 caracteres");
        }
    }
    
    private void validatePrice(double price) {
        if (price <= 0) {
            throw new InvalidEntityException("Product", "Preço deve ser maior que zero");
        }
        if (price > 999999.99) {
            throw new InvalidEntityException("Product", "Preço deve ser menor que R$ 999.999,99");
        }
    }
}