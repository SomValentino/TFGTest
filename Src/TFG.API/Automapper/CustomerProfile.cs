using AutoMapper;
using TFG.API.Dto.Request;
using TFG.API.Dto.Response;
using TFG.Domain.Entities;

namespace TFG.API.Automapper;

public class CustomerProfile : Profile {
    public CustomerProfile () {
        CreateMap<CustomerDto, Customer> ();
        CreateMap<Customer, CustomerResponseDto> ();
    }
}