using AutoMapper;
using Imageboard.Application.Exceptions;
using Imageboard.Application.Models;
using Imageboard.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application.Commands
{
    public class CreateTopicCommand : IRequest<int>
    {
        public int BoardId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Signature { get; set; }

        public IFormFileCollection Attachments { get; set; } = new FormFileCollection();
    }

    public class CreateTopicCommandHandler : RequestHandlerBase<CreateTopicCommand, int>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IFileService _fileService;

        public CreateTopicCommandHandler(IImageboardDbContext context, IMapper mapper, IDateTimeService dateTimeService, IFileService fileService) : base(context, mapper)
        {
            _dateTimeService = dateTimeService;
            _fileService = fileService;
        }

        public override async Task<int> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var board = await Context.Boards.SingleOrDefaultAsync(e => e.Id == request.BoardId);

            if (board == null)
                throw new NotFoundException(nameof(Board), request.BoardId);

            using(var transaction = await Context.BeginTransaction())
            {
                var now = _dateTimeService.Now;

                var post = new Post()
                {
                    Created = now,
                    Text = request.Text,
                    Signature = request.Signature,
                    IsOp = true
                };

                foreach(var formFile in request.Attachments)
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

                var topic = new Topic()
                {
                    BoardId = board.Id,
                    Title = request.Title,
                    Signature = request.Signature,
                    Created = now,
                    LastUpdated = now,
                    Posts =
                    {
                        post
                    }
                };

                Context.Topics.Add(topic);

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

                return topic.Id;
            }
        }
    }
}
