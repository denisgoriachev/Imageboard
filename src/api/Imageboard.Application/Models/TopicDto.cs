using AutoMapper;
using Imageboard.Application.Mappings;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application.Models
{
    public class TopicDto : IMapFrom<Topic>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Signature { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public int BoardId { get; set; }

        public virtual IEnumerable<PostDto> Posts { get; set; } = new List<PostDto>();

        public BoardShortDto Board { get; set; }
    }
}
