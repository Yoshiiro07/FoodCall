using FluentAssertions;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.ValueObjects;

namespace FoodCall.Tests.Domain;

public class UserTests
{
    [Fact]
    public void User_ShouldBeCreated_WithValidData()
    {
        // Arrange
        var name = "João Silva";
        var email = "joao@email.com";
        var phone = "11999999999";
        var passwordHash = "hashed_password_123";

        // Act
        var user = new User(name, email, phone, passwordHash);

        // Assert
        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Name.Should().Be(name);
        user.Email.Should().Be(email);
        user.Phone.Should().Be(phone);
        user.PasswordHash.Should().Be(passwordHash);
        user.Addresses.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void User_ShouldThrowException_WhenNameIsInvalid(string? invalidName)
    {
        // Arrange & Act
        Action act = () => new User(invalidName, "email@test.com", "11999999999", "hash");

        // Assert
        act.Should().Throw<InvalidEntityException>()
            .WithMessage("*Nome*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid-email")]
    public void User_ShouldThrowException_WhenEmailIsInvalid(string? invalidEmail)
    {
        // Arrange & Act
        Action act = () => new User("João Silva", invalidEmail, "11999999999", "hash");

        // Assert
        act.Should().Throw<InvalidEntityException>()
            .WithMessage("*Email*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void User_ShouldThrowException_WhenPhoneIsInvalid(string? invalidPhone)
    {
        // Arrange & Act
        Action act = () => new User("João Silva", "email@test.com", invalidPhone, "hash");

        // Assert
        act.Should().Throw<InvalidEntityException>()
            .WithMessage("*Telefone*");
    }

    [Fact]
    public void AddAddress_ShouldAddAddressToUser_WhenAddressIsValid()
    {
        // Arrange
        var user = new User("João Silva", "joao@email.com", "11999999999", "hash");
        var address = new Address(
            "Rua das Flores",
            "123",
            null,
            "Jardim Primavera",
            "São Paulo",
            "SP",
            "01234-567"
        );

        // Act
        user.AddAddress(address);

        // Assert
        user.Addresses.Should().HaveCount(1);
        user.Addresses.First().Street.Should().Be("Rua das Flores");
    }

    [Fact]
    public void AddAddress_ShouldThrowException_WhenAddressIsNull()
    {
        // Arrange
        var user = new User("João Silva", "joao@email.com", "11999999999", "hash");

        // Act
        Action act = () => user.AddAddress(null!);

        // Assert
        act.Should().Throw<InvalidEntityException>()
            .WithMessage("*Endereço*nulo*");
    }

    [Fact]
    public void UpdateName_ShouldUpdateUserName_WhenNameIsValid()
    {
        // Arrange
        var user = new User("João Silva", "joao@email.com", "11999999999", "hash");
        var newName = "João Pedro Silva";

        // Act
        user.UpdateName(newName);

        // Assert
        user.Name.Should().Be(newName);
    }

    [Fact]
    public void UpdatePhone_ShouldUpdateUserPhone_WhenPhoneIsValid()
    {
        // Arrange
        var user = new User("João Silva", "joao@email.com", "11999999999", "hash");
        var newPhone = "11988888888";

        // Act
        user.UpdatePhone(newPhone);

        // Assert
        user.Phone.Should().Be(newPhone);
    }
}
