using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Entities;
using Api.Interface;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(User user)
    {
        var TokenKey = config["TokenKey"] ?? throw new Exception("cannot Get TokenKey");
        if (TokenKey.Length < 64) throw new Exception("Your TokenKey Less Than 64");
        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));
        var claims = new List<Claim>
        {
            new (ClaimTypes.Email,user.Email),
            new (ClaimTypes.NameIdentifier,user.UserId)

        };
        var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = Creds

        };
        var TokenHandler = new JwtSecurityTokenHandler();
        var token =TokenHandler.CreateToken(TokenDescriptor);
        return TokenHandler.WriteToken(token);
         
    }
}
