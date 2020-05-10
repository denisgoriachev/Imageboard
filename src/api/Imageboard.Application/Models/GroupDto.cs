using AutoMapper;
using Imageboard.Application.Mappings;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imageboard.Application.Models
{
    public class GroupDto : IMapFrom<Group>
    {
        public int Id { get; set; }

        public int SortOrder { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<BoardShortDto> Boards { get; set; }
    }
}
