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
     

    public async Task<User> FindByEmailAsync(string email)
    {

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        return user;
    }

     
}
