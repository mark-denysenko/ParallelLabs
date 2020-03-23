using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab4.DiningPhilosophersProblem
{
    public class Philosopher
    {
        public readonly int Id;

        public Fork LeftFork { get; }
        public Fork RightFork { get; }
        public int EatTimes { get; private set; }

        private int thinkTime => DateTime.Now.Millisecond;
        private const int timesToEat = 10;
        private const int timeToEat = 400;

        public Philosopher(int id, Fork leftFork, Fork rightFork)
        {
            Id = id;
            LeftFork = leftFork;
            RightFork = rightFork;
        }

        public void StartDinner()
        {
            EatTimes = 0;

            while (timesToEat > EatTimes)
            {
                Think();

                if (Monitor.TryEnter(LeftFork))
                {
                    LeftFork.IsTaken = true;

                    if (Monitor.TryEnter(RightFork))
                    {
                        RightFork.IsTaken = true;

                        Eat();

                        RightFork.IsTaken = true;
                        Monitor.Exit(RightFork);
                    }

                    LeftFork.IsTaken = false;
                    Monitor.Exit(LeftFork);
                }
            }

            Console.WriteLine(ToString() + " finished dinner");
        }

        private void Think()
        {
            Thread.Sleep(thinkTime);
            Console.WriteLine(ToString() + " is thinking ...");
        }

        private void Eat()
        {
            EatTimes++;
            Console.WriteLine(ToString() + $" is eating ... ({EatTimes})");
            Thread.Sleep(timeToEat);
        }

        public override string ToString()
        {
            return $"Philosopher {Id}";
        }
    }
}
