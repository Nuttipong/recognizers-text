using BenchmarkDotNet.Running;
using System;

namespace RecognizersTextApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Recognizer's Sample console application!");
            Console.WriteLine();

            var summary = BenchmarkRunner.Run<DateTimeRecognizerWrapper>();
            Console.WriteLine(summary.AllRuntimes);
            Console.ReadKey();
        }
    }
}
