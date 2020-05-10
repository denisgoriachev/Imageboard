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

    public class GetPostQueryTests : TestBase
    {
        [Test]
        public async Task ShouldThrowOnCommandWithDefaults()
        {
            await SeedTestData();

            var command = new GetPostQuery();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldNotThrow()
        {
            await SeedTestData();

            var command = new GetPostQuery() { 
                Id = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should().NotThrow();
        }

        [Test]
        public async Task ShouldThrowOnNotExistinUrl()
        {
            await SeedTestData();

            var command = new GetPostQuery()
            {
                Id = 10
            };

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldReturnAnyData()
        {
            await SeedTestData();

            var command = new GetPostQuery()
            {
                Id = 1
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
        }
    }
}
