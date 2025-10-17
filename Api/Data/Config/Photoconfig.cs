using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Config;

public class Photoconfig : IEntityTypeConfiguration<Photo>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(x => x.PhotoId);
        
    }
}
