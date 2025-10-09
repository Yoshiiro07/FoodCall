package web;

import java.math.BigDecimal;
import java.util.List;
import java.util.UUID;

public record CreateOrderRequest(UUID customerId, UUID restaurantId, List<OrderItemDTO> items) {}

public record OrderItemDTO(UUID productId, int quantity, BigDecimal price) {}


