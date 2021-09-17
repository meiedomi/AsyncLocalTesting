using Shared;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace NUnitTests
{
    [TestFixture]
    public class NUnitTest
    {
		[Test]
		public async Task TestExecutionFlow()
		{
			var tc = new SharedTest();
			var res = await tc.TestExecutionFlow();
			Console.Write(tc.Output);
            Assert.IsTrue(res);
		}
	}
}