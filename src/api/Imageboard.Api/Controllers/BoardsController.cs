using Imageboard.Api.Conventions;
using Imageboard.Application.Models;
using Imageboard.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Api.Controllers
{
    [ApiConventionType(typeof(ImageboardApiConventions))]
    public class BoardsController : ApiController
    {
        [HttpGet("{shortUrl}")]
        public async Task<BoardDto> Get(string shortUrl)
        {
            return await Mediator.Send(new GetBoardQuery() { ShortUrl = shortUrl });
        }
    }
}
