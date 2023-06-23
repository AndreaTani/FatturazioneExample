using FatturazioneExample.Data.Models;
using FatturazioneExample.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace FatturazioneExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            await _customerService.AddCustomer(customer);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            if (customers is null)
            {
                return NotFound("Customers not found");
            }
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer is null)
            {
                return NotFound("Customers not found");
            }
            return Ok(customer);
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer request)
        {
            var customer = await _customerService.UpdateCustomer(request);
            if (customer is null)
            {
                return NotFound("Customers not found");
            }
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return Ok();
        }

    }
}
