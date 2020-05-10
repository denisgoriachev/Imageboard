using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imageboard.Application.Models
{
    public class FormFileCollection : List<IFormFile>, IFormFileCollection
    {
        public IFormFile this[string name] => this.FirstOrDefault(e => e.Name == name);

        public IFormFile GetFile(string name)
        {
            return this[name];
        }

        public IReadOnlyList<IFormFile> GetFiles(string name)
        {
            return this.Where(e => e.Name == name).ToList();
        }
    }
}
