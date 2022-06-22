using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFG.API.Controllers;
using TFG.API.Dto.Request;
using TFG.Application.Contracts.Service;
using TFG.Domain.Entities;
using TFG.Unit.Tests.Setup;
using Xunit;

namespace TFG.Unit.Tests
{
    public class CustiomerControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IConfiguration _configuration;

        public CustiomerControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _mapperMock = new Mock<IMapper>();

            _configuration = TestMocks.SetupIConfiguration();
        }

        [Fact]
        public async Task GetCustomers_WithNoSearchCriteria_ReturnsAllCustomers()
        {
            TestMocks.SetupGetCustomersServiceMock(_customerServiceMock);
            TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, TestCustomer.GetCustomers().FirstOrDefault());

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.GetCustomers();

            var okResult = result as OkObjectResult;

            var statusCode = 200;

            var data = okResult.Value as IEnumerable<Customer>;

            Assert.Equal(okResult.StatusCode, statusCode);

        }

        [Fact]
        public async Task GetCustomers_WithSearchCriteria_ReturnsAllCustomers()
        {
            TestMocks.SetupGetCustomersServiceMock(_customerServiceMock);
            TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, TestCustomer.GetCustomers().FirstOrDefault());

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.GetCustomers("ayi");

            var okResult = result as OkObjectResult;

            var statusCode = 200;

            var data = okResult.Value as IEnumerable<Customer>;

            Assert.Equal(okResult.StatusCode, statusCode);
        }

        [Fact]
        public async Task GetCustomer_WithId_ReturnsSpecificCutomer()
        {

            var customer = TestCustomer.GetCustomers().FirstOrDefault(x => x.Id == "62b212b7aa7678cee7faad0d");
            TestMocks.SetupGetCustomerByIdServiceMock(_customerServiceMock, customer);

            TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, customer);

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.GetCustomer("62b212b7aa7678cee7faad0d");

            var okResult = result as OkObjectResult;

            var statusCode = 200;

            var data = okResult.Value as Customer;

            Assert.Equal(okResult.StatusCode, statusCode);
        }

        [Fact]
        public async Task GetCustomer_WithWrongId_ReturnsNotFoundResponse()
        {

            var customer = TestCustomer.GetCustomers().FirstOrDefault(x => x.Id == "62b212b7aa7678cee7faad0d");
            TestMocks.SetupGetCustomerByIdServiceMock(_customerServiceMock, customer);

            TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, customer);

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.GetCustomer("62b212b7aa7678cee7faad0dffff");

            var okResult = result as NotFoundResult;

            var statusCode = 404;

            Assert.Equal(okResult.StatusCode, statusCode);
        }

        [Fact]
        public async Task UpdateCustomer_WithRightId_ReturnsNoContent()
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
            TestMocks.SetupGetCustomerByIdServiceMock(_customerServiceMock, customer);

            TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, customer);

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.UpdateCustomer("62b212b7aa7678cee7faad0d",customerDto);

            var okResult = result as NoContentResult;

            var statusCode = 204;

            Assert.Equal(okResult.StatusCode, statusCode);
        }

        [Fact]
        public async Task UpdateCustomer_WithWrongId_ReturnsNoFound()
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
            TestMocks.SetupGetCustomerByIdServiceMock(_customerServiceMock, customer);

            TestMocks.SetResponseDtoCustomerMappingMock(_mapperMock, customer);

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.UpdateCustomer("62b212b7aa7678cee7faad0dffff", customerDto);

            var okResult = result as NotFoundResult;

            var statusCode = 404;

            Assert.Equal(okResult.StatusCode, statusCode);
        }

        [Fact]
        public async Task DeleteCustomer_WithRightId_ReturnsNoContent()
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
            TestMocks.SetupGetCustomerByIdServiceMock(_customerServiceMock, customer);

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.DeleteCustomer("62b212b7aa7678cee7faad0d");

            var okResult = result as NoContentResult;

            var statusCode = 204;

            Assert.Equal(okResult.StatusCode, statusCode);
        }

        [Fact]
        public async Task DeleteCustomer_WithWrongId_ReturnsNoFound()
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
            TestMocks.SetupGetCustomerByIdServiceMock(_customerServiceMock, customer);

            var customerController = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, null);

            var result = await customerController.DeleteCustomer("62b212b7aa7678cee7faad0dffff");

            var okResult = result as NotFoundResult;

            var statusCode = 404;

            Assert.Equal(okResult.StatusCode, statusCode);
        }
    }
}
