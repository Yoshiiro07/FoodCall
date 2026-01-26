package com.foodcall.domain.entities;

import com.foodcall.domain.exceptions.InvalidEntityException;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Entity
@Table(name = "categories")
@Getter
@NoArgsConstructor
public class Category {
    
    @Id
    private UUID id;
    
    @Column(nullable = false, length = 50)
    private String name;
    
    @Column(length = 200)
    private String description;
    
    @Column(nullable = false)
    private boolean isActive;
    
    public Category(String name, String description) {
        validateName(name);
        validateDescription(description);
        
        this.id = UUID.randomUUID();
        this.name = name;
        this.description = description;
        this.isActive = true;
    }
    
    public void updateName(String name) {
        validateName(name);
        this.name = name;
    }
    
    public void updateDescription(String description) {
        validateDescription(description);
        this.description = description;
    }
    
    public void activate() {
        this.isActive = true;
    }
    
    public void deactivate() {
        this.isActive = false;
    }
    
    private void validateName(String name) {
        if (name == null || name.trim().isEmpty()) {
            throw new InvalidEntityException("Category", "Nome não pode ser vazio");
        }
        if (name.length() < 2) {
            throw new InvalidEntityException("Category", "Nome deve ter pelo menos 2 caracteres");
        }
        if (name.length() > 50) {
            throw new InvalidEntityException("Category", "Nome não pode ter mais de 50 caracteres");
        }
    }
    
    private void validateDescription(String description) {
        if (description != null && !description.trim().isEmpty() && description.length() > 200) {
            throw new InvalidEntityException("Category", "Descrição não pode ter mais de 200 caracteres");
        }
    }
}