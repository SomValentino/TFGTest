using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFG.API.Dto.Request;
using TFG.API.Dto.Response;
using TFG.Application.Contracts.Service;
using TFG.Domain.Entities;

namespace TFG.API.Controllers;

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

        var response = _mapper.Map<IEnumerable<CustomerResponseDto>> (customers);

        return Ok (response);
    }
    
    [Authorize]
    [HttpGet ("{id}")]
    public async Task<IActionResult> GetCustomer (string id) {
        var customer = await _customerService.GetAsync (id);

        if (customer == null) return NotFound ();

        var response = _mapper.Map<CustomerResponseDto> (customer);

        return Ok (response);
    }

    
    [Authorize]
    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateCustomer (string id, [FromBody] CustomerDto customerDto) {
        var customer = await _customerService.GetAsync (id);

        if (customer == null) return NotFound ();

        _mapper.Map (customerDto, customer);

        await _customerService.Update (customer);

        return NoContent ();
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteCustomer (string id) {
        var customer = await _customerService.GetAsync (id);

        if (customer == null) return NotFound ();

        await _customerService.Delete (customer);

        return NoContent ();
    }
}