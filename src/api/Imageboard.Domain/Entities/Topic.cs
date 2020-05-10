using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Domain.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Signature { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public int BoardId { get; set; }

        public virtual Board Board { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
