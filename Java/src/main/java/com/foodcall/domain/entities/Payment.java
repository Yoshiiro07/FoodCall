package com.foodcall.domain.entities;

import com.foodcall.domain.enums.PaymentMethod;
import com.foodcall.domain.exceptions.BusinessRuleException;
import com.foodcall.domain.exceptions.InvalidEntityException;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.UUID;

@Entity
@Table(name = "payments")
@Getter
@NoArgsConstructor
public class Payment {
    
    @Id
    private UUID id;
    
    @Column(nullable = false)
    private UUID orderId;
    
    @Column(nullable = false)
    private double amount;
    
    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private PaymentMethod method;
    
    @Column(nullable = false)
    private boolean isPaid;
    
    private LocalDateTime paidAt;
    
    public Payment(UUID orderId, double amount, PaymentMethod method) {
        validateOrderId(orderId);
        validateAmount(amount);
        validatePaymentMethod(method);
        
        this.id = UUID.randomUUID();
        this.orderId = orderId;
        this.amount = amount;
        this.method = method;
        this.isPaid = false;
        this.paidAt = null;
    }
    
    public void markAsPaid() {
        if (isPaid) {
            throw new BusinessRuleException("MarkAsPaid", "Pagamento já foi efetuado");
        }
        
        this.isPaid = true;
        this.paidAt = LocalDateTime.now();
    }
    
    public void cancelPayment() {
        if (!isPaid) {
            throw new BusinessRuleException("CancelPayment", "Não é possível cancelar um pagamento que não foi efetuado");
        }
        
        this.isPaid = false;
        this.paidAt = null;
    }
    
    private void validateOrderId(UUID orderId) {
        if (orderId == null) {
            throw new InvalidEntityException("Payment", "OrderId não pode ser vazio");
        }
    }
    
    private void validateAmount(double amount) {
        if (amount <= 0) {
            throw new InvalidEntityException("Payment", "Valor do pagamento deve ser maior que zero");
        }
        if (amount > 999999.99) {
            throw new InvalidEntityException("Payment", "Valor do pagamento deve ser menor que R$ 999.999,99");
        }
    }
    
    private void validatePaymentMethod(PaymentMethod method) {
        if (method == null) {
            throw new InvalidEntityException("Payment", "Método de pagamento inválido");
        }
    }
}