using FluentAssertions;
using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Users.Commands.CreateUser;
using FoodCall.Application.UseCases.Users.Queries.GetUserById;
using FoodCall.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FoodCall.API.Controllers;

namespace FoodCall.Tests.API;

public class UsersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnCreatedResult_WhenUserIsCreated()
    {
        // Arrange
        var createUserDto = new CreateUserDto(
            Name: "João Silva",
            Email: "joao@email.com",
            Phone: "11999999999",
            Password: "senha123",
            Addresses: new List<CreateAddressDto>()
        );

        var userDto = new UserDto(
            Id: Guid.NewGuid(),
            Name: createUserDto.Name,
            Email: createUserDto.Email,
            Phone: createUserDto.Phone,
            Addresses: new List<AddressDto>()
        );

        _mediatorMock
            .Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDto);

        // Act
        var result = await _controller.CreateUser(createUserDto);

        // Assert
        result.Should().NotBeNull();
        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.StatusCode.Should().Be(201);
        createdResult.Value.Should().BeEquivalentTo(userDto);
        createdResult.ActionName.Should().Be(nameof(_controller.GetUserById));

        _mediatorMock.Verify(x => x.Send(
            It.Is<CreateUserCommand>(c => c.User == createUserDto), 
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnOkResult_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDto = new UserDto(
            Id: userId,
            Name: "João Silva",
            Email: "joao@email.com",
            Phone: "11999999999",
            Addresses: new List<AddressDto>()
        );

        _mediatorMock
            .Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDto);

        // Act
        var result = await _controller.GetUserById(userId);

        // Assert
        result.Should().NotBeNull();
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(userDto);

        _mediatorMock.Verify(x => x.Send(
            It.Is<GetUserByIdQuery>(q => q.Id == userId), 
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task GetUserById_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityNotFoundException("User", userId));

        // Act
        Func<Task> act = async () => await _controller.GetUserById(userId);

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException>()
            .WithMessage($"*{userId}*");

        _mediatorMock.Verify(x => x.Send(
            It.Is<GetUserByIdQuery>(q => q.Id == userId), 
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }
}
