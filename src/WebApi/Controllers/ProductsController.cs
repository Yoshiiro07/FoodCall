// src/WebApi/Controllers/ProductsController.cs
using Application.Products.Queries.GetAllProducts;
using Application.Products.Queries.GetProductById;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ProductsController : ApiControllerBase
{
    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await Mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(products);
    }

    // GET: api/products/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await Mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        
        if (product == null)
        {
            return NotFound(new { Message = $"Produto com ID {id} não encontrado." });
        }

        return Ok(product);
    }
}