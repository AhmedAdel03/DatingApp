using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
     public DbSet<Member> Members { get; set; }
     public DbSet<RefreshToken> RefreshTokens {get;set;}
      public DbSet<Photo> Photos { get; set; }
      public DbSet<MemberLikes> Likes { get; set; }
     protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
     base.OnModelCreating(modelBuilder);
     modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
     var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
    v => v.ToUniversalTime(),
    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
);

foreach (var entityType in modelBuilder.Model.GetEntityTypes())
{
    foreach (var property in entityType.GetProperties())
    {
        if (property.ClrType == typeof(DateTime))
        {
            property.SetValueConverter(dateTimeConverter);
        }
    }
}
 }
}
