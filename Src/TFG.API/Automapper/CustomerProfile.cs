using AutoMapper;
using TFG.API.Dto.Request;
using TFG.Domain.Entities;

namespace TFG.API.Automapper;

public class CustomerProfile : Profile {
   public CustomerProfile()
   {
       var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDto, Customer> ());
   }
}