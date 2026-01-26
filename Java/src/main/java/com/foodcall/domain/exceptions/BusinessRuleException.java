package com.foodcall.domain.exceptions;

public class BusinessRuleException extends DomainException {
    public BusinessRuleException(String message) {
        super(message);
    }

    public BusinessRuleException(String rule, String reason){
        super(String.format("Regra de negócio violada (%s): %s", rule, reason));
    }
    
}
