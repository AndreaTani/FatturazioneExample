using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.CustomerService
{
    public interface ICustomerService
    {
        Task AddCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> UpdateCustomer(Customer request);
        Task DeleteCustomer(int id);
    }
}
