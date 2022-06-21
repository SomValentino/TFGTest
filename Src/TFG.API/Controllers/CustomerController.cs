using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TFG.API.Dto.Request;
using TFG.Domain.Entities;

namespace TFG.Application.Contracts.Service;

[ApiController]
[Route ("api/[controller]")]
public class CustomerController : ControllerBase {
    private readonly IMapper _mapper;
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController (ICustomerService customerService,
        IMapper mapper,
        ILogger<CustomerController> logger) {
        _mapper = mapper;
        _customerService = customerService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers (string? search = null) {

        IEnumerable<Customer> customers = null;
        if (string.IsNullOrEmpty (search)) {
            customers = await _customerService.GetAsync ();
        } else {
            customers = await _customerService.SearchAsync (search);
        }

        return Ok (customers);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> GetCustomer (string id) {
        var customer = await _customerService.GetAsync (id);

        if (customer == null) return NotFound ();

        return Ok (customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer ([FromBody] CustomerDto customerDto) {
        var customer = _mapper.Map<Customer> (customerDto);

        customer = await _customerService.Create (customer);

        return Ok (customer);
    }

    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateCustomer (string id, [FromBody] CustomerDto customerDto) {
        var customer = await _customerService.GetAsync (id);

        if (customer == null) return NotFound ();

        _mapper.Map (customerDto, customer);

        await _customerService.Update (customer);

        return NoContent ();
    }

    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteCustomer (string id) 
    {
        var customer = await _customerService.GetAsync (id);

        if (customer == null) return NotFound ();

        await _customerService.Delete(customer);

        return NoContent ();
    }
}