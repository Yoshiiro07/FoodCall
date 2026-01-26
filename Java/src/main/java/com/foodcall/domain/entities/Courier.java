package com.foodcall.domain.entities;

import com.foodcall.domain.exceptions.InvalidEntityException;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Entity
@Table(name = "couriers")
@Getter
@NoArgsConstructor
public class Courier {
    
    @Id
    private UUID id;
    
    @Column(nullable = false, length = 200)
    private String name;
    
    @Column(nullable = false, length = 7)
    private String vehiclePlate;
    
    @Column(nullable = false)
    private boolean isAvailable;
    
    public Courier(String name, String vehiclePlate) {
        validateName(name);
        validateVehiclePlate(vehiclePlate);
        
        this.id = UUID.randomUUID();
        this.name = name;
        this.vehiclePlate = vehiclePlate;
        this.isAvailable = true;
    }
    
    public void markAsAvailable() {
        this.isAvailable = true;
    }
    
    public void markAsUnavailable() {
        this.isAvailable = false;
    }
    
    public void updateName(String name) {
        validateName(name);
        this.name = name;
    }
    
    public void updateVehiclePlate(String vehiclePlate) {
        validateVehiclePlate(vehiclePlate);
        this.vehiclePlate = vehiclePlate;
    }
    
    private void validateName(String name) {
        if (name == null || name.trim().isEmpty()) {
            throw new InvalidEntityException("Courier", "Nome não pode ser vazio");
        }
        if (name.length() < 3) {
            throw new InvalidEntityException("Courier", "Nome deve ter no mínimo 3 caracteres");
        }
        if (name.length() > 200) {
            throw new InvalidEntityException("Courier", "Nome deve ter no máximo 200 caracteres");
        }
    }
    
    private void validateVehiclePlate(String vehiclePlate) {
        if (vehiclePlate == null || vehiclePlate.trim().isEmpty()) {
            throw new InvalidEntityException("Courier", "Placa do veículo não pode ser vazia");
        }
        
        String plateClean = vehiclePlate.replaceAll("[^a-zA-Z0-9]", "");
        
        if (plateClean.length() != 7) {
            throw new InvalidEntityException("Courier", "Placa do veículo deve ter 7 caracteres");
        }
    }
}