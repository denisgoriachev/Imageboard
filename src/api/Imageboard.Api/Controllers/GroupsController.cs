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
    public class GroupsController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<GroupDto>> Get()
        {
            return await Mediator.Send(new GetGroupsQuery());
        }
    }
}
