using System;
using Api.DTOs;
using Api.Interface;

namespace Api.Services.Interface;

public interface IAccountService
{
    public Task<UserDTO> LoginAsync(LoginDTO loginDTO);
      public Task<UserDTO> RegisterAsync(RegisterDTO loginDTO);


}
