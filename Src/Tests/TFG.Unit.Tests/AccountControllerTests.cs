using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using TFG.Application.Contracts.Service;
using TFG.API.Controllers;
using TFG.API.Dto.Request;
using TFG.Domain.Entities;
using TFG.Unit.Tests.Setup;
using Xunit;

namespace TFG.Unit.Tests;

public class AccountControllerTests {
    private readonly Mock<ICustomerService> _customerServiceMock;
    private readonly Mock<ILoginService> _loginServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRoleService> _roleServiceMock;
    private readonly IConfiguration _configuration;
    public AccountControllerTests () {
        _customerServiceMock = new Mock<ICustomerService> ();
        _loginServiceMock = new Mock<ILoginService> ();
        _roleServiceMock = new Mock<IRoleService> ();
        _mapper = new Mock<IMapper> ();

        _configuration = TestMocks.SetupIConfiguration ();
    }

    [Fact]
    public void Login_WithRightCredentials_ReturnsJwtToken () {
        var loginDto = new LoginDto {
            Username = "aiyanda",
            Password = "password123"
        };

        TestMocks.SetupGetCustomerUsernameServiceMock (_customerServiceMock, loginDto.Username);

        var accountController = new AccountController (_customerServiceMock.Object, _loginServiceMock.Object,
            _roleServiceMock.Object, _mapper.Object, _configuration, null);

        var result = await accountController.Login (loginDto);
    }
}