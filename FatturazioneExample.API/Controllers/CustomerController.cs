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
        public ActionResult AddCustomer(Customer customer)
        {
            _customerService.AddCustomer(customer);
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            if (customers is null)
            {
                return NotFound("Customers not found");
            }
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer is null)
            {
                return NotFound("Customers not found");
            }
            return Ok(customer);
        }

        [HttpPut]
        public ActionResult<Customer> UpdateCustomer(Customer request)
        {
            var customer = _customerService.UpdateCustomer(request);
            if (customer is null)
            {
                return NotFound("Customers not found");
            }
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return Ok();
        }

    }
}
