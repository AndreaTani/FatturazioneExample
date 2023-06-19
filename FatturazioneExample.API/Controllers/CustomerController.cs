using FatturazioneExample.Data.Data;
using FatturazioneExample.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FatturazioneExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            return Ok(await  _context.Customers.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return BadRequest("Customer not found!");
            }
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer request)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return BadRequest("Customer not found!");
            }

            if (id != request.Id)
            {
                return BadRequest("Data mismatch!");
            }

            customer.Name = request.Name;
            customer.Address = request.Address;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return BadRequest("Customer not found!");
            }
            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
