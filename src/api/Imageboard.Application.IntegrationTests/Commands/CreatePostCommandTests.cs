using FluentAssertions;
using Imageboard.Application.Commands;
using Imageboard.Application.Exceptions;
using Imageboard.Application.IntegrationTests.Fakes;
using Imageboard.Application.Queries;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests.Commands
{
    using static Testing;

    public class CreatePostCommandTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            await SeedTestData();

            var command = new CreatePostCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateRootPost()
        {
            await SeedTestData();

            var command = new CreatePostCommand() { 
                TopicId = 1,
                Text = "Test Text",
                Signature = null
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldCreateReplyPost()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 1,
                ParentPostId = 1,
                Text = "Test Text",
                Signature = null
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldNotCreatePostForNonExistentTopic()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 10,
                Text = "Test Text",
                Signature = null
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldNotCreateReplyPostForNonExistentTopic()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 10,
                Text = "Test Text",
                Signature = null,
                ParentPostId = 1
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldNotCreateReplyPostForNonExistentParentPost()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 1,
                Text = "Test Text",
                Signature = null,
                ParentPostId = 10
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldNotCreateReplyPostForNonExistentTopicAndParentPost()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 10,
                Text = "Test Text",
                Signature = null,
                ParentPostId = 10
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldCreateRootPostWithAttachments()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 1,
                Text = "Test Text",
                Signature = null,
                Attachments = new FormFileCollection()
                {
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg")
                }
            };

            var newPostId = await SendAsync(command);

            var getPostComand = new GetPostQuery()
            {
                Id = newPostId
            };

            var newPost = await SendAsync(getPostComand);

            newPost.Id.Should().Be(newPostId);
            newPost.TopicId.Should().Be(1);
            newPost.Text.Should().Be("Test Text");

            newPost.Attachments
                .Should().HaveCount(1);

            var attachment = newPost.Attachments.First();
            attachment.OriginalFilename.Should().Be("sasha.jpeg");
            attachment.ContentType.Should().Be("image/jpeg");
        }

        [Test]
        public async Task ShouldCreateReplyPostWithAttachments()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 1,
                ParentPostId = 1,
                Text = "Test Text",
                Signature = null,
                Attachments = new FormFileCollection()
                {
                    new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg")
                }
            };

            var newPostId = await SendAsync(command);

            var getPostComand = new GetPostQuery()
            {
                Id = newPostId
            };

            var newPost = await SendAsync(getPostComand);

            newPost.Id.Should().Be(newPostId);
            newPost.TopicId.Should().Be(1);
            newPost.ParentId.Should().Be(1);
            newPost.Text.Should().Be("Test Text");

            newPost.Attachments
                .Should().HaveCount(1);

            var attachment = newPost.Attachments.First();
            attachment.OriginalFilename.Should().Be("sasha.jpeg");
            attachment.ContentType.Should().Be("image/jpeg");
        }

        [Test]
        public async Task ShouldNotCreateRootPostWithManyAttachments()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 1,
                Text = "Test Text",
                Signature = null,
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
        public async Task ShouldCreateReplyPostWithManyAttachments()
        {
            await SeedTestData();

            var command = new CreatePostCommand()
            {
                TopicId = 1,
                ParentPostId = 1,
                Text = "Test Text",
                Signature = null,
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
    }
}
