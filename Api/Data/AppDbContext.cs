using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
     public DbSet<Member> Members { get; set; }
      public DbSet<Photo> Photos { get; set; }
     protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
     base.OnModelCreating(modelBuilder);
     modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
 }
}
