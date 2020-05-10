using Imageboard.Application.Mappings;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application.Models
{
    public class AttachmentDto : IMapFrom<Attachment>
    {
        public long Id { get; set; }

        public string OriginalFilename { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public int Size { get; set; }

        public DateTime Created { get; set; }

        public int PostId { get; set; }
    }
}
