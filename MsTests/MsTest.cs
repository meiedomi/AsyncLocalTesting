using Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace MsTests
{
    [TestClass]
    public class MsTest
    {
		[TestMethod]
		public async Task TestExecutionFlow()
		{
			var tc = new SharedTest();
			var res = await tc.TestExecutionFlow();
			Console.Write(tc.Output);
			Assert.IsTrue(res);
		}
	}
}
