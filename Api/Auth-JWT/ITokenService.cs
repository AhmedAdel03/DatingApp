using System;
using Api.Entities;

namespace Api.Interface;

public interface ITokenService
{
    Task<TokenResponse> CreateToken(User user);
  Task<User> CheckUserAndRefreshToken(string RefreshToken);
  

}
