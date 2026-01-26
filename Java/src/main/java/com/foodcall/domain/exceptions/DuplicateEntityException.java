package com.foodcall.domain.exceptions;

public class DuplicateEntityException extends DomainException {
    
    public DuplicateEntityException(String entityName, String identifier) {
        super(String.format("%s com '%s' já existe no sistema.", entityName, identifier));
    }
    
    public DuplicateEntityException(String message) {
        super(message);
    }
}