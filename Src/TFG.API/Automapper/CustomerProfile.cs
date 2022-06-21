using AutoMapper;
using TFG.API.Dto.Request;
using TFG.Domain.Entities;

namespace TFG.API.Automapper;

public class CustomerProfile : Profile {
    public CustomerProfile () {
        CreateMap<CustomerDto, Customer> ();
    }
}