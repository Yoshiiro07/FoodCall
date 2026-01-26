package com.foodcall.domain.exceptions;

public class InvalidEntityException extends DomainException {
    
    public InvalidEntityException(String message) {
        super(message);
    }
    
    public InvalidEntityException(String entityName, String reason) {
        super(String.format("%s inválido(a): %s", entityName, reason));
    }
}