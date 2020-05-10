using Imageboard.Application;
using Imageboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Infrastructure.Data
{
    public class ImageboardDbContext : DbContext, IImageboardDbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public ImageboardDbContext(
            DbContextOptions options) : base(options)
        {
            
        }

        public Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return base.Database.BeginTransactionAsync(isolationLevel);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
