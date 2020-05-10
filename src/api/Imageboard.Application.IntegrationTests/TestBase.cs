using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public void TestSetUp()
        {
            ResetState();
        }
    }
}
