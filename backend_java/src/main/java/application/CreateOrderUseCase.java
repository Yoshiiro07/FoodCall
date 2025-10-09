package application;
import domain.Order.Order;

public interface CreateOrderUseCase {
    Order execute(CreateOrderCommand command);
}

