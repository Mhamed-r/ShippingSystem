using Microsoft.IdentityModel.Tokens;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shipping.Api.Infrastructure.Repositories;

public class JWTProvider:IJWTProvider
{
    public (string token, int expiresIn) GenerateJwtToken(ApplicationUser user)
    {
        Claim[] claims = [
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Name,user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.CreateVersion7().ToString())
            ];
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lEQCxTrFYTOsyFtbtoWwPdDJ3066bWiP"));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);
        var expiresIn = 60;
        var expiration = DateTime.Now.AddMinutes(expiresIn);
        var token = new JwtSecurityToken(
            issuer: "Shipping",
            audience: "Shipping Users",
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );
        return (new JwtSecurityTokenHandler().WriteToken(token), expiresIn * 60);
    }
}
