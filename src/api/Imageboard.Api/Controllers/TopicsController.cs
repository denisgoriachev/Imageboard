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
    public class TopicsController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<TopicDto> Get(int id)
        {
            return await Mediator.Send(new GetTopicQuery() { Id = id });
        }

        [HttpPost]
        public async Task<int> Create([FromForm]CreateTopicCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
