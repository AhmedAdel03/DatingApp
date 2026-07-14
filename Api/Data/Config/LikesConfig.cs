using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Config;

public class LikesConfig : IEntityTypeConfiguration<MemberLikes>
{
    public void Configure(EntityTypeBuilder<MemberLikes> builder)
    {
         builder.HasKey(x=> new
         {
             x.SourceMemberId,x.TargetMemberId
         });
         builder.HasOne(x=>x.SourceMember).WithMany(x=>x.LikesISent).HasForeignKey(x=>x.SourceMemberId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x=>x.TargetMember).WithMany(x=>x.LikesIRecived).HasForeignKey(x=>x.TargetMemberId).OnDelete(DeleteBehavior.NoAction);

    }
}
