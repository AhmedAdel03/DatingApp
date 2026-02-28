using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Config;

public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Token).HasMaxLength(200);
        builder.HasIndex(x=>x.Token).IsUnique();
        builder.HasOne(x=>x.User).WithMany().HasForeignKey(x=>x.UserID);
    }
}
