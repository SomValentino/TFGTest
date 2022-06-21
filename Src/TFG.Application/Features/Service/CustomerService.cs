using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using TFG.Application.Contracts.Persistence;
using TFG.Application.Contracts.Service;
using TFG.Domain.Entities;

namespace TFG.Application.Features.Service;

public class CustomerService : ICustomerService {
    private readonly ICustomerRepository _customerRepository;

    public CustomerService (ICustomerRepository customerRepository) {
        _customerRepository = customerRepository;
    }
    public async Task<Customer> Create (Customer customer) {
        return await _customerRepository.Create (customer);
    }

    public async Task<bool> Delete (Customer customer) {
        return await _customerRepository.Delete (customer);
    }

    public async Task<IEnumerable<Customer>> GetAsync () {
        return await _customerRepository.GetAsync ();
    }

    public async Task<Customer> GetAsync (string Id) {
        return await _customerRepository.GetAsync (Id);
    }

    public async Task<IEnumerable<Customer>> SearchAsync (string search) 
    {
        var queryExpr = new BsonRegularExpression (new Regex (search, RegexOptions.IgnoreCase));
        var builder = Builders<BsonDocument>.Filter;

        var firstNamefilter = builder.Regex ("FirstName", queryExpr);
        var lastNamefilter = builder.Regex ("LastName", queryExpr);
        var emailFilter = builder.Regex ("Email", queryExpr);
        var phoneNumberFilter = builder.Regex ("PhoneNumber", queryExpr);

        var combinedFilter = Builders<BsonDocument>.Filter.Or(firstNamefilter, lastNamefilter,
                                 emailFilter, phoneNumberFilter);

        return await _customerRepository.GetAsync(combinedFilter);
    }

    public async Task<bool> Update (Customer customer) {
        return await _customerRepository.Update(customer);
    }
}