using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using TFG.Application.Contracts.Service;
using TFG.API.Controllers;
using TFG.API.Dto.Request;
using TFG.Domain.Entities;
using TFG.Unit.Tests.Setup;
using Xunit;
using System.Threading.Tasks;
using TFG.Application.Features.Service;
using Microsoft.AspNetCore.Mvc;
using TFG.API.Dto.Response;

namespace TFG.Unit.Tests;

public class AccountControllerTests
{
    private readonly Mock<ICustomerService> _customerServiceMock;
    private readonly ILoginService _loginServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRoleService> _roleServiceMock;
    private readonly IConfiguration _configuration;
    public AccountControllerTests()
    {
        _customerServiceMock = new Mock<ICustomerService>();

        _roleServiceMock = new Mock<IRoleService>();
        _mapperMock = new Mock<IMapper>();

        _configuration = TestMocks.SetupIConfiguration();
        _loginServiceMock = new LoginService(_configuration);
    }

    [Fact]
    public async Task Login_WithRightCredentials_ReturnsJwtToken()
    {
        var loginDto = new LoginDto
        {
            Username = "aiyanda",
            Password = "password123"
        };

        TestMocks.SetupGetCustomerUsernameServiceMock(_customerServiceMock, loginDto.Username);

        var accountController = new AccountController(_customerServiceMock.Object, _loginServiceMock,
            _roleServiceMock.Object, _configuration, _mapperMock.Object, null);

        var result = await accountController.Login(loginDto);

        var okResult = result as OkObjectResult;

        var response = okResult.Value as TokenDto;

        var statusCode = 200;

        Assert.Equal(okResult.StatusCode, statusCode);
        Assert.NotNull(response);
        Assert.NotEmpty(response.Token);
    }

    [Fact]
    public async Task Login_WrongPassword_ReturnsBadRequest()
    {
        var loginDto = new LoginDto
        {
            Username = "aiyanda",
            Password = "password12324534543534534"
        };

        TestMocks.SetupGetCustomerUsernameServiceMock(_customerServiceMock, loginDto.Username);

        var accountController = new AccountController(_customerServiceMock.Object, _loginServiceMock,
            _roleServiceMock.Object, _configuration, _mapperMock.Object, null);

        var result = await accountController.Login(loginDto);

        var badRequestObject = result as BadRequestObjectResult;

        var statusCode = 400;

        var response = badRequestObject.Value as ErrorDto;

        var messageExpected = "Invalid username or password";

        Assert.Equal(badRequestObject.StatusCode, statusCode);
        Assert.NotNull(response);
        Assert.Equal(response.Errors, messageExpected);
    }

    [Fact]
    public async Task RegisterCustomer_WithUserRole_ReturnsCustomerCreated()
    {
        var customerDto = new CustomerDto
        {
            FirstName = "test",
            LastName = "test",
            UserName = "test.test",
            Email = "test@test.com",
            Password = "password123",
            PhoneNumber = "0123484844"
        };

        TestMocks.SetDtoCustomerMappingMock(_mapperMock, customerDto);
        var customer = _mapperMock.Object.Map<Customer>(customerDto);
        TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, customer);
        TestMocks.SetupGetCustomerEmailServiceMock(_customerServiceMock, customerDto.Email);
        TestMocks.SetupGetCustomerUsernameServiceMock(_customerServiceMock, customerDto.UserName);
        TestMocks.SetupCreateCustomerServiceMock(_customerServiceMock, customer);
        TestMocks.SetGetRoleServiceMock(_roleServiceMock, "User");

        var accountController = new AccountController(_customerServiceMock.Object, _loginServiceMock,
            _roleServiceMock.Object, _configuration, _mapperMock.Object, null);

        var result = await accountController.RegisterCustomer(customerDto);

        var createdResult = result as CreatedAtActionResult;

        var response = createdResult.Value as CustomerResponseDto;

        var statusCode = 201;

        var role = "User";

        Assert.Equal(createdResult.StatusCode, statusCode);
        Assert.Equal(response.Id, customer.Id);
        Assert.Equal(response.Role.Name, role);
    }

    [Fact]
    public async Task RegisterCustomer_WithAdministratorRole_ReturnsCustomerCreated()
    {
        var customerDto = new CustomerDto
        {
            FirstName = "test",
            LastName = "test",
            UserName = "test.test",
            Email = "test@test.com",
            RoleName = "Administrator",
            Password = "password123",
            PhoneNumber = "0123484844"
        };

        TestMocks.SetDtoCustomerMappingMock(_mapperMock, customerDto);
        var customer = _mapperMock.Object.Map<Customer>(customerDto);
        TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, customer);
        TestMocks.SetupGetCustomerEmailServiceMock(_customerServiceMock, customerDto.Email);
        TestMocks.SetupGetCustomerUsernameServiceMock(_customerServiceMock, customerDto.UserName);
        TestMocks.SetupCreateCustomerServiceMock(_customerServiceMock, customer);
        TestMocks.SetGetRoleServiceMock(_roleServiceMock, "User");

        var accountController = new AccountController(_customerServiceMock.Object, _loginServiceMock,
            _roleServiceMock.Object, _configuration, _mapperMock.Object, null);

        var result = await accountController.RegisterCustomer(customerDto);

        var createdResult = result as CreatedAtActionResult;

        var response = createdResult.Value as CustomerResponseDto;

        var statusCode = 201;

        var role = "Administrator";

        Assert.Equal(createdResult.StatusCode, statusCode);
        Assert.Equal(response.Id, customer.Id);
        Assert.Equal(response.Role.Name,role );
    }

    [Fact]
    public async Task RegisterCustomer_WithUsernameAlreadyExist_ReturnsCustomerCreated()
    {
        var customerDto = new CustomerDto
        {
            FirstName = "test",
            LastName = "test",
            UserName = "test.test",
            Email = "test@test.com",
            RoleName = "Administrator",
            Password = "password123",
            PhoneNumber = "0123484844"
        };

        TestMocks.SetDtoCustomerMappingMock(_mapperMock, customerDto);
        var customer = _mapperMock.Object.Map<Customer>(customerDto);
        TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, customer);
        TestMocks.SetupGetCustomerEmailServiceMock(_customerServiceMock, customerDto.Email);
        TestMocks.SetupGetCustomerUsernameServiceMock(_customerServiceMock, customerDto.UserName);
        TestMocks.SetupCreateCustomerServiceMock(_customerServiceMock, customer);
        TestMocks.SetGetRoleServiceMock(_roleServiceMock, "User");

        var accountController = new AccountController(_customerServiceMock.Object, _loginServiceMock,
            _roleServiceMock.Object, _configuration, _mapperMock.Object, null);

        var result = await accountController.RegisterCustomer(customerDto);

        var createdResult = result as CreatedAtActionResult;

        var response = createdResult.Value as CustomerResponseDto;

        var statusCode = 201;

        var role = "Administrator";

        Assert.Equal(createdResult.StatusCode, statusCode);
        Assert.Equal(response.Id, customer.Id);
        Assert.Equal(response.Role.Name, role);
    }
}