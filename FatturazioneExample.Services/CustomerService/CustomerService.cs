using FatturazioneExample.Data.Data;
using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer is null)
            {
                throw new Exception("Customer not found!");
            }
            _context.Customers.Remove(customer);

            _context.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer is null)
            {
                throw new Exception("Customer not found!");
            }
            return customer;
        }

        public Customer UpdateCustomer(Customer request)
        {
            if (request is null)
            {
                throw new Exception("Bad data");
            }

            var customer = _context.Customers.Find(request.Id);

            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }

            customer.Name = request.Name;
            customer.Address = request.Address;

            _context.Customers.Update(customer);
            _context.SaveChanges();

            return customer;
        }
    }
}
