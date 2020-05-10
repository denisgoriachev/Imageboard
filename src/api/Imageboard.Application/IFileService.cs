using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Imageboard.Application
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile formFile, CancellationToken cancelationToken = default);
    }
}
