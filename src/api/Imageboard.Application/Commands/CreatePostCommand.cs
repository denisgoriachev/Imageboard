using AutoMapper;
using Imageboard.Application.Exceptions;
using Imageboard.Application.Models;
using Imageboard.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application.Commands
{
    public class CreatePostCommand : IRequest<int>
    {
        public int TopicId { get; set; }

        public int? ParentPostId { get; set; }

        public string Text { get; set; }

        public bool IsOp { get; set; }

        public string Signature { get; set; }

        public IFormFileCollection Attachments { get; set; } = new FormFileCollection();
    }

    public class CreatePostCommandHandler : RequestHandlerBase<CreatePostCommand, int>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IFileService _fileService;

        public CreatePostCommandHandler(IImageboardDbContext context, IMapper mapper, IDateTimeService dateTimeService, IFileService fileService) : base(context, mapper)
        {
            _dateTimeService = dateTimeService;
            _fileService = fileService;
        }

        public override async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var topic = await Context.Topics.SingleOrDefaultAsync(e => e.Id == request.TopicId);

            if (topic == null)
                throw new NotFoundException(nameof(Topic), request.TopicId);

            if (request.ParentPostId.HasValue && !await Context.Posts.AnyAsync(e => e.Id == request.ParentPostId.Value))
                throw new NotFoundException($"Parent post {request.ParentPostId} was not found");

            using(var transaction = await Context.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                var now = _dateTimeService.Now;

                var post = new Post()
                {
                    Text = request.Text,
                    Signature = request.Signature,
                    IsOp = request.IsOp,
                    ParentId = request.ParentPostId,
                    TopicId = request.TopicId,
                    Created = now
                };

                foreach (var formFile in request.Attachments)
                {
                    var filename = await _fileService.SaveFileAsync(formFile, cancellationToken);

                    var attachment = new Attachment()
                    {
                        ContentType = formFile.ContentType,
                        Filename = filename,
                        OriginalFilename = Path.GetFileName(formFile.FileName),
                        Created = now,
                        Size = formFile.Length
                    };

                    post.Attachments.Add(attachment);
                }

                Context.Posts.Add(post);
                topic.LastUpdated = now;

                try
                {
                    await Context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }

                return post.Id;
            }
        }
    }
}
