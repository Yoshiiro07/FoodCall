package application;

import domain.Order.Order;
import domain.Order.OrderRepository;

public class CreateOrderService implements CreateOrderUseCase {

    private final OrderRepository orderRepository;

    public CreateOrderService(OrderRepository orderRepository) {
        this.orderRepository = orderRepository;
    }

    @Override
    public Order execute(CreateOrderCommand command) {
        Order order = new Order(command.customerId(), command.restaurantId(), command.items());
        return orderRepository.save(order);
    }
}

