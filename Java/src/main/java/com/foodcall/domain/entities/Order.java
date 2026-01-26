package com.foodcall.domain.entities;

import com.foodcall.domain.enums.OrderStatus;
import com.foodcall.domain.exceptions.BusinessRuleException;
import com.foodcall.domain.exceptions.EntityNotFoundException;
import com.foodcall.domain.exceptions.InvalidEntityException;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

@Entity
@Table(name = "orders")
@Getter
@NoArgsConstructor
public class Order {
    
    @Id
    private UUID id;
    
    @Column(nullable = false)
    private UUID customerId;
    
    @Column(nullable = false)
    private UUID restaurantId;
    
    @Column(nullable = false)
    private double totalValue;
    
    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private OrderStatus status;
    
    @OneToMany(cascade = CascadeType.ALL, orphanRemoval = true)
    @JoinColumn(name = "order_id")
    private List<OrderItem> items = new ArrayList<>();
    
    public Order(UUID customerId, UUID restaurantId) {
        validateCustomerId(customerId);
        validateRestaurantId(restaurantId);
        
        this.id = UUID.randomUUID();
        this.customerId = customerId;
        this.restaurantId = restaurantId;
        this.status = OrderStatus.PENDING;
        this.totalValue = 0;
    }
    
    public void addItem(UUID productId, String productName, double unitPrice, int quantity) {
        if (status != OrderStatus.PENDING) {
            throw new BusinessRuleException("AddItem", "Só é possível adicionar itens em pedidos pendentes");
        }
        
        OrderItem item = new OrderItem(productId, productName, unitPrice, quantity);
        this.items.add(item);
        recalculateTotal();
    }
    
    public void removeItem(UUID itemId) {
        if (status != OrderStatus.PENDING) {
            throw new BusinessRuleException("RemoveItem", "Só é possível remover itens de pedidos pendentes");
        }
        
        OrderItem item = items.stream()
                .filter(i -> i.getId().equals(itemId))
                .findFirst()
                .orElseThrow(() -> new EntityNotFoundException("OrderItem", itemId));
        
        this.items.remove(item);
        recalculateTotal();
    }
    
    public void confirm() {
        if (status != OrderStatus.PENDING) {
            throw new BusinessRuleException("ConfirmOrder", "Apenas pedidos pendentes podem ser confirmados");
        }
        if (items.isEmpty()) {
            throw new BusinessRuleException("ConfirmOrder", "Pedido deve ter pelo menos um item");
        }
        this.status = OrderStatus.CONFIRMED;
    }
    
    public void startPreparing() {
        if (status != OrderStatus.CONFIRMED) {
            throw new BusinessRuleException("StartPreparing", "Apenas pedidos confirmados podem iniciar preparo");
        }
        this.status = OrderStatus.PREPARING;
    }
    
    public void sendForDelivery() {
        if (status != OrderStatus.PREPARING) {
            throw new BusinessRuleException("SendForDelivery", "Apenas pedidos em preparo podem ser enviados");
        }
        this.status = OrderStatus.OUT_FOR_DELIVERY;
    }
    
    public void markAsDelivered() {
        if (status != OrderStatus.OUT_FOR_DELIVERY) {
            throw new BusinessRuleException("MarkAsDelivered", "Apenas pedidos em entrega podem ser marcados como entregues");
        }
        this.status = OrderStatus.DELIVERED;
    }
    
    public void cancel() {
        if (status == OrderStatus.DELIVERED) {
            throw new BusinessRuleException("CancelOrder", "Pedidos já entregues não podem ser cancelados");
        }
        if (status == OrderStatus.CANCELLED) {
            throw new BusinessRuleException("CancelOrder", "Pedido já está cancelado");
        }
        this.status = OrderStatus.CANCELLED;
    }
    
    private void recalculateTotal() {
        this.totalValue = items.stream()
                .mapToDouble(OrderItem::getSubTotal)
                .sum();
    }
    
    private void validateCustomerId(UUID customerId) {
        if (customerId == null) {
            throw new InvalidEntityException("Order", "CustomerId não pode ser vazio");
        }
    }
    
    private void validateRestaurantId(UUID restaurantId) {
        if (restaurantId == null) {
            throw new InvalidEntityException("Order", "RestaurantId não pode ser vazio");
        }
    }
}