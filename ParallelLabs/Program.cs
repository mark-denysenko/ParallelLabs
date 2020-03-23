using System;
using System.Diagnostics;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Mark Dneysenko IP-61 - LAB 1";
            Console.CursorVisible = false;

            Test();

            Console.ReadKey();
        }

        private static async void Test()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var vector = Enumerable.Range(0, 10_000_000).Select(_ => random.NextDouble()).ToList();

            var time = new Stopwatch();

            time.Start();
            var norm = EuclideanNorm.EuclideanNormCalculate(vector);
            time.Stop();
            Console.WriteLine($"Norm: \t{norm}");
            Console.WriteLine($"Sync time: \t{time.Elapsed}");

            time.Restart();
            norm = EuclideanNorm.CalculateParallel(vector);
            time.Stop();
            Console.WriteLine($"Norm: \t{norm}");
            Console.WriteLine($"Parallel time: \t{time.Elapsed}");

            time.Restart();
            norm = await EuclideanNorm.CalculateParallelPartitions(vector, 4);
            time.Stop();
            Console.WriteLine($"Norm: \t{norm}");
            Console.WriteLine($"Async time: \t{time.Elapsed}");

            
        }
    }
}
