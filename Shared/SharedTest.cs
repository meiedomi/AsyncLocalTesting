using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared
{
    public class SharedTest
    {
		private StringBuilder _sb = new();

		private readonly AsyncLocal<int> asyncLocal;

		public SharedTest()
        {
			asyncLocal = new(args => {
				_sb.AppendLine($"Managed thread ID: {Thread.CurrentThread.ManagedThreadId}");
				_sb.AppendLine($"Context switch: {args.ThreadContextChanged}");
				_sb.AppendLine($"Previous value: {args.PreviousValue}");
				_sb.AppendLine($"Current value: {args.CurrentValue}");
				_sb.AppendLine("---");
			});

		}
		
		public string Output => _sb.ToString();

		public async Task<bool> TestExecutionFlow()
		{
			asyncLocal.Value = 42;

			// ConfigureAwait(false) is the only way to guarantee that thread X will run the continuation.
			await DisposeAsync().ConfigureAwait(false);

			// This continuation will be executed by thread X,
			// and this will have the same execution context as X,
			// which is why the value is 55.
			var actual = asyncLocal.Value;
			var result = actual == 55;
			return result;
		}

		private Task DisposeAsync()
		{
			var asyncFlowControl = ExecutionContext.SuppressFlow();

			// Suppressed, so nothing gets captured by the next call.
			return DisposeAsyncCore(asyncFlowControl);
		}

		private async Task DisposeAsyncCore(AsyncFlowControl asyncFlowControl)
		{
			// Now make sure to stop the suppressing here, as we want to normally flow again from here on.
			asyncFlowControl.Dispose();

			await DoStuffAsync();

			// The current thread is X.
			asyncLocal.Value = 55;
		} // Async method ends here, but the execution context will not be restored, since there was nothing captured to restore it to.

		private async Task DoStuffAsync()
		{
			await Task.Delay(10);
		}
	}
}
