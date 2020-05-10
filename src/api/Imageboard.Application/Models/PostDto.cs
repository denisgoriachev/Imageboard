using Imageboard.Application.Mappings;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application.Models
{
    public class PostDto : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Signature { get; set; }

        public bool IsOp { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public int TopicId { get; set; }

        public int? ParentId { get; set; }

        public virtual IEnumerable<PostDto> Children { get; set; } = new List<PostDto>();

        public virtual IEnumerable<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
    }
}
