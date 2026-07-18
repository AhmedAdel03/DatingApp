using System;
using Api.DTOs;
using Api.Entities;

namespace Api.Data.Repositories;

public interface IAccountRepository : IRepository<User>
{
    public Task<User> FindByEmailAsync(string email);
  
}
