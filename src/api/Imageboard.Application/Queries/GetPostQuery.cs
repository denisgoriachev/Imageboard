using AutoMapper;
using Imageboard.Application.Exceptions;
using Imageboard.Application.Models;
using Imageboard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application.Queries
{
    public class GetPostQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
    }

    public class GetPostQueryHandler : RequestHandlerBase<GetPostQuery, PostDto>
    {
        public GetPostQueryHandler(IImageboardDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await Context.Posts.SingleOrDefaultAsync(e => e.Id == request.Id);

            if (post == null)
                throw new NotFoundException(nameof(Post), request.Id);

            return Mapper.Map<PostDto>(post);
        }
    }
}
