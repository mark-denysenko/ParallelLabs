using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab4.DiningPhilosophersProblem
{
    public class DiningTable
    {
        private IList<Fork> forks { get; set; }
        private IList<Philosopher> philosophers { get; set; }

        public DiningTable()
        {
            forks = Enumerable.Range(1, 5).Select(i => new Fork(i)).ToList();
            philosophers = Enumerable.Range(0, 5)
                .Select(i =>
                {
                    // previous fork
                    // for the first philosopher - last
                    Index leftFork = i == 0 ? ^1 : i - 1;
                    Index rightFork = i;

                    return new Philosopher(i + 1, forks[leftFork], forks[rightFork]);
                })
                .ToList();
        }

        public void Run()
        {
            var threads = new List<Thread>(5);

            foreach(var philosopher in philosophers)
            {
                var thread = new Thread(philosopher.StartDinner);
                threads.Add(thread);

                thread.Start();
            }

            foreach(var thread in threads)
            {
                thread.Join();
            }
        }
    }
}
