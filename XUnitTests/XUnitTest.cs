using Shared;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTests
{
	public class XUnitTest
	{
        private readonly ITestOutputHelper testOutputHelper;

        public XUnitTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

		[Fact]
		public async Task TestExecutionFlow()
		{
			var tc = new SharedTest();
			var res = await tc.TestExecutionFlow();
			testOutputHelper.WriteLine(tc.Output);
			Assert.True(res);
		}
	}
}
