package com.foodcall.domain.exceptions;

import java.util.UUID;

public class EntityNotFoundException extends DomainException {
    
    public EntityNotFoundException(String entityName, UUID id) {
        super(String.format("%s com ID '%s' não foi encontrado(a).", entityName, id));
    }
    
    public EntityNotFoundException(String entityName, String identifier) {
        super(String.format("%s com identificador '%s' não foi encontrado(a).", entityName, identifier));
    }
}