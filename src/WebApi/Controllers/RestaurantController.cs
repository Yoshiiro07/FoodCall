using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetAllProducts;
using Application.Products.Queries.GetProductById;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Commands.DeleteProduct;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class RestaurantsController : ApiControllerBase
    {


        public RestaurantsController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAll(CancellationToken cancellationToken)
        {
            var restaurants = await Mediator.Send(new GetAllRestaurantsQuery(), cancellationToken);
            return Ok(restaurants);
        }
    }
}
