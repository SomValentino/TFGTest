using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using TFG.Application.Contracts.Service;
using TFG.API.Dto.Request;
using TFG.API.Dto.Response;
using TFG.Domain.Entities;
namespace TFG.Unit.Tests.Setup;

public class TestMocks {
    public static void SetupGetCustomerUsernameServiceMock (Mock<ICustomerService> mock, string username) {

        mock.Setup (x => x.GetCustomerByUsernameAsync (username)).ReturnsAsync (TestCustomer.GetCustomers ().Where (x => x.UserName == username).FirstOrDefault ());

    }

    public static void SetupGetCustomerEmailServiceMock (Mock<ICustomerService> mock, string email) {

        mock.Setup (x => x.GetCustomerByEmailAsync (email)).ReturnsAsync (TestCustomer.GetCustomers ().Where (x => x.Email == email).FirstOrDefault ());

    }

    public static void SetupGetCustomerByIdServiceMock (Mock<ICustomerService> mock, string id) {

        mock.Setup (x => x.GetAsync (id)).ReturnsAsync (TestCustomer.GetCustomers ().Where (x => x.Id == id).FirstOrDefault ());

    }

    public static void SetupSearchCustomererviceMock (Mock<ICustomerService> mock, string search) {

        mock.Setup (x => x.SearchAsync (search)).ReturnsAsync (TestCustomer.GetCustomers ().Where (x => x.UserName == search));

    }

    public static void SetupDeleteCustomererviceMock (Mock<ICustomerService> mock, Customer customer) {

        mock.Setup (x => x.Delete (customer)).ReturnsAsync (true);

    }

    public static void SetupCreateCustomerServiceMock (Mock<ICustomerService> mock, Customer customer) {

        mock.Setup (x => x.Create (customer)).ReturnsAsync (customer);

    }

    public static void SetupUpdateCustomerServiceMock (Mock<ICustomerService> mock, Customer customer) {

        mock.Setup (x => x.Update (customer)).ReturnsAsync (true);

    }

    public static void SetDtoCustomerMappingMock (Mock<IMapper> mock, CustomerDto customerDto) {
        var customer = new Customer {
            Id = "62b212b7aa7678cee7faad0d",
            UserName = customerDto.UserName,
            Role = customerDto.RoleName == "Administrator" ? new Role
            {
                Id = "62b211baaa7678cee7faad0c",
                Name = "Administrator",
                CreatedAt = DateTime.Now
            }:
            new Role
            {
                Id = "62b211baaa7678cee7faad0c",
                Name = "User",
                CreatedAt = DateTime.Now
            },
            Password = customerDto.Password,
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            Email = customerDto.Email,
            PhoneNumber = customerDto.PhoneNumber,
            CreatedAt = DateTime.Now
        };

        mock.Setup (x => x.Map<Customer> (customerDto)).Returns (customer);
    }
    public static void SetGetRoleServiceMock(Mock<IRoleService> mock, string rolename)
    {
        mock.Setup(x => x.GetRoleByName(rolename)).ReturnsAsync(TestRole.GetRoles().Where(x => x.Name == rolename).FirstOrDefault());
    }

    public static void SetResponseDtoCustomerMappingMock (Mock<IMapper> mock, Customer customer) {
        var customerResponseDto = new CustomerResponseDto {
            Id = customer.Id,
            UserName = customer.UserName,
            FirstName = customer.FirstName,
            Role = customer.Role,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CreatedAt = customer.CreatedAt
        };

        mock.Setup (x => x.Map<Customer, CustomerResponseDto> (customer)).Returns (customerResponseDto);
    }

    public static IConfiguration SetupIConfiguration () {
        return new ConfigurationBuilder ().AddInMemoryCollection (new Dictionary<string, string> { { "jwtSecret", TestConfiguration.GetJWTSecret () },
            { "jwtExpiry", TestConfiguration.GetJWTExpry ().ToString () },
            { "baseUrl", TestConfiguration.BaseUrl () }
        }).Build ();
    }
}