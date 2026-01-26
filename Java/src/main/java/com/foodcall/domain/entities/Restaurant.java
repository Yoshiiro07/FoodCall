package com.foodcall.domain.entities;

import com.foodcall.domain.exceptions.EntityNotFoundException;
import com.foodcall.domain.exceptions.InvalidEntityException;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

@Entity
@Table(name = "restaurants")
@Getter
@NoArgsConstructor
public class Restaurant {
    
    @Id
    private UUID id;
    
    @Column(nullable = false, length = 200)
    private String name;
    
    @Column(nullable = false, unique = true, length = 14)
    private String document; // CNPJ
    
    @Column(nullable = false)
    private boolean isActive;
    
    @OneToMany(mappedBy = "restaurantId", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Product> menu = new ArrayList<>();
    
    public Restaurant(String name, String document) {
        validateName(name);
        validateDocument(document);
        
        this.id = UUID.randomUUID();
        this.name = name;
        this.document = document;
        this.isActive = true;
    }
    
    public void addProduct(String name, String description, double price) {
        Product product = new Product(this.id, name, description, price);
        this.menu.add(product);
    }
    
    public void removeProduct(UUID productId) {
        Product product = menu.stream()
                .filter(p -> p.getId().equals(productId))
                .findFirst()
                .orElseThrow(() -> new EntityNotFoundException("Product", productId));
        
        this.menu.remove(product);
    }
    
    public void activate() {
        this.isActive = true;
    }
    
    public void deactivate() {
        this.isActive = false;
    }
    
    public void updateName(String name) {
        validateName(name);
        this.name = name;
    }
    
    private void validateName(String name) {
        if (name == null || name.trim().isEmpty()) {
            throw new InvalidEntityException("Restaurant", "Nome não pode ser vazio");
        }
        if (name.length() < 3) {
            throw new InvalidEntityException("Restaurant", "Nome deve ter no mínimo 3 caracteres");
        }
        if (name.length() > 200) {
            throw new InvalidEntityException("Restaurant", "Nome deve ter no máximo 200 caracteres");
        }
    }
    
    private void validateDocument(String document) {
        if (document == null || document.trim().isEmpty()) {
            throw new InvalidEntityException("Restaurant", "CNPJ não pode ser vazio");
        }
        
        String cnpjNumeros = document.replaceAll("\\D", "");
        
        if (cnpjNumeros.length() != 14) {
            throw new InvalidEntityException("Restaurant", "CNPJ deve conter 14 dígitos");
        }
    }
}