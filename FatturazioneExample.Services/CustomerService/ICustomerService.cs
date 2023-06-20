using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.CustomerService
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        Customer UpdateCustomer(Customer request);
        void DeleteCustomer(int id);
    }
}
