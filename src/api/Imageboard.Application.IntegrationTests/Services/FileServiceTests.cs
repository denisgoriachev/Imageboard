using FluentAssertions;
using Imageboard.Application.IntegrationTests.Fakes;
using Imageboard.Infrastructure.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests.Services
{
    using static Testing;

    public class FileServiceTests : TestBase
    {
        [Test]
        public void ShouldSaveFile()
        {
            var fileService = new FileService(Configuration);

            var file = new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg");

            FluentActions.Invoking(() => fileService.SaveFileAsync(file))
                .Should().NotThrow();
        }
    }
}
