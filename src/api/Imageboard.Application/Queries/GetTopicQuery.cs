using AutoMapper;
using Imageboard.Application.Exceptions;
using Imageboard.Application.Models;
using Imageboard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application.Queries
{
    public class GetTopicQuery : IRequest<TopicDto>
    {
        public int Id { get; set; }
    }

    public class GetTopicQueryHandler : RequestHandlerBase<GetTopicQuery, TopicDto>
    {
        public GetTopicQueryHandler(IImageboardDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<TopicDto> Handle(GetTopicQuery request, CancellationToken cancellationToken)
        {
            var topic = await Context.Topics.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (topic == null)
                throw new NotFoundException(nameof(Topic), request.Id);

            var result = Mapper.Map<TopicDto>(topic);

            result.Posts = result.Posts.Where(e => e.ParentId == null).OrderBy(e => e.Created);

            foreach(var post in result.Posts)
            {
                SortChildrenPosts(post);
            }

            return result;
        }

        private void SortChildrenPosts(PostDto post)
        {
            post.Children = post.Children.OrderBy(e => e.Created);

            foreach(var child in post.Children)
            {
                SortChildrenPosts(child);
            }
        }
    }
}
