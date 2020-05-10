using Imageboard.Api.Conventions;
using Imageboard.Application.Commands;
using Imageboard.Application.Models;
using Imageboard.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imageboard.Api.Controllers
{
    [ApiConventionType(typeof(ImageboardApiConventions))]
    public class PostsController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<PostDto> Get(int id)
        {
            return await Mediator.Send(new GetPostQuery() { Id = id });
        }

        [HttpPost]
        public async Task<int> Create([FromForm]CreatePostCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
