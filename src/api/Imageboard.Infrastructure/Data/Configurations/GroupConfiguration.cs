using Imageboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Infrastructure.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasIndex(e => e.SortOrder);

            builder.Property(g => g.Title)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(g => g.Description)
                .HasMaxLength(512)
                .IsRequired();
        }
    }
}
