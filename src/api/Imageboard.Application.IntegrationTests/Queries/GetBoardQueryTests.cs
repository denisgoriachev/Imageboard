using FluentAssertions;
using Imageboard.Application.Exceptions;
using Imageboard.Application.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests.Queries
{
    using static Testing;

    public class GetBoardQueryTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            await SeedTestData();

            var command = new GetBoardQuery();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldNotThrow()
        {
            await SeedTestData();

            var command = new GetBoardQuery() { 
                ShortUrl = "tb1"
            };

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should().NotThrow();
        }

        [Test]
        public async Task ShouldThrowOnNotExistinUrl()
        {
            await SeedTestData();

            var command = new GetBoardQuery()
            {
                ShortUrl = "not-existing-url"
            };

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldReturnAnyData()
        {
            await SeedTestData();

            var command = new GetBoardQuery() { 
                ShortUrl = "tb1"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
        }
    }
}
