using TFG.Domain.Entities;

namespace TFG.Application.Contracts.Service;

public interface ICustomerService {
    Task<Customer> Create (Customer customer);
    Task<bool> Update (Customer customer);
    Task<bool> Delete (Customer customer);
    Task<IEnumerable<Customer>> GetAsync ();
    Task<Customer> GetAsync (string Id);
    Task<Customer> GetCustomerByUsernameAsync (string username);
    Task<Customer> GetCustomerByEmailAsync (string email);
    Task<IEnumerable<Customer>> SearchAsync (string search);
}