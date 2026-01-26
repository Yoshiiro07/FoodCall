package com.foodcall.domain.enums;

public enum OrderStatus {
    PENDING,          // Aguardando confirmação do restaurante
    CONFIRMED,        // Restaurante aceitou
    PREPARING,        // Na cozinha
    OUT_FOR_DELIVERY, // Com o entregador
    DELIVERED,        // Entregue
    CANCELLED         // Cancelado
}
