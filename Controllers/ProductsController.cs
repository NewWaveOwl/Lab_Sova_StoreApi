using Lab00_Sova.DTOs;
using Lab00_Sova.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab00_Sova.Controllers
{

    [ApiController]
    [Route("api/products")]
    public sealed class ProductsController(IProductService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await service.GetAllAsync();
            return Ok(products.Select(product => product.ToDto()).ToList());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await service.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductRequest request)
        {
            try
            {
                var product = await service.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product.ToDto());
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Update(
            Guid id, UpdateProductRequest request)
        {
            try
            {
                var product = await service.UpdateAsync(id, request);
                return product is null ? NotFound() : Ok(product.ToDto());
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
