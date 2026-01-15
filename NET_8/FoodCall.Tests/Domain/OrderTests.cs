using FluentAssertions;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Enums;
using FoodCall.Domain.Exceptions;

namespace FoodCall.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Order_ShouldBeCreated_WithValidData()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var restaurantId = Guid.NewGuid();

        // Act
        var order = new Order(customerId, restaurantId);

        // Assert
        order.Should().NotBeNull();
        order.Id.Should().NotBeEmpty();
        order.CustomerId.Should().Be(customerId);
        order.RestaurantId.Should().Be(restaurantId);
        order.Status.Should().Be(OrderStatus.Pending);
        order.Items.Should().BeEmpty();
        order.TotalValue.Should().Be(0);
    }

    [Fact]
    public void AddItem_ShouldAddItemToOrder_WhenItemIsValid()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid());
        var productId = Guid.NewGuid();
        var productName = "Pizza Margherita";
        var quantity = 2;
        var unitPrice = 25.50m;

        // Act
        order.AddItem(productId, productName, unitPrice, quantity);

        // Assert
        order.Items.Should().HaveCount(1);
        order.Items.First().ProductId.Should().Be(productId);
        order.Items.First().ProductName.Should().Be(productName);
        order.Items.First().Quantity.Should().Be(quantity);
        order.Items.First().UnitPrice.Should().Be(unitPrice);
        order.TotalValue.Should().Be(51.00m);
    }

    [Fact]
    public void AddItem_ShouldThrowException_WhenQuantityIsInvalid()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid());

        // Act
        Action act = () => order.AddItem(Guid.NewGuid(), "Produto", 25.50m, 0);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Quantidade*");
    }

    [Fact]
    public void AddItem_ShouldThrowException_WhenPriceIsNegative()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid());

        // Act
        Action act = () => order.AddItem(Guid.NewGuid(), "Produto", -10m, 2);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*preço*");
    }

    [Fact]
    public void TotalValue_ShouldBeCorrect_WithMultipleItems()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid());
        order.AddItem(Guid.NewGuid(), "Pizza", 25.00m, 2);  // 50.00
        order.AddItem(Guid.NewGuid(), "Refrigerante", 15.50m, 1);  // 15.50
        order.AddItem(Guid.NewGuid(), "Batata Frita", 10.00m, 3);  // 30.00

        // Assert
        order.TotalValue.Should().Be(95.50m);
    }

    [Fact]
    public void TotalValue_ShouldBeZero_WhenNoItems()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        order.TotalValue.Should().Be(0m);
    }

    [Fact]
    public void Confirm_ShouldUpdateOrderStatus_WhenOrderHasItems()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid());
        order.AddItem(Guid.NewGuid(), "Pizza", 25.00m, 1);

        // Act
        order.Confirm();

        // Assert
        order.Status.Should().Be(OrderStatus.Confirmed);
    }

    [Fact]
    public void StartPreparing_ShouldUpdateStatus_WhenOrderIsConfirmed()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), Guid.NewGuid());
        order.AddItem(Guid.NewGuid(), "Pizza", 25.00m, 1);
        order.Confirm();

        // Act
        order.StartPreparing();

        // Assert
        order.Status.Should().Be(OrderStatus.Preparing);
    }
}
