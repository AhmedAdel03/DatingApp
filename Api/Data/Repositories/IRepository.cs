using System;

namespace Api.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(T Entity);

    Task<T?> GetByIdAsync(string id);
    Task AddAsync(T Entity);
    void Update(T Entity);
    Task DeleteById(int Id);
   Task SaveChangesAsync();

 }
