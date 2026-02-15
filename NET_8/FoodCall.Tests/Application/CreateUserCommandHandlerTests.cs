using FluentAssertions;
using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Users.Commands.CreateUser;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using Moq;

namespace FoodCall.Tests.Application;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        
        _unitOfWorkMock.Setup(x => x.Users).Returns(_userRepositoryMock.Object);
        
        _handler = new CreateUserCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenDataIsValid()
    {
        // Arrange
        var createUserDto = new CreateUserDto(
            Name: "João Silva",
            Email: "joao@email.com",
            Phone: "11999999999",
            Password: "senha123",
            Addresses: new List<CreateAddressDto>
            {
                new CreateAddressDto(
                    Street: "Rua das Flores",
                    Number: "123",
                    Neighborhood: "Jardim Primavera",
                    City: "São Paulo",
                    ZipCode: "01234-567"
                )
            }
        );

        var command = new CreateUserCommand(createUserDto);

        _userRepositoryMock
            .Setup(x => x.ExistsByEmailAsync(createUserDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        User capturedUser = null!;
        _userRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>((user, _) => capturedUser = user)
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(createUserDto.Name);
        result.Email.Should().Be(createUserDto.Email);
        result.Phone.Should().Be(createUserDto.Phone);
        result.Addresses.Should().HaveCount(1);

        capturedUser.Should().NotBeNull();
        capturedUser.Name.Should().Be(createUserDto.Name);
        capturedUser.Email.Should().Be(createUserDto.Email);
        capturedUser.Addresses.Should().HaveCount(1);

        _userRepositoryMock.Verify(x => x.ExistsByEmailAsync(createUserDto.Email, It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        var createUserDto = new CreateUserDto(
            Name: "João Silva",
            Email: "joao@email.com",
            Phone: "11999999999",
            Password: "senha123",
            Addresses: new List<CreateAddressDto>()
        );

        var command = new CreateUserCommand(createUserDto);

        _userRepositoryMock
            .Setup(x => x.ExistsByEmailAsync(createUserDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DuplicateEntityException>()
            .WithMessage($"*{createUserDto.Email}*");

        _userRepositoryMock.Verify(x => x.ExistsByEmailAsync(createUserDto.Email, It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldHashPassword_BeforeCreatingUser()
    {
        // Arrange
        var plainPassword = "senha123";
        var createUserDto = new CreateUserDto(
            Name: "João Silva",
            Email: "joao@email.com",
            Phone: "11999999999",
            Password: plainPassword,
            Addresses: new List<CreateAddressDto>()
        );

        var command = new CreateUserCommand(createUserDto);

        _userRepositoryMock
            .Setup(x => x.ExistsByEmailAsync(createUserDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        User capturedUser = null!;
        _userRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>((user, _) => capturedUser = user)
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedUser.Should().NotBeNull();
        capturedUser.PasswordHash.Should().NotBe(plainPassword);
        capturedUser.PasswordHash.Should().StartWith("$2");  // BCrypt hash prefix
        BCrypt.Net.BCrypt.Verify(plainPassword, capturedUser.PasswordHash).Should().BeTrue();
    }
}
