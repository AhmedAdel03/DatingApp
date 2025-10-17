using System;
using System.Security.Cryptography;
using System.Text;
using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories;

public class AccountRepository : Repository<User>, IAccountRepository
{
    private readonly AppDbContext _context;
         

    public AccountRepository(AppDbContext context ) : base(context)
    {
        _context = context;
        
    }
    private async Task<bool> EmailExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    

    public async Task<User> LoginAsync(LoginDTO loginDTO)
    {

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);
        if (user == null) return null;
        var hmac = new HMACSHA512(user.PassWordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
        if (computedHash.SequenceEqual(user.PassWordHash))
        {
            return user;
        }
        else
        {
            return null;
        }
    }

    public async Task<User> RegisterAsync(RegisterDTO registerDTO)
    {
        var hmac = new HMACSHA512();
        if (await EmailExist(registerDTO.Email)) throw new Exception("Email Already Exist");
        var password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
        var PassWordSalt = hmac.Key;
        var newUser = new User
        {
            Email = registerDTO.Email,
            Name = registerDTO.Name,    
            PassWordHash = password,
            PassWordSalt = PassWordSalt
        };
        await  AddAsync(newUser);
        await  SaveChangesAsync();
        return newUser;

    }
}
