using TFG.Application.Contracts.Persistence;
using TFG.Domain.Entities;
using TFG.Infrastructure.Data;

namespace TFG.Infrastructure.Repository;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
   public CustomerRepository(CustomerDataContext customerDataContext) : base(customerDataContext)
   {
       
   }
}