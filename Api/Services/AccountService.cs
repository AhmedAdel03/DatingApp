using System;
using System.Security.Cryptography;
using System.Text;
using Api.Data.Repositories;
using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using Api.Interface;
using Api.Services.Interface;

namespace Api.Services;

public class AccountService(ITokenService tokenService,IAccountRepository accountRepository) : IAccountService
{
    
    public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
    {
        var user = await accountRepository.FindByEmailAsync(loginDTO.Email);
        if (user == null) throw new Exception("Invalid credentials");
        var hmac = new HMACSHA512(user.PassWordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
        if (computedHash.SequenceEqual(user.PassWordHash))
        {
            return await UserExtention.ToDTO(user, tokenService);
        }
        else
         {
              throw new Exception("Invalid credentials");;
        }
 
    }

    public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        var hmac = new HMACSHA512();
        var userExist = await accountRepository.FindByEmailAsync(registerDTO.Email);
        if (userExist != null) throw new Exception("User Exists");

        else
        {
            var password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            var PassWordSalt = hmac.Key;
            var newUser = new User
            {
                Email = registerDTO.Email,
                Name = registerDTO.DisplayName,
                PassWordHash = password,
                PassWordSalt = PassWordSalt,
                Member=new Member
                {
                     City=registerDTO.City,
                     Country=registerDTO.Country,
                     DateOfBirth=registerDTO.Dateofbirth,
                     Gender=registerDTO.Gender,
                     Description=registerDTO.Description,
                     DisplayName=registerDTO.DisplayName
                    
                }
            };
            await accountRepository.AddAsync(newUser);
            await accountRepository.SaveChangesAsync();
            return await UserExtention.ToDTO(newUser, tokenService);
        }
    }
}
