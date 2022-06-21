
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TFG.Application.Utils;

public class JWTTokenCreator {
    public static string GenerateAccessToken (IEnumerable<Claim> claims, string jwtSigningKey, double expiry, string issuer) {
        var key = new SymmetricSecurityKey (System.Text.Encoding.UTF8.GetBytes (jwtSigningKey));

        var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity (claims),
            Expires = DateTime.Now.AddSeconds (expiry),
            SigningCredentials = credentials,
            Issuer = issuer,
            Audience = issuer
        };

        var tokenHandler = new JwtSecurityTokenHandler ();

        var token = tokenHandler.CreateToken (tokenDescriptor);

        return tokenHandler.WriteToken (token);
    }
}