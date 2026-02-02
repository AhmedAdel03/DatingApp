using System;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories;

public class Repository<T> (AppDbContext context): IRepository<T> where T:class
{

    public async Task AddAsync(T Entity)
    {
        await context.Set<T>().AddAsync(Entity);
    }
    public async Task DeleteById(int id)
    {
          var entity = await context.Set<T>().FindAsync(id);
      if (entity == null)
        throw new KeyNotFoundException($"Entity with id {id} not found.");

    context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync(T Entity)
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
       return await context.SaveChangesAsync()>0;
    }

    public void Update(T Entity)
    {
                context.Set<T>().Update(Entity);

    }
}
