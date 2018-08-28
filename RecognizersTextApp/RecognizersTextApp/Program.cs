using BenchmarkDotNet.Running;
using System;

namespace RecognizersTextApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<DateTimeRecognizerWrapper>();
            Console.WriteLine(summary.AllRuntimes);
            Console.ReadKey();
        }
    }
}
