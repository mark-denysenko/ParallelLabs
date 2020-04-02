using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5
{
    class Program
    {
        private static int listSize = 100;

        private static Random random = new Random(DateTime.Now.Millisecond);

        static void Main(string[] args)
        {
            Console.WriteLine("CompletableFuture");

            var list1 = new List<int>();
            var list2 = new List<int>();

            foreach(var _ in Enumerable.Range(0, listSize))
            {
                list1.Add(random.Next(0, 1000));
                list2.Add(random.Next(0, 1000));
            }

            var averageTask1 = Task.Run(() => list1.Average())
                .ContinueWith(average => list1.Where(element => element > average.Result));
            var averageTask2 = Task.Run(() => list2.Average())
                .ContinueWith(average => list2.Where(element => element < average.Result));

            var result = Task.Factory.ContinueWhenAll(
                new[] { averageTask1, averageTask2 }, 
                lists =>
                {
                    var l1 = lists[0].Result.ToList();
                    var l2 = lists[1].Result.ToList();

                    l1.Sort();
                    l2.Sort();

                    var unique1 = l1.Except(l2);
                    var unique2 = l2.Except(l1);

                    var unioned = unique1.Union(unique2).ToList();
                    unioned.Sort();

                    return unioned;
                });
            

            Console.WriteLine("Result = ");
            foreach(var num in result.Result)
            {
                Console.Write(" - " + num);
            }


            Console.ReadKey();
        }
    }
}
