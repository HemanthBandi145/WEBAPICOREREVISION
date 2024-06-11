using WebAPIRevision.DTO;
using WebAPIRevision.Models;

namespace WebAPIRevision.Data
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);

        Task UpdateCustomer(CustomerDTO customer);
        Task DeleteCustomerByIdAsync(int id);
    }
}
