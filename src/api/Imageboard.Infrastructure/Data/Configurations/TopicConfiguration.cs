using Imageboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Infrastructure.Data.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasIndex(e => e.LastUpdated);

            builder.Property(g => g.Title)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(g => g.Signature)
                .HasMaxLength(32);
        }
    }
}
