using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using TFG.Application.Contracts.Service;
using TFG.Application.Models;
using TFG.Application.Utils;
using TFG.Domain.Entities;

namespace TFG.Application.Features.Service;

public class LoginService : ILoginService {
    private readonly IConfiguration _configuration;

    public LoginService (IConfiguration configuration) {
        _configuration = configuration;
    }
    public async Task<IdentityResult> SignIn (Customer customer, string password) 
    {
        var result = new IdentityResult ();

        result.Success = customer.Password.compare (password);

        if (result.Success) {
            var secret = _configuration.GetValue<string> ("jwtSecret");
            var expiry = _configuration.GetValue<double> ("jwtExpiry");
            var issuer = _configuration.GetValue<string> ("baseUrl");

            result.Token = JWTTokenCreator.GenerateAccessToken (new [] { new Claim ("Id", customer.Id,"Role", customer.Role.Name) }, secret, expiry, issuer);

            return result;
        }

        result.ErrorMessage = "Invalid username or password";

        return result;
    }
}