using System;
using Api.Data.Repositories;
using Api.DTOs;
using Api.Extensions;
using Api.Interface;
using Api.Services.Interface;

namespace Api.Services;

public class AccountService(ITokenService tokenService,IAccountRepository accountRepository) : IAccountService
{
    public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
    {
        var user = await accountRepository.LoginAsync(loginDTO);
        if (user == null) throw new Exception("Invalid credentials");
        
       return UserExtention.ToDTO(user, tokenService);

    }

    public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        var newUser = await accountRepository.RegisterAsync(registerDTO);
        return  UserExtention.ToDTO(newUser, tokenService);
    }
}
