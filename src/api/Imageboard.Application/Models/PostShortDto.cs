using Imageboard.Application.Mappings;
using Imageboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application.Models
{
    public class PostShortDto : IMapFrom<Post>
    {
        public int Id { get; set; }

        public bool IsOp { get; set; }

        public string Text { get; set; }

        public string Signature { get; set; }
    }
}
