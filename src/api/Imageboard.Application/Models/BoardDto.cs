using Imageboard.Application.Mappings;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application.Models
{
    public class BoardDto : IMapFrom<Board>
    {
        public int Id { get; set; }

        public string ShortUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }

        public virtual IEnumerable<TopicShortDto> Topics { get; set; } = new List<TopicShortDto>();

        public int GroupId { get; set; }
    }
}
