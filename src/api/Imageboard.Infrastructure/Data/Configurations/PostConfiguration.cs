using Imageboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Infrastructure.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasIndex(e => e.Created);

            builder.Property(g => g.Signature)
                .HasMaxLength(32);

            builder.Property(g => g.Text)
                .HasMaxLength(15000)
                .IsRequired();

            builder.HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
