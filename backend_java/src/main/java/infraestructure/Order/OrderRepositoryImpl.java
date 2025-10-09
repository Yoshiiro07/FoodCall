package infraestructure.Order;

import domain.Order.Order;
import domain.Order.OrderRepository;
import domain.Order.OrderStatus;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

@Repository
public class OrderRepositoryImpl implements OrderRepository {

    private final JpaOrderRepository jpaOrderRepository;

    public OrderRepositoryImpl(JpaOrderRepository jpaOrderRepository) {
        this.jpaOrderRepository = jpaOrderRepository;
    }

    @Override
    public Order save(Order order) {
        OrderEntity entity = OrderMapper.toEntity(order);
        OrderEntity saved = jpaOrderRepository.save(entity);
        return OrderMapper.toDomain(saved);
    }

    @Override
    public Optional<Order> findById(UUID id) {
        return jpaOrderRepository.findById(id).map(OrderMapper::toDomain);
    }

    @Override
    public List<Order> findByCustomerId(UUID customerId) {
        return jpaOrderRepository.findByCustomerId(customerId)
                .stream().map(OrderMapper::toDomain).toList();
    }

    @Override
    public void updateStatus(UUID orderId, OrderStatus status) {
        jpaOrderRepository.updateStatus(orderId, status);
    }
}

