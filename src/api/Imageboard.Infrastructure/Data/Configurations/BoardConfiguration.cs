using Imageboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Infrastructure.Data.Configurations
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasIndex(e => e.ShortUrl).IsUnique();

            builder.HasIndex(e => e.SortOrder);

            builder.Property(g => g.ShortUrl)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(g => g.Title)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(g => g.Description)
                .HasMaxLength(512)
                .IsRequired();
        }
    }
}
