package com.foodcall.domain.entities;

import com.foodcall.domain.exceptions.InvalidEntityException;
import com.foodcall.domain.valueobjects.Address;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;


@Entity
@Table(name = "users")
@Getter
@NoArgsConstructor

public class User {

    @Id
    private UUID id;

    @Column(nullable = false, length = 200)
    private String name;
    
    @Column(nullable = false, unique = true, length = 255)
    private String email;
    
    @Column(nullable = false, length = 20)
    private String phone;
    
    @Column(nullable = false)
    private String passwordHash;


    @ElementCollection
    @CollectionTable(name = "user_addresses", joinColumns = @JoinColumn(name = "user_id"))
    private List<Address> addresses = new ArrayList<>();

    public User(String name, String email, String phone, String passwordHash) {
        validateName(name);
        validateEmail(email);
        validatePhone(phone);
        validatePasswordHash(passwordHash);

        this.id = UUID.randomUUID();
        this.name = name;
        this.email = email;
        this.phone = phone;
        this.passwordHash = passwordHash;
     
    }

    public void addAddress(Address address) {
        if (address == null) {
            throw new InvalidEntityException("Address", "Endereço não pode ser nulo");
        }
        this.addresses.add(address);
    }
    
    public void updateName(String name) {
        validateName(name);
        this.name = name;
    }
    
    public void updatePhone(String phone) {
        validatePhone(phone);
        this.phone = phone;
    }
    
    public void updatePassword(String passwordHash) {
        validatePasswordHash(passwordHash);
        this.passwordHash = passwordHash;
    }
    
    private void validateName(String name) {
        if (name == null || name.trim().isEmpty()) {
            throw new InvalidEntityException("User", "Nome não pode ser vazio");
        }
        if (name.length() < 3) {
            throw new InvalidEntityException("User", "Nome deve ter no mínimo 3 caracteres");
        }
        if (name.length() > 200) {
            throw new InvalidEntityException("User", "Nome deve ter no máximo 200 caracteres");
        }
    }
    
    private void validateEmail(String email) {
        if (email == null || email.trim().isEmpty()) {
            throw new InvalidEntityException("User", "Email não pode ser vazio");
        }
        if (!email.contains("@")) {
            throw new InvalidEntityException("User", "Email inválido");
        }
        if (email.length() > 255) {
            throw new InvalidEntityException("User", "Email deve ter no máximo 255 caracteres");
        }
    }
    
    private void validatePhone(String phone) {
        if (phone == null || phone.trim().isEmpty()) {
            throw new InvalidEntityException("User", "Telefone não pode ser vazio");
        }
        if (phone.length() < 10 || phone.length() > 20) {
            throw new InvalidEntityException("User", "Telefone deve ter entre 10 e 20 caracteres");
        }
    }
    
    private void validatePasswordHash(String passwordHash) {
        if (passwordHash == null || passwordHash.trim().isEmpty()) {
            throw new InvalidEntityException("User", "Hash da senha não pode ser vazio");
        }
    }
    
}
