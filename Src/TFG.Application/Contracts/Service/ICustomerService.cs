using TFG.Domain.Entities;

namespace TFG.Application.Contracts.Service;

public interface ICustomerService 
{
    Task<Customer> Create (Customer customer);
    Task<bool> Update (Customer customer);
    Task<bool> Delete (Customer customer);
    Task<IEnumerable<Customer>> GetAsync ();
    Task<Customer> GetAsync (string Id);
    Task<IEnumerable<Customer>> SearchAsync (string search);
}