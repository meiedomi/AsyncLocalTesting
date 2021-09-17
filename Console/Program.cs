using Shared;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tc = new SharedTest();
            var res = await tc.TestExecutionFlow();
            Console.Write(tc.Output);
            Console.WriteLine("Result: " + res);
        }
    }
}
