using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TFG.Application.Contracts.Service;
using TFG.API.Dto.Request;
using TFG.API.Dto.Response;
using TFG.Domain.Entities;

namespace TFG.API.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class AccountController : ControllerBase {
    private readonly ICustomerService _customerService;
    private readonly ILoginService _loginService;
    private readonly IRoleService _roleService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerController> _logger;

    public AccountController (ICustomerService customerService,
        ILoginService loginService,
        IRoleService roleService,
        IConfiguration configuration,
        IMapper mapper,
        ILogger<CustomerController> logger) {
        _customerService = customerService;
        _loginService = loginService;
        _roleService = roleService;
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost ("login")]
    public async Task<IActionResult> Login ([FromBody] LoginDto loginDto) {
        var customer = await _customerService.GetCustomerByUsernameAsync (loginDto.Username);

        if (customer == null) return BadRequest(new ErrorDto { Errors = "Invalid username or password" }); ;

        var result = await _loginService.SignIn (customer, loginDto.Password);

        if (!result.Success) return BadRequest (new ErrorDto { Errors = "Invalid username or password" });

        return Ok (new TokenDto { Token = result.Token, Scheme = "Bearer", Expiry = _configuration.GetValue<double> ("jwtExpiry") });
    }

    [HttpPost ("register")]
    public async Task<IActionResult> RegisterCustomer ([FromBody] CustomerDto customerDto) {
        var customer = _mapper.Map<Customer> (customerDto);

        var customerUserNameExist = await _customerService.GetCustomerByUsernameAsync (customerDto.UserName);

        if (customerUserNameExist != null) return BadRequest(new ErrorDto { Errors = "Invalid username or password" }); ;

        var customerEmailExist = await _customerService.GetCustomerByEmailAsync (customerDto.Email);

        if (customerEmailExist != null) return BadRequest(new ErrorDto { Errors = "Invalid username or password" }); ;

        var role = await _roleService.GetRoleByName (customerDto.RoleName);

        if (role == null) {
            role = await _roleService.GetRoleByName ("User"); // set to default user role
        }

        customer.Role = role;
        customer = await _customerService.Create (customer);
        var actionName = nameof (CustomerController.GetCustomer);

        var response = _mapper.Map<Customer, CustomerResponseDto>(customer);

        return CreatedAtAction (actionName, "Customer", new { id = customer.Id }, response);
    }

}