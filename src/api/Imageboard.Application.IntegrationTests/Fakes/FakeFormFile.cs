using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests.Fakes
{
    public class FakeFormFile : IFormFile
    {
        private byte[] _content;

        public string ContentType { get; }
        public string ContentDisposition { get; }
        public IHeaderDictionary Headers { get; }
        public long Length { get; }
        public string Name { get; }
        public string FileName { get; }

        public FakeFormFile(string filename, string contentType)
        {
            _content = File.ReadAllBytes(filename);
            ContentType = contentType;
            Length = _content.Length;
            Name = Path.GetFileName(filename);
            FileName = Path.GetFileName(filename);
        }

        public void CopyTo(Stream target)
        {
            target.Write(_content);
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            await target.WriteAsync(_content);
        }

        public Stream OpenReadStream()
        {
            return new MemoryStream(_content);
        }
    }
}
