using FatturazioneExample.Data.Data;
using FatturazioneExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FatturazioneExample.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                throw new Exception("Customer not found!");
            }

            customer.IsDeleted = true;
            customer.DeletionDate = DateTime.Now;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers
                .Where(c => c.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                throw new Exception("Customer not found!");
            }
            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer request)
        {
            if (request is null)
            {
                throw new Exception("Bad data");
            }

            var customer = await _context.Customers
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }

            customer.Name = request.Name;
            customer.Address = request.Address;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
