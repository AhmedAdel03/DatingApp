using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.Entities;
using Api.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class TokenService(IConfiguration config,AppDbContext context) : ITokenService
{
  public async Task<TokenResponse> CreateToken(User user)
    {
        var AccessToken=CreateAccessToken(user);
        var RefreshToken=await CreateRefreshToken(user);   
        await context.SaveChangesAsync();
       return new TokenResponse
       {
           AccessToken=AccessToken,
           RefreshToken=RefreshToken
       };
       
    }
    public async Task<User> CheckUserAndRefreshToken(string RefreshToken)
{
    var storedToken =await context.RefreshTokens.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Token==RefreshToken);
    if (storedToken == null)
        return null;
   if(VerifyRefreshToken(storedToken))
        {
            RevokeRefreshToken(storedToken);
            await context.SaveChangesAsync();
            return storedToken.User;
        }
      return null;
}
    
     private bool VerifyRefreshToken(RefreshToken storedToken)
    {
        if (storedToken == null||storedToken.IsRevoked==true||storedToken.ExpireDate< DateTime.UtcNow) return false;
         return true;
    }

    private void RevokeRefreshToken(RefreshToken storedToken)
    {
        storedToken.IsRevoked = true;
    }
         
     
    private async Task<string> CreateRefreshToken(User user)
    {
        var RefreshTokenCode=Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
         var refreshToken= new RefreshToken
        {
            Token=RefreshTokenCode ,
            UserID=user.UserId,
            IsRevoked=false,
            CreatedAt=DateTime.UtcNow
 
        };
        await context.RefreshTokens.AddAsync(refreshToken);
        return RefreshTokenCode;
    }

   

    private string CreateAccessToken(User user)
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
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = Creds

        };
        var TokenHandler = new JwtSecurityTokenHandler();
        var token =TokenHandler.CreateToken(TokenDescriptor);

        return TokenHandler.WriteToken(token);
         
    }

    
}
