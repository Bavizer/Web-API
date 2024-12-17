using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_WebAPI.Models;

namespace My_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("Get/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetProduct(int id)
        {
            Product? product = await _repository.GetAsync(id);
            return product != null ? Ok(product) : BadRequest("Product with provided ID doesn't exist.");
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _repository.AddAsync(product);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, null);
        }

        [HttpPut("Update/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            Product? prod = await _repository.GetAsync(id);
            if (prod == null)
                return BadRequest("Product with provided ID doesn't exist.");

            prod.Name = product.Name;

            _repository.Update(prod);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product? product = await _repository.GetAsync(id);
            if (product == null)
                return BadRequest("Product with provided ID doesn't exist.");

            _repository.Remove(product);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
