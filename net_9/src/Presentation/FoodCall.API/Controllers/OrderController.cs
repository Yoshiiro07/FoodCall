using System;
using System.Threading.Tasks;
using FoodCall.Application.Commands;
using FoodCall.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FoodCall.Application.Queries;

namespace FoodCall.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="createOrderDto">Order creation data</param>
        /// <returns>Created order details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                _logger.LogInformation("Creating new order for customer {CustomerId}", createOrderDto.CustomerId);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var command = new CreateOrderCommand(createOrderDto);
                var result = await _mediator.Send(command);

                _logger.LogInformation("Order created successfully with ID {OrderId}", result.Id);

                return CreatedAtAction(
                    nameof(GetOrderById), 
                    new { id = result.Id }, 
                    result
                );
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument provided for order creation");
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation during order creation");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating order");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Retrieves all Orders
        /// </summary>
        /// <returns>List of orders</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            try{
                var query = new GetAllOrdersQuery();
                var result = await _mediator.Send(query);
                if (result == null || !result.Any()){
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Gets an order by ID
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order details</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving order with ID {OrderId}", id);

                if (id == Guid.Empty)
                {
                    return BadRequest(new { message = "Invalid order ID" });
                }

                var query = new GetOrderByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    _logger.LogWarning("Order with ID {OrderId} not found", id);
                    return NotFound(new { message = $"Order with ID {id} not found" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving order {OrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while processing your request." });
            }
        }
    }
}   