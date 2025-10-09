package application;

import domain.Order.OrderItem;

import java.util.List;
import java.util.UUID;

public record CreateOrderCommand(UUID customerId, UUID restaurantId, List<OrderItem> items) {}

