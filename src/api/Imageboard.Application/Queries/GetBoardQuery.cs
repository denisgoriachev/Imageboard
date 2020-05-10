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
    public class GetBoardQuery : IRequest<BoardDto>
    {
        public string ShortUrl { get; set; }
    }

    public class GetBoardQueryHandler : RequestHandlerBase<GetBoardQuery, BoardDto>
    {
        public GetBoardQueryHandler(IImageboardDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<BoardDto> Handle(GetBoardQuery request, CancellationToken cancellationToken)
        {
            var board = await Context.Boards.FirstOrDefaultAsync(e => e.ShortUrl == request.ShortUrl);

            if(board == null)
                throw new NotFoundException(nameof(Board), request.ShortUrl);    

            var result = Mapper.Map<BoardDto>(board);
            result.Topics = result.Topics.OrderByDescending(e => e.LastUpdated);

            return result;
        }
    }
}
