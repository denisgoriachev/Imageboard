using Imageboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application
{
    public interface IImageboardDbContext
    {
        DbSet<Group> Groups { get; set; }

        DbSet<Board> Boards { get; set; }

        DbSet<Topic> Topics { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<Attachment> Attachments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
