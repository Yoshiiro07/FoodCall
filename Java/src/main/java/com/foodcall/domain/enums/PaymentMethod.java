package com.foodcall.domain.enums;

public enum PaymentMethod {
    CREDIT_CARD(1, "Cartão de Crédito"),
    DEBIT_CARD(2, "Cartão de Débito"),
    PIX(3, "PIX"),
    CASH(4, "Dinheiro"),
    VOUCHER(5, "Vale Refeição");


    private final int value;
    private final String description;

    PaymentMethod(int value, String description) {
        this.value = value;
        this.description = description;
    }

    public int getValue() {
        return value;
    }

    public String getDescription() {
        return description;
    }
}
