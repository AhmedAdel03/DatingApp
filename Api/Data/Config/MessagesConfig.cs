using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Config;

public class MessageConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.HasOne(x=>x.Sender).WithMany(x=>x.MessagesSent).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x=>x.SenderId);
        builder.HasOne(x=>x.Recipient).WithMany(x=>x.MessagesRecived).OnDelete(DeleteBehavior.Restrict).HasForeignKey(x=>x.RecipientId);

         

    }
}
