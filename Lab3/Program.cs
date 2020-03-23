using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lab3
{
    class Program
    {
        // https://docs.microsoft.com/en-us/dotnet/api/system.threading.interlocked.compareexchange?view=netframework-4.8

        static void Main(string[] args)
        {
            var rand = new Random(DateTime.UtcNow.Millisecond);

            var numSequence = Enumerable.Range(0, 10000).Select(_ => rand.Next(0, int.MaxValue)).ToList();

            var counted = CountParalleByCondition(numSequence, x => x % 2 == 0);
            Console.WriteLine($"Counted by predicate x % 2 == 0 : {counted}");

            var hashSum = CountHashSum(numSequence);
            Console.WriteLine($"Hashsum: {hashSum}");

            var minMax = FindMinMaxParallel(numSequence);
            Console.WriteLine($"Min: {minMax.MinIndex} - {minMax.Min}");
            Console.WriteLine($"Max: {minMax.MaxIndex} - {minMax.Max}");


            Console.ReadKey();
        }

        private static int CountParalleByCondition(IEnumerable<int> sequence, Predicate<int> predicate)
        {
            int counter = 0;

            sequence.AsParallel().AsUnordered().ForAll(number =>
            {
                if (!predicate(number))
                {
                    return;
                }

                //Interlocked.Increment(ref counter);

                int oldValue;
                int newValue;
                do
                {
                    oldValue = counter;
                    newValue = oldValue + 1;
                } while (oldValue != Interlocked.CompareExchange(ref counter, newValue, oldValue));
            });

            return counter;
        }

        private static int CountHashSum(IEnumerable<int> sequence)
        {
            int sum = 0;

            sequence.AsParallel().AsUnordered().ForAll(number =>
            {
                int initialValue, computedValue;
                do
                {
                    // Save the current running total in a local variable.
                    initialValue = sum;

                    // Add the new value to the running total.
                    computedValue = initialValue ^ number;

                    // CompareExchange compares totalValue to initialValue. If
                    // they are not equal, then another thread has updated the
                    // running total since this loop started. CompareExchange
                    // does not update totalValue. CompareExchange returns the
                    // contents of totalValue, which do not equal initialValue,
                    // so the loop executes again.
                }
                while (initialValue != Interlocked.CompareExchange(ref sum,
                    computedValue, initialValue));
                // If no other thread updated the running total, then 
                // totalValue and initialValue are equal when CompareExchange
                // compares them, and computedValue is stored in totalValue.
                // CompareExchange returns the value that was in totalValue
                // before the update, which is equal to initialValue, so the 
                // loop ends.
            });

            return sum;
        }

        private static MinMaxWithIndexes FindMinMaxParallel(IList<int> sequence)
        {
            int minIndex = 0;
            int maxIndex = 0;

            Enumerable.Range(0, sequence.Count())
                .AsParallel()
                .AsUnordered()
                .ForAll(index =>
                {
                    int oldValue;
                    int newValue;

                    // could be added checking index 

                    // min
                    do
                    {
                        oldValue = minIndex;
                        newValue = index;

                        if (sequence[oldValue] <= sequence[newValue])
                        {
                            break;
                        }

                    } while (oldValue != Interlocked.CompareExchange(ref minIndex, newValue, oldValue));

                    // max
                    do
                    {
                        oldValue = maxIndex;
                        newValue = index;

                        if (sequence[oldValue] >= sequence[newValue])
                        {
                            break;
                        }

                    } while (oldValue != Interlocked.CompareExchange(ref maxIndex, newValue, oldValue));
                });

            return new MinMaxWithIndexes
            {
                MinIndex = minIndex,
                Min = sequence[minIndex],
                MaxIndex = maxIndex,
                Max = sequence[maxIndex]
            };
        }

        private class MinMaxWithIndexes
        {
            public int Min { get; set; } = int.MaxValue;
            public int MinIndex { get; set; }

            public int Max { get; set; } = int.MinValue;
            public int MaxIndex { get; set; }
        }
    }
}
