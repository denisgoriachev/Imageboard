using FluentAssertions;
using Imageboard.Application.IntegrationTests.Fakes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application.IntegrationTests.Extensions
{
    public class FormFileExtensionsTests
    {
        [Test]
        public void ShouldAcceptFile()
        {
            var file = new FakeFormFile("TestFiles\\sasha.jpeg", "image/jpeg");
            file.IsImage().Should().BeTrue();
        }

        [Test]
        public void ShouldNotAcceptFakeTextFile()
        {
            var file = new FakeFormFile("TestFiles\\fakeimage.png", "image/png");
            file.IsImage().Should().BeFalse();
        }

        [Test]
        public void ShouldNotAcceptFakeHtmlFile()
        {
            var file = new FakeFormFile("TestFiles\\html.png", "image/png");
            file.IsImage().Should().BeFalse();
        }
    }
}
