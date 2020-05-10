using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected IImageboardDbContext Context { get; }
        protected IMapper Mapper { get; }

        public RequestHandlerBase(IImageboardDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
