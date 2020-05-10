using FluentAssertions;
using Imageboard.Application.Commands;
using Imageboard.Application.Exceptions;
using Imageboard.Application.IntegrationTests.Fakes;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests.Commands
{
    using static Testing;
    public class CreateTopicCommandTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTopicCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTopicWithoutAttachment()
        {
            await SeedTestData();

            var command = new CreateTopicCommand()
            {
                BoardId = 1,
                Title = "Test Title",
                Text = "Test Text",
                Signature = "Test Signature"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldNotCreateTopicForNonExistentBoard()
        {
            await SeedTestData();

            var command = new CreateTopicCommand()
            {
                BoardId = 10,
                Title = "Test Title",
                Text = "Test Text",
                Signature = "Test Signature"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldCreateTopicWithAttachment()
        {
            await SeedTestData();

            var command = new CreateTopicCommand()
            {
                BoardId = 1,
                Title = "Test Title",
                Text = "Test Text",
                Signature = "Test Signature",
                Attachments = new FormFileCollection()
                {
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg")
                }
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldNotCreateTopicWithManyAttachments()
        {
            await SeedTestData();

            var command = new CreateTopicCommand()
            {
                BoardId = 1,
                Title = "Test Title",
                Text = "Test Text",
                Signature = "Test Signature",
                Attachments = new FormFileCollection()
                {
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg"),
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg"),
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg"),
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg"),
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg"),
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg")
                }
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldNotCreateTopicWithBadAttachments()
        {
            await SeedTestData();

            var command = new CreateTopicCommand()
            {
                BoardId = 1,
                Title = "Test Title",
                Text = "Test Text",
                Signature = "Test Signature",
                Attachments = new FormFileCollection()
                {
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg"),
                    new FakeFormFile("TestFiles\\html.png", "image/png"),
                }
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }
    }
}
