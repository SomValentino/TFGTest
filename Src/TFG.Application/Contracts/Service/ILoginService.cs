using TFG.Application.Models;
using TFG.Domain.Entities;

namespace TFG.Application.Contracts.Service;

public interface ILoginService 
{
    Task<IdentityResult> SignIn (Customer customer,string password);
}