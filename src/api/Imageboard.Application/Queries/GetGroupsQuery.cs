using AutoMapper;
using AutoMapper.QueryableExtensions;
using Imageboard.Application.Models;
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
    public class GetGroupsQuery : IRequest<IEnumerable<GroupDto>>
    {

    }

    public class GetGroupsQueryHandler : RequestHandlerBase<GetGroupsQuery, IEnumerable<GroupDto>>
    {
        public GetGroupsQueryHandler(IImageboardDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<IEnumerable<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var result = await Context.Groups
                    .ProjectTo<GroupDto>(Mapper.ConfigurationProvider)
                    .OrderBy(e => e.SortOrder)
                    .ToListAsync();

            foreach (var group in result)
            {
                group.Boards = group.Boards.OrderBy(e => e.SortOrder);
            }

            return result;
        }
    }
}
