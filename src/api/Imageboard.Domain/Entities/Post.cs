using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Signature { get; set; }

        public bool IsOp { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        public int? ParentId { get; set; }

        public virtual Post Parent { get; set; }

        public virtual ICollection<Post> Children { get; set; } = new HashSet<Post>();

        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}
