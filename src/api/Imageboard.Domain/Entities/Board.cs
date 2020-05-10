using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }

        public string ShortUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }

        public virtual ICollection<Topic> Topics { get; set; } = new HashSet<Topic>();

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
