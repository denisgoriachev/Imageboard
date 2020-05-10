using Imageboard.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _directory;

        public FileService(IConfiguration configuration)
        {
            _directory = configuration.GetValue<string>("FilesFolder");
        }

        public async Task<string> SaveFileAsync(IFormFile formFile, CancellationToken cancelationToken = default)
        {
            var filenameWithoutExtension = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(formFile.FileName);

            var filename = filenameWithoutExtension + extension;

            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);

            var fullpath = Path.Combine(_directory, filename);

            using (var file = new FileStream(fullpath, FileMode.CreateNew))
            {
                await formFile.CopyToAsync(file, cancelationToken);
                return filename;
            }
        }
    }
}
