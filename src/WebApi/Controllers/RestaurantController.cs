using Application.Restaurants.Commands.CreateRestaurant;
using Application.Restaurants.Commands.DeleteRestaurant;
using Application.Restaurants.Commands.UpdateRestaurant;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class RestaurantsController : ApiControllerBase
    {
       [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateRestaurant([FromBody] UpdateRestaurantCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteRestaurant([FromBody] DeleteRestaurantCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
       
        [HttpPost]
        // POST: api/restaurants/{id} minha referencia
        public async Task<ActionResult<Guid>> Create([FromBody]CreateRestaurantCommand command, CancellationToken cancellationToken)
        {
            try{
                var restaurantId = await Mediator.Send(command, cancellationToken);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}