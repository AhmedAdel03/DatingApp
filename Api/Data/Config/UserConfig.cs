using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.Name).IsRequired().HasColumnType("TEXT");
        builder.Property(x=>x.Name).IsRequired().HasColumnType("TEXT");

    }
}
