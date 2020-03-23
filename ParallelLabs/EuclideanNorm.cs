using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    public static class EuclideanNorm
    {
        public static double EuclideanNormCalculate(IEnumerable<double> vector)
        {
            double sum = 0d;

            foreach(var number in vector)
            {
                sum += number * number;
            }

            return Math.Sqrt(sum);
        }

        public static double CalculateParallel(IEnumerable<double> vector)
        {
            return vector
                .AsParallel()
                .AsUnordered()
                .Aggregate(
                    0d,
                    (subtotal, item) => subtotal + item * item,
                    (total, thisThread) => total + thisThread,
                    (finalSum) => Math.Sqrt(finalSum)
                );
        }

        public static async Task<double> CalculateParallelPartitions(List<double> vector, int partitions)
        {
            int partitionSize = (int)Math.Ceiling((double)vector.Count / partitions);

            var tasks = new List<Task<double>>(partitions);
            for(int i = 0; i < partitions; i++)
            {
                var subValues = vector.Skip(i * partitionSize).Take(partitionSize);
                tasks.Add(Task.Run(() => subValues.Sum(v => v * v)));
            }
            
            var taskResults = await Task.WhenAll(tasks);

            return Math.Sqrt(taskResults.Sum());
        }
    }
}
