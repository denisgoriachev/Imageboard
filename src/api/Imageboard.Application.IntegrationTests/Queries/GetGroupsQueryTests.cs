using FluentAssertions;
using Imageboard.Application.Queries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests.Queries
{
    using static Testing;

    public class GetGroupsQueryTests : TestBase
    {
        [Test]
        public async Task ShouldNotThrow()
        {
            await SeedTestData();

            var command = new GetGroupsQuery();

            FluentActions.Invoking(() =>
                SendAsync(command))
                .Should().NotThrow();
        }

        [Test]
        public async Task ShouldReturnAnyData()
        {
            await SeedTestData();

            var command = new GetGroupsQuery();

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();

            var boardDto = result.First();

            boardDto.Title.Should().Be("Test Group 1");
            boardDto.SortOrder.Should().Be(1);

            boardDto.Boards.Should().NotBeNull();
            boardDto.Boards.Should().NotBeEmpty();
        }
    }
}
