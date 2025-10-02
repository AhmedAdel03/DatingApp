using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Entities;
using Api.Interface;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class TokenService2(IConfiguration config) : ITokenService
{
    public string CreateToken(User user)
    {
        var TokenKey = config["TokenKey"];
        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email,user.Email),
            new(ClaimTypes.NameIdentifier,user.Name)

        };
        var SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = SigningCredentials
        };
        var TokenHandler = new JwtSecurityTokenHandler();
        var token = TokenHandler.CreateToken(TokenDescriptor);
        return TokenHandler.WriteToken(token);
        
    }
}
