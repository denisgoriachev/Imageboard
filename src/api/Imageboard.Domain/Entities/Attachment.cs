using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Domain.Entities
{
    public class Attachment
    {
        public long Id { get; set; }

        public string OriginalFilename { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }

        public DateTime Created { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
