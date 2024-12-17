using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_WebAPI.Models;

namespace My_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _repository;

        public CustomersController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("Get/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Customer? customer = await _repository.GetAsync(id);
            return customer != null ? Ok(customer) : BadRequest("Customer with provided ID doesn't exist.");
        }
        
        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            await _repository.AddAsync(customer);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, null);
        }

        [HttpPut("Update/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            Customer? cust = await _repository.GetAsync(id);
            if (cust == null)
                return BadRequest("Customer with provided ID doesn't exist.");

            cust.FirstName = customer.FirstName;
            cust.LastName = customer.LastName;

            _repository.Update(cust);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Customer? customer = await _repository.GetAsync(id);
            if (customer == null)
                return BadRequest("Customer with provided ID doesn't exist.");

            _repository.Remove(customer);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
