using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace My_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> _repository;

        public OrdersController(IRepository<Order> repository)
        {
            _repository = repository;
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("Get/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(int id)
        {
            Order? order = await _repository.GetAsync(id);
            return order != null ? Ok(order) : BadRequest("Order with provided ID doesn't exist.");
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrder([Required] int productId, [Required] uint amount, [Required] int customerId)
        {
            Product? product = await _repository.Context.Products.FindAsync(productId);
            Customer? customer = await _repository.Context.Customers.FindAsync(customerId);

            if (product == null || customer == null)
                return BadRequest("Wrong product ID and/or customer ID");

            Order order = new Order() { Product = product, Amount = amount, Customer = customer };
            await _repository.AddAsync(order);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, null);
        }

        [HttpPut("Update/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder(int id, [Required] uint amount)
        {
            Order? ord = await _repository.GetAsync(id);
            if (ord == null)
                return BadRequest("Order with provided ID doesn't exist.");

            ord.Amount = amount;

            _repository.Update(ord);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            Order? order = await _repository.GetAsync(id);
            if (order == null)
                return BadRequest("Order with provided ID doesn't exist.");

            _repository.Remove(order);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
