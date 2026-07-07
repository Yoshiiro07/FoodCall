// src/WebApi/Controllers/ProductsController.cs
using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetAllProducts;
using Application.Products.Queries.GetProductById;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Commands.DeleteProduct;
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

    [HttpPost]
    // POST: api/products/{id}
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var productId = await Mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
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

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new { Message = "O ID da URL não é o mesmo do ID do corpo da requisição." });
        }

        try
        {
            await Mediator.Send(command, cancellationToken);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await Mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}